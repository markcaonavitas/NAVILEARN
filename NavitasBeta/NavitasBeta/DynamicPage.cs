using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OxyPlot;
using System.Reflection;
using Xamarin.Essentials;
using System.Xml.Linq;

namespace NavitasBeta
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public class DynamicPage : NavitasGeneralPage
    {
        //When working with UWP the local storage can be found at:
        //C:\Users\rgreen\AppData\Local\Packages\5f82c415-f5ba-4b36-b814-761e9d451aaf_va7gssv7xkj84\LocalState\Navitas
        //Users\your user name
        //this package number changes install to install but look for the latest one

        Image imgCompanyLogo = new Image
        {
            Source = "icon.png",
            Style = (Style)Application.Current.Resources["toolbarImageStyle"]
        };

        // Navigatescreen isRequiredProperToolbar = false
        public DynamicPage(string fileName, bool isRequiredStandardToolbar = true)
        {
            var elements = new List<View>();
            if (isRequiredStandardToolbar)
            {
                Image hamburgerContainer = new Image
                {
                    Source = "hamburger.png",
                    Margin = new Thickness(10, 0),
                    Scale = 1.5,
                    HorizontalOptions = LayoutOptions.Start
                };

                TapGestureRecognizer menuTapGestureRecognizer = new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1
                };

                menuTapGestureRecognizer.Tapped += ShowMasterMenu;
                hamburgerContainer.GestureRecognizers.Add(menuTapGestureRecognizer);

                elements = new List<View>() { hamburgerContainer, imgCompanyLogo };
            }
            else
            {
                // required navigation bar
                NavigationPage.SetHasNavigationBar(this, false);
                var FileNameOnly = Path.GetFileNameWithoutExtension(fileName);
                FileNameOnly = FileNameOnly.Replace('_', ' ');

                Label lblTitle = new Label
                {
                    Text = FileNameOnly,
                    Style = (Style)Application.Current.Resources["titleStyle"]
                };
                Image imgBackButton = new Image
                {
                    Source = "backArrow.png",
                    Scale = .8,
                    Style = (Style)Application.Current.Resources["toolbarImageStyle"]
                };

                TapGestureRecognizer backBtnTapGestureRecognizer = new TapGestureRecognizer
                {
                    NumberOfTapsRequired = 1
                };

                backBtnTapGestureRecognizer.Tapped += BackbuttonClicked;
                imgBackButton.GestureRecognizers.Add(backBtnTapGestureRecognizer);

                elements = new List<View>() { imgBackButton, imgCompanyLogo, lblTitle };
            }

            try
            {
                if(fileName.Contains(".html"))
                {
                    Content = new AbsoluteLayout
                    {
                        Children = { BuildToolbar(elements) }
                    };
                }
                else
                {
                    PageViewModel pageDescriptions = FileManager.GetDeserializedObject<PageViewModel>(fileName);

                    Title = pageDescriptions.PageTitle;

                    //</ StackLayout >

                    ////////////////////////////////////////////////////////////body////////////////////////////////////////////////////////
                    //<ScrollView AbsoluteLayout.LayoutBounds="0, 1, 1, 0.88"
                    //  AbsoluteLayout.LayoutFlags="All">
                    ScrollView scrollView = new ScrollView
                    {
                        //Setting AbsoluteLayout.LayoutBounds by using app-level static resource
                        Style = (Style)Application.Current.Resources["scrollViewProportion"]
                    };

                    // from old xaml
                    //< StackLayout Orientation = "Horizontal" VerticalOptions = "Start" BackgroundColor = "#68B04D" Padding = "1" HorizontalOptions = "FillAndExpand"
                    //   AbsoluteLayout.LayoutBounds = "0, 0, 1, 0.12"
                    //   AbsoluteLayout.LayoutFlags = "All" >

                    Content = new AbsoluteLayout
                    {
                        Children = { BuildToolbar(elements), scrollView }
                    };

                    if (ControllerTypeLocator.ControllerType == "TAC")
                    {
                        PageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, this);
                        BuildCommunicationsList(PageParameters.parameterList, App.ViewModelLocator.GetParameter("GROUPONEFAULTS"));
                        BuildCommunicationsList(PageParameters.parameterList, App.ViewModelLocator.GetParameter("GROUPTWOFAULTS"));
                        BuildCommunicationsList(PageParameters.parameterList, App.ViewModelLocator.GetParameter("GROUPTHREEFAULTS"));
                        BuildCommunicationsList(PageParameters.parameterList, App.ViewModelLocator.GetParameter("GROUPFOURFAULTS"));
                        BuildCommunicationsList(PageParameters.parameterList, App.ViewModelLocator.GetParameter("GROUPONEWARNINGS"));
                    }
                    else
                    {
                        PageParameters = new PageParameterList(PageParameterList.ParameterListType.TSX, this);
                        BuildCommunicationsList(PageParameters.parameterList, App.ViewModelLocator.GetParameterTSX("PARSTARTUPERRORS"));
                        BuildCommunicationsList(PageParameters.parameterList, App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSLOW"));
                        BuildCommunicationsList(PageParameters.parameterList, App.ViewModelLocator.GetParameterTSX("PARRUNTIMEERRORSHIGH"));
                    }

                    StackLayout stacklayout = new StackLayout();

                    scrollView = BuildScrollView(stacklayout, scrollView, pageDescriptions);

                    if (fileName.Contains(".TAChelp") || fileName.Contains(".TSXhelp"))
                    {
                        FillInDescriptions(stacklayout.Children[0], new EventArgs());
                    }

                    foreach (var x in DeviceComunication.PageCommunicationsListPointer)
                    {
                        if (x.parentPage.ParentTitle == PageParameters.ParentTitle)
                        {
                            RemovePacketList(x.parentPage.uniqueID);
                        }
                    }
                    // We might miss to set active back to false
                    // When there are many places in the code attempt to make usertabbedpage modification
                    PageParameters.Active = false;

                    pageUniqueId = App._devicecommunication.AddToPacketList(PageParameters); // Set up things that need to read on this page

                    AddActivityPopUp();
                    BuildErrorIcon();
                    BuildDemoFlashing();
                    BuildDoneButtonWarningTextBox();
                }
            }
            catch (Exception e)
            {
                DisplayAlert("XML error: ", e.ToString(), "Close");
            }
        }

        private async void RemovePacketList(long pageUniqueId)
        {

            //wait for  it to be sent and removed
            while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                await Task.Delay(10);
            App._devicecommunication.SetUniqueIdToBeRemoved(pageUniqueId);

            //wait for  it to be sent and removed
            while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                await Task.Delay(10);
        }


        ///////////////////////////////////////////////////custom tool bar//////////////////////////////////////////////////////
        public StackLayout BuildToolbar(List<View> toolbarElements)
        {
            // from old xaml
            //< StackLayout Orientation = "Horizontal" HorizontalOptions = "Start" >
            //       < StackLayout.GestureRecognizers >
            //           < TapGestureRecognizer Tapped = "ShowMasterMenu" NumberOfTapsRequired = "1" />
            //        </ StackLayout.GestureRecognizers >
            //        < Image Source = "hamburger.png" Margin = "10,0,0,0" Scale = "1.5" HorizontalOptions = "Start" />
            //        < Image Source = "icon.png" Margin = "0" Scale = ".6" HorizontalOptions = "Start" />
            //</ StackLayout >

            StackLayout toolBarContainer = new StackLayout
            {
                // If we want to use absolute and screen proportion
                // should not include this layout option
                //VerticalOptions = LayoutOptions.Start,
                Style = (Style)Application.Current.Resources["toolbarStyle"]
            };

            //from old xaml
            //< StackLayout Orientation = "Horizontal" HorizontalOptions = "CenterAndExpand" >
            //  < local:CustomImage x:Name = "CONTROLLERERROR" Scale = ".50" Opacity = "{Binding Path=parameterValue}" HorizontalOptions = "CenterAndExpand" Source = "IconError.png" />
            //  < Image x: Name = "COMMERROR" Scale = ".8" Opacity = "{Binding Path=parameterValue}" HorizontalOptions = "Center" Source = "IconCommunication.png" />
            //  < Label x: Name = "GROUPONEFAULTS" Text = "Used for controller error display" IsVisible = "False" />
            //  < Label x: Name = "GROUPTWOFAULTS" Text = "Used for controller error display" IsVisible = "False" />
            //  < Label x: Name = "GROUPTHREEFAULTS" Text = "Used for controller error display" IsVisible = "False" />
            //  < Label x: Name = "GROUPFOURFAULTS" Text = "Used for controller error display" IsVisible = "False" />
            //  < Label x: Name = "VdcBusHalfBridgeTestResult_puq7" Text = "Used for controller error display" IsVisible = "False" />
            //  < Label x: Name = "PhaseAHalfBridgeTestResult_puq7" Text = "Used for controller error display" IsVisible = "False" />
            //  < Label x: Name = "PhaseBHalfBridgeTestResult_puq7" Text = "Used for controller error display" IsVisible = "False" />
            //  < Label x: Name = "PhaseCHalfBridgeTestResult_puq7" Text = "Used for controller error display" IsVisible = "False" />
            //</ StackLayout >

            StackLayout controllerAndComErrorContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout toolbarElementsContainer = new StackLayout
            {
                Style = (Style)Application.Current.Resources["toolbarElementsContainerStyle"]
            };

            foreach (var element in toolbarElements)
            {
                toolbarElementsContainer.Children.Add(element);
            }

            toolBarContainer.Children.Add(toolbarElementsContainer);
            toolBarContainer.Children.Add(controllerAndComErrorContainer);

            CustomImage controllerError = new CustomImage
            {
                Source = "IconError.png",
                Scale = 0.5,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Image commError = new Image
            {
                Source = "IconCommunication.png",
                Scale = .8,
                HorizontalOptions = LayoutOptions.Center
            };

            if (ControllerTypeLocator.ControllerType == "TAC")
            {
                controllerError.ParentAction += TACDisplayFaultMessage;
                controllerError.BindingContext = App.ViewModelLocator.GetParameter("CONTROLLERERROR");
                commError.BindingContext = App.ViewModelLocator.GetParameter("COMMERROR");
            }
            else
            {
                controllerError.ParentAction += TSXDisplayFaultMessage;
                controllerError.BindingContext = App.ViewModelLocator.GetParameterTSX("CONTROLLERERROR");
                commError.BindingContext = App.ViewModelLocator.GetParameterTSX("COMMERROR");
            }

            controllerError.SetBinding(CustomImage.OpacityProperty, "parameterValue", BindingMode.OneWay);
            commError.SetBinding(CustomImage.OpacityProperty, "parameterValue", BindingMode.OneWay);

            controllerAndComErrorContainer.Children.Add(controllerError);
            controllerAndComErrorContainer.Children.Add(commError);

            return toolBarContainer;
        }

        protected override void OnAppearing()
        {
            //Gauge page is index 0, normally it is always active any communication because it has the most general
            //registers like faults and stuff which could be used in the future for a general activity bar
            //for these "engineering" type pages, turn it off so that only this page is communicating and hence faster communications
            PageParameters.Active = true;
            base.OnAppearing(); //this will enable this pages communications like normal
        }
        private async void BackbuttonClicked(object sender, EventArgs e)
        {
            Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
            if (hybridWebView != null)
            {
                await hybridWebView.communications.StopContinuousReadsAndTimer();

                // We could handle this better in the derived class if this method is not an eventhandler delegate
                if (this.Title == "Calibrate Throttle")
                {
                    //Restore the vehicle's state to earlier
                    QueParameter(new SetParameterEventArgs(App.ViewModelLocator.GetParameter("VEHICLELOCKED").Address,
                                                          (int)App.ViewModelLocator.GetParameter("VEHICLELOCKED").parameterValue,
                                                          "VEHICLELOCKED"));
                }
            }
            else
            {
                while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                    await Task.Delay(10); //TODO:Flag this as an issue if it happens
                App._devicecommunication.SetUniqueIdToBeRemoved(pageUniqueId); //still not getting object reference required for non static.....
                while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                    await Task.Delay(50);
            }

            await Navigation.PopAsync();
        }
    }
}