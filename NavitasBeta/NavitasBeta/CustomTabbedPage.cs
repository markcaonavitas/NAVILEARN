using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class CustomTabbedPage : Xamarin.Forms.TabbedPage
    {
        
        public static readonly BindableProperty IsHiddenProperty =
            BindableProperty.Create(nameof(IsHidden), typeof(bool), typeof(CustomTabbedPage), false);

        public bool IsHidden
        {
            get { return (bool)GetValue(IsHiddenProperty); }
            set { SetValue(IsHiddenProperty, value); }
        }

        private static SemaphoreSlim slim = new SemaphoreSlim(1, 1);

        public async Task<bool> ManipulateTabbedPage(string action, NavitasGeneralPage generalPage, int indexToChange = -1)
        {
            //string pageTitle = generalPage.PageParameters.ParentTitle;
            await slim.WaitAsync();
            //Debug.WriteLine("Page {0} - Start1", pageTitle);
            await Device.InvokeOnMainThreadAsync(() =>
            {
                switch (action)
                {
                    case "Add":
                        //Debug.WriteLine("{0}: {1}", action, pageTitle);
                        this.Children.Add(generalPage);
                        break;
                    case "Replace":
                        //Debug.WriteLine("{0}: {1}", action, pageTitle);
                        this.Children[indexToChange] = generalPage;
                        break;
                    default:
                        Debug.WriteLine("Invalid action!");
                        break;
                }
            });

            //Debug.WriteLine("Page {0} - End1", pageTitle);
            slim.Release();
            return true;
        }
    }
}