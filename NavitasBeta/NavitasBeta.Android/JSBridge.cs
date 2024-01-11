using System;
using Android.Webkit;
using Java.Interop;

namespace NavitasBeta.Droid
{
    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<HybridWebViewRenderer> hybridWebViewRenderer;

        public JSBridge(HybridWebViewRenderer hybridRenderer)
        {
            hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            HybridWebViewRenderer hybridRenderer;

            if (hybridWebViewRenderer != null && hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                if (hybridRenderer.Element != null) //it can be null and I'm not sure why, some timing between closing and stop calling
                    hybridRenderer.Element.InvokeAction(data);
            }
        }
    }
}

