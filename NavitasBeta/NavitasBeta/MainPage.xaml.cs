using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

using System.IO;
using System.Xml.Serialization;
using Xamarin.Essentials;
using OxyPlot;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.Reflection;
using Newtonsoft.Json;
using System.Threading;
using Xamarin.Forms.Internals;

namespace NavitasBeta
{
    public partial class MainFlyoutPage : FlyoutPage
    {
        public UserTabbedPage userTabbedPage;
        UserTabbedPage userTabbedPageTSX;
        public UserTabbedPage userTabbedPageDisplayed;
        public ScopePage ScopePage; //Remeber this is just temporary, we don't want device communications to have to know about this

        public DeviceListPage _DeviceListPage;
        ExpireNotificationPage _ExpireNotificationPage;

        public static DealerSettingsTSXPage DealerSettingsTSXPage;
        public static DealerSettingsPage DealerSettingsPage;

        public ParameterFileHandler ParameterFileHandler; //Remeber this is just temporary, we don't want device communications to have to know about this

        public Dictionary<string, IList<string>> listOfSerialNumbersApprovials;

        public MainFlyoutPage()
        {
            InitializeComponent();
            //RG Oct 2020: Moved this here with future concept of things like:
            //Only Dealers or Engs access can connect to vehicles listed in DeviceListPage?
            _DeviceListPage = new DeviceListPage();
            _ExpireNotificationPage = new ExpireNotificationPage();
            masterPage.ListView.ItemSelected += async (sender, args) => await OnItemSelected(sender, args);
            userTabbedPage = new UserTabbedPage();
            userTabbedPageTSX = new UserTabbedPage();

            BackgroundColor = Color.Transparent;
            _DeviceListPage.BackgroundColor = Color.Transparent;



            FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;

            messagingCenterHandling();

            Gauge GaugePage = new Gauge();
            userTabbedPage.Children.Add(GaugePage);
            userTabbedPage.CurrentPageChanged += PageChanged;
            userTabbedPage.CurrentPage = GaugePage;

            GaugeTSX GaugeTSXPage = new GaugeTSX();
            userTabbedPageTSX.Children.Add(GaugeTSXPage);
            userTabbedPageTSX.CurrentPageChanged += PageChanged;
            userTabbedPageTSX.CurrentPage = GaugeTSXPage;

            userTabbedPageDisplayed = null;

            //Add more startup logic here if we don't want to force people to select a device (future auto connect and things)
            Detail.Navigation.PushAsync(_DeviceListPage);
#if (DEALER)
            if (Device.RuntimePlatform == Device.iOS)
            {
                Detail.Navigation.PushModalAsync(_ExpireNotificationPage);
            }
            else
            {
                DisplayAlert("NOTICE!", "The beta period of this app has ended. \n" +
                    "Download the official Navitas Dashboard directly through the Google Play Store (Search for “Navitas Vehicle Systems”)\n\n" +
                    "User ID will be required for advanced options", "OK");
            }
#endif

        }

        async Task OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is MasterPageItem item)
            {
                if (item.Title == "Dash Board")
                {
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                }
                else if (item.Title == "Communications")
                {
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    await Detail.Navigation.PushAsync(_DeviceListPage);
                }
                else if (item.Title == "Exit Demo")
                {
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    App._MainFlyoutPage.ModifyMasterPageItems("Exit Demo", "Communications");
                    await Detail.Navigation.PushAsync(_DeviceListPage);
                }
                else if (item.Title == "Controller Firmware Download")
                {
                    if (App._MainFlyoutPage.UserHasWriteCredentials())
                    {
                        BootloaderDetermined(false);
                    }
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                }
                else if (item.Title == "View Engineering Tabs")
                {
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    if (!userTabbedPageDisplayed.EngTabsInitialized)
                        await InitEngTabs();
                }
                //else if (item.Title == "Parameter File Handling")
                //{
                //    masterPage.ListView.SelectedItem = null;
                //    IsPresented = false;
                //    ParameterFilePage = new ParameterFilePage();
                //    await Detail.Navigation.PushAsync(ParameterFilePage);
                //}
                else if (item.Title == "Login")
                {
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    await Detail.Navigation.PushAsync(new LoginPage());
                }

                else if (item.Title == "Check for updates")
                {
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    //concept depricated because it is automatic await DownloadUpdates();
                }
                else if (item.Title == "My Account")
                {
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    await Detail.Navigation.PushAsync(new MyAccountPage());
                }
                else
                {
                    //Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                    //NavigationPage.SetHasBackButton(Detail, true);
                    //NavigationPage.SetHasNavigationBar(Detail, true);
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;

                    if (item.Title.Contains("html"))
                    {
                        if (userTabbedPageDisplayed != Detail.Navigation.NavigationStack.First())
                            Detail.Navigation.InsertPageBefore(userTabbedPageDisplayed, Detail.Navigation.NavigationStack.First());

                        await Detail.Navigation.PopAsync();
                        await Navigation.PushModalAsync(new ScriptingPage("UserOnboarding.html", true)); // item.FileName, true););
                    }
                    else
                    {
                        var newPage = (Page)Activator.CreateInstance(typeof(DynamicPage), item.FileName, false);
                        newPage.BackgroundColor = Color.Default;
                        await Detail.Navigation.PushAsync(newPage);
                    }
                }
            }
        }

        public void PageChanged(object sender, EventArgs args)
        {
            var currentPage = userTabbedPageDisplayed.CurrentPage as ContentPage;
            var index = 0;
            foreach (var x in DeviceComunication.PageCommunicationsListPointer)
            {
                if (index++ != 0) //A Gauge Page
                    x.parentPage.Active = false;
            }
            if (currentPage != null)
            {
                if ((currentPage as NavitasGeneralPage).PageParameters != null) //scripting pages don't use this
                    (currentPage as NavitasGeneralPage).PageParameters.Active = true;
                //if (userTabbedPageDisplayed.Children.IndexOf(currentPage) != 0) //A Guage Page
                //(userTabbedPageDisplayed.Children[0] as NavitasGeneralPage).Disappeared = true;
            }

        }

        void messagingCenterHandling()
        {
            MessagingCenter.Subscribe<DeviceListPage>(this, "DisplayDemoAlert", (sender) =>
            {
                BeginInvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Demo Mode Activated", "You are not connected to a real controller", "OK");
                });
            });


            MessagingCenter.Subscribe<DeviceListPage>(this, "CloseDeviceList", (sender) =>
            {
                BeginInvokeOnMainThreadAsync(async () =>
                {
                    if (Detail.Navigation.NavigationStack.Count > 1)//only this popup
                        if (userTabbedPageDisplayed == Detail.Navigation.NavigationStack.First())
                            await Detail.Navigation.PopToRootAsync();
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                    IsGestureEnabled = true; //it is set to false in _DeviceListPage
                });
            });

            MessagingCenter.Subscribe<DeviceListPage>(this, "ShowDeviceList", (sender) =>
            {
                BeginInvokeOnMainThreadAsync(async () =>
                {
                    if (Detail.Navigation.NavigationStack.Count <= 1 &&
                        !(Detail.Navigation.NavigationStack.First() is DeviceListPage)) //nothing showing, must be empty
                    {
                        await Detail.Navigation.PushAsync(_DeviceListPage);
                    }
 
                    masterPage.ListView.SelectedItem = null;
                    IsPresented = false;
                });
            });

            MessagingCenter.Subscribe<DeviceComunication>(this, "ShowActivity", (sender) =>
            {// only called when saving command is issued
                BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Saving...";
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = true;
                    if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                    {
                        //LIFO is the only game in town! - so send back the last page

                        int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;

                        (Application.Current.MainPage.Navigation.NavigationStack[index] as NavitasGeneralPage).PageIsBusy = true;
                    }
                });
            });

            MessagingCenter.Subscribe<DeviceComunication>(this, "StopActivity", (sender) =>
            {
                BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = false;
                    if (Application.Current.MainPage.Navigation.NavigationStack.Count > 0)
                    {
                        //LIFO is the only game in town! - so send back the last page

                        int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;

                        (Application.Current.MainPage.Navigation.NavigationStack[index] as NavitasGeneralPage).PageIsBusy = false;
                    }
                });
            });
        https://law.stackexchange.com/questions/91205/are-there-potential-legal-considerations-in-the-u-s-when-two-people-work-from-t
            MessagingCenter.Subscribe<DeviceComunication>(this, "SettingsSaved", (sender) =>
            {
                //After pressing save button, this message should popup but gauge page
                if (userTabbedPageDisplayed.CurrentPage == null)
                {
                    BeginInvokeOnMainThreadAsync(() =>
                    {
                        DisplayAlert("Settings have been saved", "Cycle key switch OFF then ON to enable your changes", "Ok");
                    });
                }
                else
                {
                    if (!userTabbedPageDisplayed.CurrentPage.Title.Contains("Dashboard"))
                        BeginInvokeOnMainThreadAsync(() =>
                        {
                            DisplayAlert("Settings have been saved", "Cycle key switch OFF then ON to enable your changes", "Ok");
                        });
                }
            });

            MessagingCenter.Subscribe<NavitasGeneralPage>(this, "ShowActivity", (sender) =>
            {
                BeginInvokeOnMainThreadAsync(() =>
                {
                    //not sure why userTabbedPageDisplayed.CurrentPage can't use this technique
                    (Detail.Navigation.NavigationStack.Last() as NavitasGeneralPage).PageIsBusy = true;
                });
            });

            MessagingCenter.Subscribe<NavitasGeneralPage>(this, "StopActivity", (sender) =>
            {
                BeginInvokeOnMainThreadAsync(() =>
                {
                    (Detail.Navigation.NavigationStack.Last() as NavitasGeneralPage).PageIsBusy = false;
                });
            });

            MessagingCenter.Subscribe<DeviceListPage>(this, "ShowTabPage", async (sender) =>
            {
                SetupMenusAndTabs();
            });
            MessagingCenter.Subscribe<LoginPage>(this, "ShowTabPage", async (sender) =>
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                try
                {
                    cts.CancelAfter(TimeSpan.FromMilliseconds(5000));
                    // Send Cancellation Token down to check for update could improve UI blocking unpredictable
                    await CheckForUpdates(cts.Token);
                }
                catch (OperationCanceledException oce)
                {
                    System.Diagnostics.Debug.WriteLine("MessagingCenter LoginPage Exception: " + oce.Message);
                    userTabbedPageDisplayed.DisplayAlert("Connection Error", "Intermittent internet connection occured", "Ok");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("MessagingCenter LoginPage Exception: " + ex.Message);
                }
                finally
                {
                    cts.Dispose();
                    await BeginInvokeOnMainThreadAsync(() =>
                    {
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = false;
                    });
                    //Once tab bar constructs pages, pages removing option can not be executed when you re-login BUT replacement
                    //Close the app and reopen will solve this issue
                    FileManager.AddLocalPublicFiles(userTabbedPageDisplayed);
                    SetupMenusAndTabs();
                    if (!Authentication.GetUserCredentials()["ListOfRegisterControllers"].Contains(App.PresentConnectedController) &&
                    !(App.AppConfigurationLevel == "DEALER") &&
                    !(App.AppConfigurationLevel == "ENG") &&
                    !(App.AppConfigurationLevel == "DEV"))
                    {
                        if (!Authentication.CheckUserWasShownVehicleOperatorVerification())
                        {
                            await _DeviceListPage.VerifyRegisteredUserChanged();
                            SecuredVehicle();
                        }
                    }
                    //Doing this hack will check for member approval and replace default screens if necessary
                    SetDirtyBitToSoftwareRevision();
                }
            });
            MessagingCenter.Subscribe<SignUpPage>(this, "ShowTabPage", (sender) =>
            {
                SetupMenusAndTabs();
                if (!Authentication.GetUserCredentials()["ListOfRegisterControllers"].Contains(App.PresentConnectedController) &&
                !(App.AppConfigurationLevel == "DEALER") &&
                !(App.AppConfigurationLevel == "ENG") &&
                !(App.AppConfigurationLevel == "DEV"))
                {
                    if (!Authentication.CheckUserWasShownVehicleOperatorVerification())
                        SecuredVehicle();
                }
            });
            MessagingCenter.Subscribe<AdvancedRequestAccessPage>(this, "ShowTabPage", (sender) =>
            {
                SetupMenusAndTabs();
                if (!Authentication.GetUserCredentials()["ListOfRegisterControllers"].Contains(App.PresentConnectedController) &&
                !(App.AppConfigurationLevel == "DEALER") &&
                !(App.AppConfigurationLevel == "ENG") &&
                !(App.AppConfigurationLevel == "DEV"))
                    if (!Authentication.CheckUserWasShownVehicleOperatorVerification())
                        SecuredVehicle();
            });

            MessagingCenter.Subscribe<NavitasGeneralPage, string>(this, "PinToTab", (sender, fileName) =>
            {
                FileManager.AddHelperTab(fileName, userTabbedPageDisplayed);
            });

            MessagingCenter.Subscribe<MyAccountPage>(this, "ShowGaugePage", async (sender) =>
            {
                if (userTabbedPageDisplayed != Detail.Navigation.NavigationStack.First())
                    Detail.Navigation.InsertPageBefore(userTabbedPageDisplayed, Detail.Navigation.NavigationStack.First());

                await Detail.Navigation.PopAsync();
                Navigation.PushModalAsync(new ScriptingPage("PnrdVerification.html", true));
            });
        }

        public static Task BeginInvokeOnMainThreadAsync(Action action)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        async public void BootloaderDetermined(bool alreadyInBootMode)
        {
            await BeginInvokeOnMainThreadAsync(async () =>
            {
                if (ControllerTypeLocator.ControllerType == "TSX")
                {
                    if (alreadyInBootMode)
                    {
                        await GetLastestFirmwareAndSupplierChainWhileInBootmode();
                    }

                    FirmwareDownloadTSXPage firmwareDownloadPage = new FirmwareDownloadTSXPage(alreadyInBootMode);
                    firmwareDownloadPage.WritePacket += App._devicecommunication.WritePacket;
                    firmwareDownloadPage.ProgrammingDone += App._devicecommunication.ProgrammingDoneTSX;
                    firmwareDownloadPage.ClearQueue += App._devicecommunication.ClearQueue;
                    App._devicecommunication.SetFirmwareDownloadPageRef(firmwareDownloadPage);

                    //Detail.Navigation.InsertPageBefore(firmwareDownloadPage, Detail.Navigation.NavigationStack.First());
                    //await Detail.Navigation.PopToRootAsync();
                    await Detail.Navigation.PushAsync(firmwareDownloadPage);
                    if (alreadyInBootMode)
                        await DisplayAlert("Programming must have failed, restart the download", "You have been directed to the Download Page.", "Continue");

                    App._devicecommunication.bEnableCommunicationTransmissions = true;
                }
                else if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    if (alreadyInBootMode)
                    {
                        await GetLastestFirmwareAndSupplierChainWhileInBootmode();
                    }

                    FirmwareDownloadPage firmwareDownloadPage = new FirmwareDownloadPage(alreadyInBootMode);
                    firmwareDownloadPage.Write += App._devicecommunication.Write;
                    firmwareDownloadPage.Write20Bytes += App._devicecommunication.Write20Bytes;
                    firmwareDownloadPage.ProgrammingDone += App._devicecommunication.ProgrammingDone;
                    firmwareDownloadPage.WriteBlock += App._devicecommunication.WriteBlock;
                    App._devicecommunication.SetFirmwareDownloadPageRef(firmwareDownloadPage);

                    //Detail.Navigation.InsertPageBefore(firmwareDownloadPage, Detail.Navigation.NavigationStack.First());
                    //await Detail.Navigation.PopToRootAsync();
                    await Detail.Navigation.PushAsync(firmwareDownloadPage);
                    if (alreadyInBootMode)
                        await DisplayAlert("Programming must have failed, restart the download", "You have been directed to the Download Page.", "Continue");
                    App._devicecommunication.bEnableCommunicationTransmissions = true;
                }
            });
        }

        async Task GetLastestFirmwareAndSupplierChainWhileInBootmode()
        {
            InitialSC.SupplierChains = (ControllerTypeLocator.ControllerType == "TAC") ? App.ViewModelLocator.MainViewModel.SupplierChains :
                                                                                        App.ViewModelLocator.MainViewModelTSX.SupplierChains;
            List<string> userFileList = await GetFileListFromUser("FileList");
            await FileManager.ValidateAndDownloadFiles(userFileList);
            await FileManager.CheckSupplierChainUpdates();
        }

        async public Task Init()
        {
            System.Diagnostics.Debug.WriteLine("Init --Start");
            CancellationTokenSource cts = new CancellationTokenSource();
            try
            {
                await BeginInvokeOnMainThreadAsync(() =>
                    {
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Tabs";
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = true;
                    });

                if (!(userTabbedPageTSX.TabsInitialized || userTabbedPage.TabsInitialized))
                {
                    accessLevel = Authentication.GetUserCredentials()["AccessLevel"].ToLower();
                    App.ViewModelLocator.GetParameter("PARPROFILENUMBER").ReCalculate += NotifyPartProfileNumberChanged;
                    App.ViewModelLocator.GetParameterTSX("PARPROFILENUMBER").ReCalculate += NotifyPartProfileNumberChanged;
                }
                App._devicecommunication.bEnableCommunicationTransmissions = true;

                if (ControllerTypeLocator.ControllerType == "TSX")
                {

                    await BeginInvokeOnMainThreadAsync(() =>
                    {
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Diagnostics";
                        //UserDiagnosticsPageTSX UserDiagnosticsPageTSX = new UserDiagnosticsPageTSX();
                        //userTabbedPageTSX.Children.Add(UserDiagnosticsPageTSX);
                        userTabbedPageTSX.Children.Add(new DynamicPage("UserDiagnosticsPage.TSXscreen"));
                    });

                    await BeginInvokeOnMainThreadAsync(() =>
                    {
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Settings";
                        //DealerSettingsTSXPage = new DealerSettingsTSXPage();
                        //userTabbedPageTSX.Children.Add(DealerSettingsTSXPage);
                        userTabbedPageTSX.Children.Add(new DynamicPage("DealerSettingsPage.TSXscreen"));
                    });
                    userTabbedPage.SettingsTabsInitialized = true;
                    userTabbedPageTSX.TabsInitialized = true;
                }
                else if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    await BeginInvokeOnMainThreadAsync(() =>
                    {
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Diagnostics";
                        //UserDiagnosticsPage UserDiagnosticsPage = new UserDiagnosticsPage();
                        //userTabbedPage.Children.Add(UserDiagnosticsPage);
                        userTabbedPage.Children.Add(new DynamicPage("UserDiagnosticsPage.TACscreen"));
                    }
                    );
                    await BeginInvokeOnMainThreadAsync(() =>
                    {
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Settings";
                        //DealerSettingsPage = new DealerSettingsPage();
                        //userTabbedPage.Children.Add(DealerSettingsPage);
                        userTabbedPage.Children.Add(new DynamicPage("DealerSettingsPage.TACscreen"));
                    });
                    userTabbedPage.SettingsTabsInitialized = true;
                    userTabbedPage.TabsInitialized = true;
                }

                cts.CancelAfter(TimeSpan.FromMilliseconds(5000));
                // Send Cancellation Token down to check for update could improve UI blocking unpredictable
                await CheckForUpdates(cts.Token);
            }
            catch(OperationCanceledException oce)
            {
                System.Diagnostics.Debug.WriteLine("Init Exception: " + oce.Message);
                userTabbedPageDisplayed.DisplayAlert("Connection Error", "Intermittent internet connection occured", "Ok");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Init Exception: " + ex.Message);
            }
            finally
            {
                //WT Sep 2023, TODO: I'm not sure if this a right way to implemnt asynchronous thread
                FileManager.AddLocalPublicFiles(userTabbedPageDisplayed);
                FileManager.AddLocalNavigationPublicFiles(masterPage);
                FileManager.CheckSupplierChainUpdates();
                cts.Dispose();
                await BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = false;
                });
                await BeginInvokeOnMainThreadAsync(() =>
                {
                    App._devicecommunication.Init();
                }
                );
                var index = 0;
                foreach (var x in DeviceComunication.PageCommunicationsListPointer) //TAC and TSX gauge pages are always index 0
                {
                    if (index++ != 0)
                        x.parentPage.Active = false;
                    else
                        x.parentPage.Active = true;
                }

                if (Authentication.GetUserCredentials()["UserName"] == "testc5a3nFD43M")
                    App._MainFlyoutPage.UserHasWriteCredentials(); // user is not logged in so always popup login and operator verification
            }
            System.Diagnostics.Debug.WriteLine("Init --ENd");
        }
        string prevPartProfileNumber;
        public async void NotifyPartProfileNumberChanged(GoiParameter partProfileNumber)
        {
            string strPartProfileNumber = partProfileNumber.parameterValue.ToString();
            await BeginInvokeOnMainThreadAsync(() => UpdateTabbedPages(strPartProfileNumber, userTabbedPageDisplayed));
        }

        public async Task UpdateTabbedPages(string strPartProfileNumber, UserTabbedPage userTabbedPageDisplayed)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet &&
                prevPartProfileNumber != strPartProfileNumber)
            {
                prevPartProfileNumber = strPartProfileNumber;
                var objectType = "SupplyChainAccessObject";
                //Get approriate screen file's names
                var fileListFromPartProfile = await GetCustomAccessObjectUsingPartProfileNumber(strPartProfileNumber);
                await FileManager.CheckAndRemoveFilesSupplierChain(objectType);
                if (fileListFromPartProfile != null && fileListFromPartProfile.Count != 0)
                    await FileManager.DownloadUpdates(fileListFromPartProfile, objectType, userTabbedPageDisplayed);
            }

            // Should USER LEVEL be limited to DEAER for member of list or be applied to anyone?
            if (App.AppConfigurationLevel == "DEALER" || App.AppConfigurationLevel == "ENG")
            {
                ResetMemberVerification();
                // Check member of list first then decide how to add files properly
                SupplierChain presentSupplierChain;

                if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    presentSupplierChain = InitialSC.SupplierChains.
                    FirstOrDefault(s => s.ProfileNumber == strPartProfileNumber);
                }
                else
                {
                    //TSX firmwares share same part profile number
                    //So including current rating, would make finding correct firmware at ease
                    var maxCurrent = App.ViewModelLocator.GetParameterTSX("PARCONTROLLERMODELNUMBER").parameterValue;
                    presentSupplierChain = InitialSC.SupplierChains.
                        FirstOrDefault(s => s.ProfileNumber == strPartProfileNumber &&
                        maxCurrent == Convert.ToDouble((int)s.CurrentRating));
                }

                if (presentSupplierChain != null)
                {
                    isMemberApprovalRequired = presentSupplierChain.IsMemberApprovalRequired;
                    if (presentSupplierChain.IsMemberApprovalRequired)
                    {
                        hasUserBeenVerifiedByOEM = VerifyMemberOfList(presentSupplierChain);

                        // Check Dealer whether he was verified or not
                        if (!hasUserBeenVerifiedByOEM)
                        {
                            // Do we need to check DEALER only ??
                            // Check internally if a member is required for approval instead of querying to the database
                            App.AppConfigurationLevel = "USER";
                            authorizationErrorMessage = authorizationErrorMessage.Replace("[name placeholder]", $"{presentSupplierChain.FriendlyCompanyName}");
                            BeginInvokeOnMainThreadAsync(() =>
                            userTabbedPageDisplayed.DisplayAlert($"{char.ToUpper(accessLevel[0]) + accessLevel.Substring(1)} " +
                                                        "Authorization Needed",
                                                        authorizationErrorMessage, "Ok"));
                        }

                   }
                }
                // other wise supplier chain was not found or error !!!
            }
            else if (App.AppConfigurationLevel == "USER" || App.AppConfigurationLevel == "ADVANCED_USER")
            {
                // There is no further actions required for this type of USER at this point

            }
            //replace screens
            //TODO: NOT SURE ADD LOCAL PUBLIC aware of priority or not
            await FileManager.AddLocalPublicFiles(strPartProfileNumber, userTabbedPageDisplayed);
        }

        private void ResetMemberVerification()
        {
            hasUserBeenVerifiedByOEM = false;
            isMemberApprovalRequired = false;
            authorizationErrorMessage = $"Please contact [name placeholder] to obtain {accessLevel} approval to allow modifications to this vehicle settings";
        }

        public bool hasUserBeenVerifiedByOEM;
        public bool isMemberApprovalRequired;
        private string authorizationErrorMessage;
        private string accessLevel;

        private static bool VerifyMemberOfList(SupplierChain supplierChain)
        {
            string memberOfList = Authentication.GetMemberOfList(); //OFFLINE
            var category = Enum.GetName(typeof(InitialSC.Categories), supplierChain.Category);
            category = category.Substring(0, category.Length - 1).ToUpper();

            if (memberOfList != "" && memberOfList.Contains(category))
            {
                return ExtractMemberOfListInternal(memberOfList, category).Contains(supplierChain.CompanyName);
            }

            return false;
        }

        public string[] ExtractMemberOfList(string memberOfList, string category)
        {
            return ExtractMemberOfListInternal(memberOfList, category);
        }

        public static string[] ExtractMemberOfListInternal(string memberOfList, string category)
        {
            Match match = Regex.Match(memberOfList, @"(?<=" + category + @":).+?(?=,\w+:|$)", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return match.Value.Split(',');
            }

            return new string[] {""};
        }


        public async Task<List<string>> GetCustomAccessObjectUsingPartProfileNumber(string strPartProfileNumber)
        {
            var className = "SupplyChain";
            var prefix = "";
            Dictionary<string, object> supplyChainDictionary = new Dictionary<string, object>();

            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                prefix = "TSX";
                var strCurrentRating = Enum.GetName(typeof(InitialSC.CurrentRating),
                    (int)App.ViewModelLocator.GetParameterTSX("PARCONTROLLERMODELNUMBER").parameterValue);

                supplyChainDictionary.Add("CurrentRating", strCurrentRating.Substring(3));
            }

            //supplyChainDictionary.Add("lastTwoDigit", "69"); //LandMaster
            supplyChainDictionary.Add("ProfileNumber", strPartProfileNumber);
            var customAccessObject = await App.ParseManagerAdapter.GetCustomAccessObjectsFromSupplyChain(prefix + className, supplyChainDictionary);

            return (customAccessObject?["FileList"] as List<object>)?.Select(s => (string)s).ToList();
        }

        async public Task CheckForUpdates(CancellationToken cancellationToken)
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    // Connection to internet is available
                    Dictionary<string, object> AccessObjectAppOnlySchema = new Dictionary<string, object>
                    {
                        ["FileList"] = "string list",
                        ["SupplierChain"] = new List<string> { "MANUFACTURER", "OEM", "DISTRIBUTER", "DEALER" },
                        ["ScreenList"] = new List<string> { "a", "b", "c", "d" },
                        ["Something else"] = new List<string> { "1", "2", "3", "4" }
                    };

                    FetchAllCustomAccessObjectBelongsToMember();

                    await BeginInvokeOnMainThreadAsync(() =>
                    {
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Checking For Updates...";
                        (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = true;
                    });
                    foreach (var AccessObject in AccessObjectAppOnlySchema)
                    {
                        switch (AccessObject.Key)
                        {
                            //
                            case "FileList":
                                Task<List<string>> serverResponseTask = GetFileListFromUser(AccessObject.Key, cancellationToken);
                                var tcs = new TaskCompletionSource<bool>();
                                using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs))
                                {
                                    // If the serverResponseTask completes first, this will await that task, 
                                    // but if the cancellation token triggers first, this will await the tcs.Task
                                    if (serverResponseTask != await Task.WhenAny(serverResponseTask, tcs.Task))
                                    {
                                        throw new OperationCanceledException(cancellationToken);
                                    }
                                }
                                var objectType = "CustomAccessObject";

                                List<string> userFileList = await serverResponseTask;
                                //check for files to remove
                                FileManager.CheckAndRemoveFiles(userFileList, objectType);

                                if (userFileList.Count != 0)
                                    await FileManager.DownloadUpdates(userFileList, objectType, userTabbedPageDisplayed);
                                break;
                            case "2":
                                Debug.WriteLine("Case 2");
                                break;
                            default:
                                Debug.WriteLine("Default case");
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("CheckForUpdates Exception: " + e.Message);
                throw;
            }
        }

        public async Task<List<string>> GetFileListFromUser(string key)
        {
            Dictionary<string, object> listOfAccessObjects = await App.ParseManagerAdapter.GetCustomAccessObjects(App.PresentConnectedController);
            List<string> FileList = new List<string>();
            if (listOfAccessObjects != null && listOfAccessObjects.Count > 0)
            {
                foreach (var accessObject in listOfAccessObjects)
                {
                    if ((accessObject.Value as Dictionary<string, object>).ContainsKey(key))
                        FileList = ((accessObject.Value as Dictionary<string, object>)[key] as List<object>).Select(s => (string)s).ToList();
                    else
                        break;
                }
            }
            return FileList;
        }

        public async Task<List<string>> GetFileListFromUser(string key, CancellationToken cancellationToken)
        {
            Dictionary<string, object> listOfAccessObjects = await App.ParseManagerAdapter.GetCustomAccessObjects(App.PresentConnectedController, cancellationToken);
            List<string> FileList = new List<string>();
            if (listOfAccessObjects != null && listOfAccessObjects.Count > 0)
            {
                foreach (var accessObject in listOfAccessObjects)
                {
                    if ((accessObject.Value as Dictionary<string, object>).ContainsKey(key))
                        FileList = ((accessObject.Value as Dictionary<string, object>)[key] as List<object>).Select(s => (string)s).ToList();
                    else
                        break;
                }
            }
            return FileList;
        }

        async public Task<bool> InitEngTabs()
        {
            App._devicecommunication.bEnableCommunicationTransmissions = false;

            await BeginInvokeOnMainThreadAsync(() =>
            {
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Tabs";
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = true;
            });

            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                await BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Read Tab";
                    //ReadOnlyTSXPage ReadOnlyTSXPage = new ReadOnlyTSXPage();
                    //userTabbedPageTSX.Children.Add(ReadOnlyTSXPage);
                    userTabbedPageTSX.Children.Add(new DynamicPage("ReadOnlyPage.TSXscreen"));
                }
                );
                await BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Write Tab";
                    //WriteTSXPage WriteTSXPage = new WriteTSXPage();
                    //userTabbedPageTSX.Children.Add(WriteTSXPage);
                    userTabbedPageTSX.Children.Add(new DynamicPage("WritePage.TSXscreen"));
                }
                );
                userTabbedPageTSX.EngTabsInitialized = true;
            }
            else if (ControllerTypeLocator.ControllerType == "TAC")
            {
                await BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Read Tab";
                    //ReadOnlyPage ReadOnlyPage = new ReadOnlyPage();
                    //userTabbedPage.Children.Add(ReadOnlyPage);
                    userTabbedPage.Children.Add(new DynamicPage("ReadOnlyPage.TACscreen"));
                }
                );
                await BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Write Tab";
                    //WritePage WritePage = new WritePage();
                    //userTabbedPage.Children.Add(WritePage);
                }
                );
                await BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Scope Tab";
                    ScopePage = new ScopePage();
                    userTabbedPage.Children.Add(ScopePage);
                    userTabbedPage.Children.Add(new DynamicPage("WritePage.TACscreen"));
                }
                );

                userTabbedPage.EngTabsInitialized = true;
            }

            var index = 0;
            foreach (var x in DeviceComunication.PageCommunicationsListPointer) //TAC and TSX gauge pages are always index 0
            {
                if (index++ != 0)
                    x.parentPage.Active = false;
                else
                    x.parentPage.Active = true;
            }

            await BeginInvokeOnMainThreadAsync(() =>
            {
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = false;
            });

            App._devicecommunication.bEnableCommunicationTransmissions = true;
            return true;
        }

        async public Task<bool> InitDealerTabs()
        {
            App._devicecommunication.bEnableCommunicationTransmissions = false;

            await BeginInvokeOnMainThreadAsync(() =>
            {
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Tabs";
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = true;
            });

            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                await BeginInvokeOnMainThreadAsync(() =>
                {
                    (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).activityMessage.Text = "Loading Settings";
                    DealerSettingsTSXPage = new DealerSettingsTSXPage();
                    userTabbedPageTSX.Children.Add(DealerSettingsTSXPage);
                });
                userTabbedPageTSX.SettingsTabsInitialized = true;
            }
            var index = 0;
            foreach (var x in DeviceComunication.PageCommunicationsListPointer) //TAC and TSX gauge pages are always index 0
            {
                if (index++ != 0)
                    x.parentPage.Active = false;
                else
                    x.parentPage.Active = true;
            }

            await BeginInvokeOnMainThreadAsync(() =>
            {
                (userTabbedPageDisplayed.CurrentPage as NavitasGeneralPage).PageIsBusy = false;
            });

            App._devicecommunication.bEnableCommunicationTransmissions = true;
            return true;
        }

        async public Task<string> GetAppConfigurationLevel()
        {
            App._AutomaticLogin = new AutomaticLogin();
            Task.Factory.StartNew(async () => await App._AutomaticLogin.Login());
            System.Diagnostics.Debug.WriteLine("Logged In with Level " + Authentication.GetUserCredentials()["AccessLevel"]);
            return Authentication.GetUserCredentials()["AccessLevel"];
        }
       

        void SetupMenusAndTabs()
        {
            try
            {
                BeginInvokeOnMainThreadAsync(async () =>
                {
                    if (App.AppConfigurationLevel == "ENG_USER" || App.AppConfigurationLevel == "ENG" || App.AppConfigurationLevel == "DEV")
                    {
                        if (!masterPage.masterPageItems.Where(x => x.Title.Contains("View Engineering Tabs")).Any())
                            masterPage.masterPageItems.Add(new MasterPageItem
                            {
                                Title = "View Engineering Tabs",
                                //IconSource = "contacts.png",
                                TargetType = typeof(UserTabbedPage)
                            });
                    }

                    if (masterPage.masterPageItems.Where(x => x.Title.Contains("Parameter Files")).Any())
                        masterPage.masterPageItems.Remove(masterPage.masterPageItems.First(x => x.Title.Contains("Parameter Files")));

                    if (App.AppConfigurationLevel == "DEALER" ||
                        App.AppConfigurationLevel == "ENG_USER" ||
                        App.AppConfigurationLevel == "ENG" ||
                        App.AppConfigurationLevel == "DEV")
                    {
                        if (ControllerTypeLocator.ControllerType == "TAC")
                        {
                            if (!masterPage.masterPageItems.Where(x => x.Title.Contains("Parameter Files")).Any())
                                masterPage.masterPageItems.Add(new MasterPageItem
                                {
                                    Title = "Parameter Files",
                                    //IconSource = "contacts.png",
                                    TargetType = typeof(DynamicPage),
                                    FileName = "Parameter_Files.TACNavigatescreen"
                                });
                        }
                        else
                        {
                            if (!masterPage.masterPageItems.Where(x => x.Title.Contains("Parameter Files")).Any())
                                masterPage.masterPageItems.Add(new MasterPageItem
                                {
                                    Title = "Parameter Files",
                                    //IconSource = "contacts.png",
                                    TargetType = typeof(DynamicPage),
                                    FileName = "Parameter_Files.TSXNavigatescreen"
                                });
                        }
                    }

                    if (!masterPage.masterPageItems.Where(x => x.Title.Contains("Login")).Any())
                        masterPage.masterPageItems.Add(new MasterPageItem
                        {
                            Title = "Login",
                            //IconSource = "reminders.png",
                            TargetType = typeof(LoginPage)
                        });

                    if (Authentication.GetUserCredentials()["UserName"] != "testc5a3nFD43M" &&
                    !masterPage.masterPageItems.Where(x => x.Title.Contains("My Account")).Any())
                    {
                        masterPage.masterPageItems.Add(new MasterPageItem
                        {
                            Title = "My Account",
                            //IconSource = "reminders.png",
                            TargetType = typeof(MyAccountPage)
                        });
                    }

                    if (masterPage.masterPageItems.Where(x => x.Title.Contains("Controller Firmware Download")).Any())
                        masterPage.masterPageItems.Remove(masterPage.masterPageItems.First(x => x.Title.Contains("Controller Firmware Download")));
                    if (ControllerTypeLocator.ControllerType == "TSX")
                    {
                        userTabbedPageDisplayed = userTabbedPageTSX;
                        InitialSC.SupplierChains = App.ViewModelLocator.MainViewModelTSX.SupplierChains;

                        if (App.AppConfigurationLevel == "ADVANCED_USER" ||
                            App.AppConfigurationLevel == "ENG_USER" ||
                            App.AppConfigurationLevel == "DEALER" ||
                            App.AppConfigurationLevel == "ENG" ||
                            App.AppConfigurationLevel == "DEV")
                        {
                            masterPage.masterPageItems.Add(new MasterPageItem
                            {
                                Title = "Controller Firmware Download",
                                //IconSource = "contacts.png",
                                TargetType = typeof(FirmwareDownloadTSXPage)
                            });

                        }
                    }
                    else
                    {
                        userTabbedPageDisplayed = userTabbedPage;
                        InitialSC.SupplierChains = App.ViewModelLocator.MainViewModel.SupplierChains;

                        if (App.AppConfigurationLevel == "ADVANCED_USER" ||
                        App.AppConfigurationLevel == "ENG_USER" ||
                        App.AppConfigurationLevel == "DEALER" ||
                        App.AppConfigurationLevel == "ENG" ||
                        App.AppConfigurationLevel == "DEV")
                        {
                            masterPage.masterPageItems.Add(new MasterPageItem
                            {
                                Title = "Controller Firmware Download",
                                //IconSource = "contacts.png",
                                TargetType = typeof(FirmwareDownloadPage)
                            });
                        }
                    }


                    if (userTabbedPageDisplayed != Detail.Navigation.NavigationStack.First())
                        Detail.Navigation.InsertPageBefore(userTabbedPageDisplayed, Detail.Navigation.NavigationStack.First());

                    await Detail.Navigation.PopAsync();

                    if (!userTabbedPageDisplayed.TabsInitialized)
                    {
                        await Init();
                    }
                    Dictionary<string, object> listOfParentRoleAccessObjects = await App.ParseManagerAdapter.GetParentRoleAccessObjects(App.PresentConnectedController);

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        masterPage.IconImageSource = "hamburger.png";
                        masterPage.Title = "";
                    }
                    else
                    {
                        masterPage.IconImageSource = "hamburger1.png";
                        masterPage.Title = "Navitas";
                    }
                });
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
                var logVehicleHealth = new VehicleData();
                if (!(App._MainFlyoutPage._DeviceListPage._device is DemoDevice))
                    logVehicleHealth.SendVehicleInformationToDatabase(); //works but don't do it until Leroy sees it
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("SetupMenusAndTabs Exception: " + e.Message);
            }
        }
        bool popupEnabled = true;

        public void SetDirtyBitToSoftwareRevision()
        {
            if (ControllerTypeLocator.ControllerType == "TAC")
            {
                App.ViewModelLocator.GetParameter("SOFTWAREREVISION").couldBeDirtyBecauseKBDoneWasNotUsed = true;
            }
        }

        public bool UserHasWriteCredentials()
        {
            if (App._MainFlyoutPage._DeviceListPage._device is DemoDevice || App.isSkipAuthorization)
                return true;

            if (Authentication.GetUserCredentials()["UserName"] != "testc5a3nFD43M")
            {
                if (!Authentication.GetUserCredentials()["ListOfRegisterControllers"].Contains(App.PresentConnectedController) &&
                    !(App.AppConfigurationLevel == "DEALER") &&
                    !(App.AppConfigurationLevel == "ENG") &&
                    !(App.AppConfigurationLevel == "DEV"))
                {
                    // temp USER and not get APPROVED
                    if (!Authentication.GetUserCredentials()["AccessLevel"].Contains("USER") &&
                        !hasUserBeenVerifiedByOEM)
                    {
                        if (popupEnabled)
                        {
                            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                            {
                                popupEnabled = true;
                                return false;
                            });

                            Device.BeginInvokeOnMainThread(() =>
                            {
                                userTabbedPageDisplayed.DisplayAlert($"{char.ToUpper(accessLevel[0]) + accessLevel.Substring(1)} " +
                                    "Authorization Needed",
                                    authorizationErrorMessage, "Ok");
                            });
                            popupEnabled = false;
                        }

                        return false;
                    }
                    //AlreadyCheckingCredentials stops back to back writes from popping up more than one at a time
                    if (!Authentication.AlreadyCheckingCredentials)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            //Navigation.PushModalAsync(new ScriptingPage("PnrdVerification.html"));
                            SecuredVehicle();
                        });
                    }
                    Authentication.AlreadyCheckingCredentials = true;
                    return false;
                }
                else
                {
                    Authentication.AlreadyCheckingCredentials = false;
                    return true;
                }
            }
            else
            {
                if (!Authentication.AlreadyCheckingCredentials)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Task<bool> task;
                        // How possible a test account has its USER LEVEL as DEALER, ENG, DEV ???
                        if (App.AppConfigurationLevel == "DEALER" || App.AppConfigurationLevel == "ENG" || App.AppConfigurationLevel == "DEV")
                            task = DisplayAlert("Sign Up or Login", "To allow changes to advanced settings on any Navitas controller", "Login", "Ask me later");
                        else if (App.ViewModelLocator.MainViewModel.IsDemoMode || App.ViewModelLocator.MainViewModelTSX.IsDemoMode)
                            task = DisplayAlert("Sign up or login", "To allow changes and updates to your account.\n\nNote: Controller information and settings are only for simulation. To leave this demo, tap on the menu icon at the top left corner and select Exit Demo.", "Login", "Ask me later");
                        else //not in Demo
                            task = DisplayAlert("Sign up or login", "To allow configuration changes to this vehicle and secure this vehicle to your phone", "Login", "Ask me later");

                        task.ContinueWith(LoginAlertCallback);
                    });
                }
                Authentication.AlreadyCheckingCredentials = true;
                return false;
            }
        }


        void LoginAlertCallback(Task<bool> task)
        {
            string Result = task.Result ? "Login" : "Cancel";
            if (Result == "Login")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Detail.Navigation.PushAsync(new LoginPage());
                });
            }
            else
                Authentication.AlreadyCheckingCredentials = false;
        }

        private async void SecuredVehicle()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    if (!(App._MainFlyoutPage._DeviceListPage._device is DemoDevice))
                    {
                        //It looks like the properties which inside Device.cs, do not update until this function call !!
                        if (await App.ParseManagerAdapter.IsControllerRegistered(App.PresentConnectedController) &&
                            !await App.ParseManagerAdapter.IsRegisteredUser(App.PresentConnectedController))
                        {
                            //HasRegister(Controller)-ButNotYou(!IsRegisteredUser) seems to make sense 
                            await DisplayAlert("Warning", "This Cart/Controller is registered to another User ID.\nPlease contact the Owner/Dealer for options on change of registration.\n\nNOTE:\nAdditional User ID's can be added or removed by the primary User ID in the My Account section when connected to a Cart or in Demo mode.", "OK");
                            Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
                            Authentication.SetUserWasShownVehicleOperatorVerification();
                        }
                        else if (!Authentication.GetUserCredentials()["ListOfRegisterControllers"].Contains(App.PresentConnectedController))
                        {
                            //This line of code looks simple but it is our Security Feature. Believe or not ?
                            Navigation.PushModalAsync(new ScriptingPage("PnrdVerification.html", true));
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Connection Error", "Please Check Your Internet Connection and try again !", "OK");
                    Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
                    Authentication.SetUserWasShownVehicleOperatorVerification();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("SecuredVehicle Exception: " + e.Message);
            }
        }

        public void ModifyMasterPageItems(string searchTitle, string titleExpected)
        {
            if (masterPage.masterPageItems.Where(x => x.Title.Contains(searchTitle)).Any())
            {
                Device.BeginInvokeOnMainThread(() =>
                    masterPage.masterPageItems.Where(x => x.Title.Contains(searchTitle))
                                              .First().Title = titleExpected);
            }
        }

        public List<string> AccessObjectType = new List<string>() { "FileList" }; /*"ScreenList"*/

        public async Task FetchAllCustomAccessObjectBelongsToMember()
        {
            var category = "OEMs";
            var members = App.ParseManagerAdapter.GetMemberOfList();
            var extractedMembers = App._MainFlyoutPage.ExtractMemberOfList(members, category.Substring(0, category.Length - 1));

            // Collect the whole list to download at once
            var fileListCollector = new List<string>();

            // Looping through each single member to get its CustomAccessObject ??
            foreach (var member in extractedMembers)
            {
                var supplyChainCollector = await App.ParseManagerAdapter.GetOEMRecordsThatHaveMemberMatched(category, member);

                if (!supplyChainCollector.Keys.Any(key => key == "Error")) // new way to handle Internet issue ??
                {
                    var supplyChains = supplyChainCollector["SupplyChains"] as Dictionary<string, object>;
                    foreach (var supplyChain in supplyChains)
                    {
                        var customAccessObject = supplyChain.Value as Dictionary<string, object>;
                        App._MainFlyoutPage.AccessObjectType.ForEach(property =>
                        {
                            if (customAccessObject.Keys.Any(key => key == property))
                            {
                                //System.Diagnostics.Debug.WriteLine($"Automatic Login member {member}'s AccessObject has porperty: {property}");
                                // Append each file name, so later we could download once
                                (customAccessObject?[property] as List<object>)?
                                                                .Select(f => (string)f)
                                                                .ForEach(fileName =>
                                                                {
                                                                    if (!fileListCollector.Contains(fileName))
                                                                        fileListCollector.Add(fileName);
                                                                });
                            }
                        });
                    }
                }
            }
            var objectType = "SupplyChainAccessObject";
            await FileManager.ValidateAndDownloadFiles(fileListCollector, objectType);
        }
    }
}