using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
//using Syncfusion.SfNumericUpDown.XForms.iOS;

//to expose DoneEntry
using CoreGraphics;

//to expose CGRect
using System.Reflection;
using Xamarin.Forms.Platform.iOS;
using NavitasBeta.iOS;
using NavitasBeta;
[assembly: ExportRenderer(typeof(DoneEntry), typeof(DoneEntryRenderer))]


namespace NavitasBeta.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            DependencyService.Register<IInfoService, InfoService>();
            DependencyService.Register<IAdapter, Adapter>();
            DependencyService.Register<ParseManager.IParseClientManager, ParseManager.iOS.ParseClientManager>();

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY3NDUwQDMxMzcyZTMzMmUzMFl4dEQ2SXRIL0JMVVNBaWdiTkR3ck5RbTJrMEdxQVpTQXlHZEtkN1YycUE9");
            global::Xamarin.Forms.Forms.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.Forms.Init();
            new Syncfusion.SfGauge.XForms.iOS.SfLinearGaugeRenderer();

            //If iOS 15 or above
            //black tab bar appearance was fixed by this configuration
            if (UIDevice.CurrentDevice.CheckSystemVersion(15, 0))
            {
                UITabBarAppearance a = new UITabBarAppearance();
                a.ConfigureWithOpaqueBackground();
                a.BackgroundColor = UIColor.White;

                UITabBar.Appearance.StandardAppearance = a;
                UITabBar.Appearance.ScrollEdgeAppearance = a;
            }

            //new SfNumericUpDownRenderer();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }


    public class DoneEntryRenderer : EntryRenderer
    {
        private MethodInfo baseEntrySendCompleted = null;




        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var toolbar = new UIToolbar(new CGRect(0.0f, 0.0f, Control.Frame.Size.Width, 44.0f));
                Control.BackgroundColor = UIColor.White; //hack so Dark mode background is not black with black text

                toolbar.Items = new[]
                {
            new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace),
            new UIBarButtonItem(UIBarButtonSystemItem.Done, delegate {
                Control.ResignFirstResponder();
                ((IEntryController)Element).SendCompleted();
            })
        };

                this.Control.InputAccessoryView = toolbar;
            }
        }
    }

}
