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

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace NavitasBeta.UWP
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {

        //DataTemplate originalTemplate;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            Element.PropertyChanged += Element_PropertyChanged;

            //originalTemplate = Control.HeaderTemplate;

        }

        private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsHidden")
            {
                if ((Element as CustomTabbedPage).IsHidden)
                {
                    
                    //Windows.UI.Xaml.DataTemplate template = App.Current.Resources["MyDataTemplate"] as Windows.UI.Xaml.DataTemplate;
                    //Control.HeaderTemplate = template;
                    Control.IsEnabled = false;
                }
                else
                {
                    //Control.HeaderTemplate = originalTemplate;
                    Control.IsEnabled = true;
                }
            }
        }
    }
}
