using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using System.IO;

namespace NavitasBeta
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public class ScriptingPage : DynamicPage
    {
        string savedUri = "";
        bool isReadOnce = false;

        public ScriptingPage(string fileName, bool isModalPage = false, bool isTabbedPage = false) : base (fileName, isTabbedPage)
        {
            
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);

                string editedString = Path.GetFileName(fileName);

                editedString = Regex.Replace(editedString, "Scripting", "", RegexOptions.IgnoreCase); // fileName.Replace("Scripting", "").Replace("Diagnostics","");
                editedString = Regex.Replace(editedString, "Diagnostics", "", RegexOptions.IgnoreCase);
                editedString = Regex.Replace(editedString, ".html", "", RegexOptions.IgnoreCase);
                //CamelCase to Separate Words
                Title = Regex.Replace(editedString, "(?<=[a-z])([A-Z])", " $1", RegexOptions.None);

                PublicActivityIndicator = new ActivityIndicator
                {
                    IsRunning = false
                };

                htmlFilePathAndName = FileManager.FindPublicFirstOrEmbeddedFilePathAndName(fileName);

                if (htmlFilePathAndName.Length > 0)
                    hybridWebView = LoadHTMLPage(htmlFilePathAndName);
                else
                    return;  //no file found

                Padding = new Thickness(0, 0, 0, 0);
                BackgroundColor = Color.Transparent; //let the html page set it
                                                     //Opacity = 0;
                hybridWebView.VerticalOptions = LayoutOptions.Fill;
                hybridWebView.HorizontalOptions = LayoutOptions.Fill;
                AbsoluteLayout.SetLayoutFlags(hybridWebView, AbsoluteLayoutFlags.All);
                if (isModalPage)
                {
                    //InputTransparent = true;
                    AbsoluteLayout.SetLayoutBounds(hybridWebView, new Rectangle(0, 0, 1, 1)); // AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

                    Content = new AbsoluteLayout
                    {
                        Children = { hybridWebView }
                    };
                }
                else if(isTabbedPage)
                {
                    AbsoluteLayout.SetLayoutBounds(hybridWebView, Device.Idiom == TargetIdiom.Phone ? new Xamarin.Forms.Rectangle(0, 1, 1, 0.88f) : new Xamarin.Forms.Rectangle(0, 1, 1, 0.94f));
                    (this.Content as AbsoluteLayout).Children.Add(hybridWebView);
                    BuildErrorIcon();
                }
                else // Navigation page
                {
                    AbsoluteLayout.SetLayoutBounds(hybridWebView, Device.Idiom == TargetIdiom.Phone ? new Xamarin.Forms.Rectangle(0, 1, 1, 0.88f) : new Xamarin.Forms.Rectangle(0, 1, 1, 0.94f));
                    (this.Content as AbsoluteLayout).Children.Add(hybridWebView);
                    BuildErrorIcon();
                }

                AddActivityPopUp();
                FileManager.checkLiveReload(htmlFilePathAndName, PageliveReloadTime, hybridWebView);
            }
            catch (Exception e)
            {
                DisplayAlert(Title + " scripting page error: ",   e.ToString(), "Close");
            }
        }

        /// <summary>
        /// //////////////////////////////////////////////////////refactor to class
        /// </summary>
        /// <returns></returns>
        /// 

        protected async override void OnAppearing()
        {
            //Watch all this, iOS can appear (call this) after the Javascript stuff has started
            //.Active stuff would disable the javascript continuous read stuff
            //Starting and stopping comms on all pages is a bit confusing
            //And as mentioned else where, timed queues for reading word work easier?
            System.Diagnostics.Debug.WriteLine("Script screen appearing");
            if (PageParameters != null) //some pages like the scripting page don't always keep this initiated
                PageParameters.Active = true;

            if (savedUri != "")
            {
                hybridWebView.Uri = savedUri;
                savedUri = "";
            }

            //base.OnAppearing();
            //foreach (var x in DeviceComunication.PageCommunicationsListPointer)
            //{
            //    if(htmlPageIndex )
            //        x.parentPage.Active = false;
            //}

            //if (!AlreadyAppeared)
            //{
            //    AlreadyAppeared = true;
            //}

            FileManager.checkLiveReload(htmlFilePathAndName, PageliveReloadTime, hybridWebView);
        }

        protected async override void OnDisappearing()
        {
            savedUri = hybridWebView.Uri;
            //hybridWebView.Cleanup();
            System.Diagnostics.Debug.WriteLine("Script screen disappearing");
            await hybridWebView.communications.StopContinuousReadsAndTimer();
            System.Diagnostics.Debug.WriteLine("Script screen disappeared");
            base.OnDisappearing();
            //hybridWebView.Uri = ""; Android is having problems with this, but it keeps running without which we are living with temporarily
        }
    }
}