using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NavitasBeta;
using NavitasBeta.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using Android.Webkit;
using System.ComponentModel;
using Android.App;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace NavitasBeta.Droid
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Android.Webkit.WebView>
    {
        //const string JavaScriptFunction = "function invokeCSharpAction(data){window.external.notify(data);}";
        const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
        //const string JavascriptFunction = "function invokeCSharpAction(data){drawWithInputValue($('#name').val());}";
        
        Context _context;

        public HybridWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        Android.Webkit.WebView webView;
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            webView.LayoutParameters.Height = -1;

            base.OnSizeChanged(w, h, oldw, oldh);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }
            var newWebView = e.NewElement as HybridWebView;
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    //RG Oct 2020: This makes no sense but
                    //without the try catch (which geneates a catch exception on older API versions)
                    //a first time start-up android webview error
                    //chromium(20606): [ERROR:filesystem_posix.cc(62)] mkdir /data/data/com.goi_inc.navitaseng/cache/WebView/Crashpad: No such file or directory (2)[ERROR:filesystem_posix.cc(62)] mkdir /data/data/com.goi_inc.navitaseng/cache/WebView/Crashpad: No such file or directory (2)
                    //causes the webview interface to fail (my webview just goes into simulator mode since they get no response from the c# invoke command
                    //Later in the day ......
                    //wow is all I can say, I commented out the lines and everything still worked even with the Error 62
                    //is it possible this corrected the phone??
                    //I really doubt it but I would leave it all in if I were you

                    try
                    {
                        int pid = Android.OS.Process.MyPid();
                        ActivityManager activityManager = (ActivityManager)_context.GetSystemService("activity");
                        System.Collections.Generic.IList<Android.App.ActivityManager.RunningAppProcessInfo> processInfo = activityManager.RunningAppProcesses;
                        for (int i = 0; i < processInfo.Count; i++)
                        {
                            if (processInfo[i].Pid == pid)
                            {
//                                Android.Webkit.WebView.SetDataDirectorySuffix(processInfo[i].ProcessName);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Hmm.. " + ex);
                    }

                    webView = new Android.Webkit.WebView(_context);
                    webView.Settings.JavaScriptEnabled = true;
                    webView.SetWebViewClient(new JavascriptWebViewClient($"javascript: {JavascriptFunction}"));
                    webView.SetLayerType(Android.Views.LayerType.Hardware, null);
                    //webView.Settings.LoadWithOverviewMode = true;
                    //webView.Settings.UseWideViewPort = true;
                    webView.SetWebChromeClient(new WebChromeClient());
                    webView.Settings.BuiltInZoomControls = true;
                    webView.Settings.DisplayZoomControls = false;
                    SetNativeControl(webView);
                }
                Control.Settings.SetAppCacheEnabled(false);
                Control.Settings.CacheMode = Android.Webkit.CacheModes.NoCache;
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");

                if (((HybridWebView)Element).Uri.Contains("Navitas/")) //must be in our public directory
                    Control.LoadUrl($"file:///{Element.Uri}");
                else
                    Control.LoadUrl($"file:///android_asset/Content/{Element.Uri}");

                newWebView.EvaluateJavascript = async (js) =>
                {

                    ManualResetEvent reset = new ManualResetEvent(false);
                    var response = "";
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //System.Diagnostics.Debug.WriteLine("Javascript Send: " + js);
                        try
                        {
                            Control?.EvaluateJavascript(js, new JavascriptCallback((r) => { response = r; reset.Set(); }));
                        }
                        catch(Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("I don't understand how to detect a disposed object" + ex);
                        }
                    });
                    await Task.Run(() => { reset.WaitOne(); });
                    if (response == "null")
                        response = string.Empty;

                    return response;
                };
                newWebView.InjectJavascript = async (js) =>
                {

                    ManualResetEvent reset = new ManualResetEvent(false);
                    var response = "";
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        //System.Diagnostics.Debug.WriteLine("Javascript Send: " + js);
                        Control?.EvaluateJavascript(js, new JavascriptCallback((r) => { response = r; reset.Set(); }));
                    });
                    await Task.Run(() => { reset.WaitOne(); });
                    if (response == "null")
                        response = string.Empty;

                    return response;
                };

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == HybridWebView.UriProperty.PropertyName)
            {
                if (Control.Url != null)
                {
                    string[] fileList = FileManager.GetNavitasDirectoryFiles();
                    bool fileAlreadyLoaded = false;
                    foreach (string localPublicFile in fileList)
                        if (localPublicFile.Contains(Element.Uri.Replace("?", "")) && !fileAlreadyLoaded)
                        {
                            Control.LoadUrl($"file:///{localPublicFile}");
                            fileAlreadyLoaded = true;
                        }

                    if (!fileAlreadyLoaded)
                        Control.LoadUrl($"file:///android_asset/Content/{Element.Uri}");
                }
            }
        }
        internal class JavascriptCallback : Java.Lang.Object, IValueCallback
        {
            public JavascriptCallback(Action<string> callback)
            {
                _callback = callback;
            }

            private Action<string> _callback;
            public void OnReceiveValue(Java.Lang.Object value)
            {
                //System.Diagnostics.Debug.WriteLine("Javascript Return: " + Convert.ToString(value));
                _callback?.Invoke(Convert.ToString(value));
            }
        }
    }
}
