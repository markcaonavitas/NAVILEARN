using System.IO;
using NavitasBeta;
using NavitasBeta.iOS;
using Foundation;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Threading.Tasks;
using UIKit;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace NavitasBeta.iOS
{
    public class HybridWebViewRenderer : WkWebViewRenderer, IWKScriptMessageHandler
    {
        const string JavaScriptFunction = "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
        WKUserContentController userController;

        public HybridWebViewRenderer() : this(new WKWebViewConfiguration())
        {
        }

        public HybridWebViewRenderer(WKWebViewConfiguration config) : base(config)
        {
            userController = config.UserContentController;
            var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
            userController.AddUserScript(script);
            userController.AddScriptMessageHandler(this, "invokeAction");
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                userController.RemoveAllUserScripts();
                userController.RemoveScriptMessageHandler("invokeAction");
                HybridWebView hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }
            if (e.NewElement != null)
            {
                Opaque = false;
                BackgroundColor = UIColor.Clear;
                string filename;
                if (((HybridWebView)Element).Uri.Contains("Document"))
                    filename = Path.Combine("", $"{((HybridWebView)Element).Uri}"); //get from external App directory
                else
                    filename = Path.Combine(NSBundle.MainBundle.BundlePath, $"Content/{((HybridWebView)Element).Uri}"); //embedded directory

                WKNavigation debug = LoadRequest(new NSUrlRequest(new NSUrl(filename, false)));
                var webView = e.NewElement as HybridWebView;
                if (webView != null)
                {
                    webView.EvaluateJavascript = async (js) =>
                    {
                        NSObject ejs = await EvaluateJavaScriptAsync(js);
                        return ejs.ToString();
                    };
                    webView.InjectJavascript = async (js) =>
                    {
                        await EvaluateJavaScriptAsync(js);
                        return "";
                    };
                }

            }
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
                {
            ((HybridWebView)Element).InvokeAction(message.Body.ToString());
                }

        protected override void Dispose(bool disposing)
                {
            if (disposing)
                    {
                ((HybridWebView)Element).Cleanup();
        }
            base.Dispose(disposing);
        }
        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);
        //    if (e.PropertyName == HybridWebView.UriProperty.PropertyName)
        //    {
        //        if (Control.Url != null)
        //        {
        //            string[] fileList = FileManager.GetNavitasDirectoryFiles();
        //            bool fileAlreadyLoaded = false;
        //            foreach (string localPublicFile in fileList)
        //                if (localPublicFile.Contains(Element.Uri.Replace("?", "")) && !fileAlreadyLoaded)
        //                {
        //                    Control.LoadUrl($"file:///{localPublicFile}");
        //                    fileAlreadyLoaded = true;
        //                }

        //            if (!fileAlreadyLoaded)
        //                Control.LoadUrl($"file:///android_asset/Content/{Element.Uri}");
        //        }
        //    }
        //}
    }
}
