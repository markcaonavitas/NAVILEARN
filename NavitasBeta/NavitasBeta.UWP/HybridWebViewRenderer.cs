using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavitasBeta;
using NavitasBeta.UWP;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Controls;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace NavitasBeta.UWP
{
    public class HybridWebViewRenderer : WebViewRenderer
    {
        const string JavaScriptFunction = "function invokeCSharpAction(data){window.external.notify(data);}";

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (Control == null)
                {
                    SetNativeControl(new Windows.UI.Xaml.Controls.WebView());
                }
                if (e.OldElement != null)
                {
                    Control.NavigationCompleted -= OnWebViewNavigationCompleted;
                    Control.ScriptNotify -= OnWebViewScriptNotify;
                }
                if (e.NewElement != null)
                {
                    Control.NavigationCompleted += OnWebViewNavigationCompleted;
                    Control.ScriptNotify += OnWebViewScriptNotify;

                    if (((HybridWebView)Element).Uri.Contains("Navitas\\")) //must be in our public directory
                    {
                        string filename = Path.GetFileName(((HybridWebView)Element).Uri);
                        Control.Source = new Uri($"ms-appdata:///local/Navitas/{filename}");
                    }
                    else
                        Control.Source = new Uri($"ms-appx-web:///Content//{((HybridWebView)Element).Uri}");

                    if (e.NewElement is HybridWebView webView)
                    {
                        webView.EvaluateJavascript = async (js) =>
                        {
                            return await Control.InvokeScriptAsync("eval", new[] { js });
                        };
                        webView.InjectJavascript = async (js) =>
                        {
                            try
                            {
                                await Control.InvokeScriptAsync("eval", new[] { js });
                                return "Injected";
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine("whaaa");
                                return "Injected";
                            }
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UWP Webview Exeption" + ex.Message);
            }
        }

        async void OnWebViewNavigationCompleted(Windows.UI.Xaml.Controls.WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
            {
                // Inject JS script
                await Control.InvokeScriptAsync("eval", new[] { JavaScriptFunction });
            }
        }

        void OnWebViewScriptNotify(object sender, NotifyEventArgs e)
        {
            ((HybridWebView)Element).InvokeAction(e.Value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((HybridWebView)Element).Cleanup();
            }
            base.Dispose(disposing);
        }        
    }
}