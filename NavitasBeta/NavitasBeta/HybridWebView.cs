using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace NavitasBeta
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]//junk
    public class HybridWebView : WebView
	{
		Action<string> action;
        public HybridWebViewCommunication communications;
        NavitasGeneralPage parentPage;

        public HybridWebView(NavitasGeneralPage parentPage)
        {
            this.parentPage = parentPage;
            SetBinding(HybridWebView.EvaluateJavascriptProperty, new Binding("EvaluateJavascript"));
            this.communications = new HybridWebViewCommunication(parentPage, this);
         }
        public static BindableProperty EvaluateJavascriptProperty = BindableProperty.Create(
            nameof(EvaluateJavascript),
            typeof(Func<string, Task<string>>),
            typeof(HybridWebView),
            null,
            BindingMode.OneWayToSource);

        public static BindableProperty InjectJavascriptProperty = BindableProperty.Create(
            nameof(InjectJavascript),
            typeof(Func<string, Task<string>>),
            typeof(HybridWebView),
            null,
            BindingMode.OneWayToSource);

        public Func<string, Task<string>> EvaluateJavascript
        {
            get { return (Func<string, Task<string>>)GetValue(EvaluateJavascriptProperty); }
            set { SetValue(EvaluateJavascriptProperty, value); }
        }

        public Func<string, Task<string>> InjectJavascript
        {
            get { return (Func<string, Task<string>>)GetValue(InjectJavascriptProperty); }
            set { SetValue(InjectJavascriptProperty, value); }
        }

        public static readonly BindableProperty UriProperty = BindableProperty.Create (
			propertyName: "Uri",
			returnType: typeof(string),
			declaringType: typeof(HybridWebView),
			defaultValue: default(string));
		
		public string Uri {
			get { return (string)GetValue (UriProperty); }
			set { SetValue (UriProperty, value); }
		}

		public void RegisterAction (Action<string> callback)
		{
			action = callback;
		}

		public void Cleanup ()
		{
			action = null;
		}

		public void InvokeAction (string data)
		{
			if (action == null || data == null) {
				return;
			}
			action.Invoke (data);
		}
	}
}
