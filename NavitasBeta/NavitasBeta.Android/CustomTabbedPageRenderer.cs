using System;
using System.ComponentModel;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Views;
using NavitasBeta;
using NavitasBeta.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace NavitasBeta.Droid
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        private int TabBarHeight;

        public CustomTabbedPageRenderer(Context context) : base(context) { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            ShowOrHideBottomNavBar((Element as CustomTabbedPage).IsHidden);
        }

        private void ShowOrHideBottomNavBar(bool hide)
        {
            for (int i = 0; i <= this.ViewGroup.ChildCount - 1; i++)
            {
                var childView = this.ViewGroup.GetChildAt(i);
                if (childView is ViewGroup viewGroup)
                {
                    for (int j = 0; j <= viewGroup.ChildCount - 1; j++)
                    {
                        var childRelativeLayoutView = viewGroup.GetChildAt(j);
                        if (childRelativeLayoutView is BottomNavigationView)
                        {
                            if (((BottomNavigationView)childRelativeLayoutView).LayoutParameters.Height != 0) TabBarHeight = ((BottomNavigationView)childRelativeLayoutView).LayoutParameters.Height;

                            var parameters = ((BottomNavigationView)childRelativeLayoutView).LayoutParameters;
                            parameters.Height = hide ? 0 : TabBarHeight;

                            ((BottomNavigationView)childRelativeLayoutView).LayoutParameters = parameters;
                        }
                    }
                }
            }
        }
    }
}