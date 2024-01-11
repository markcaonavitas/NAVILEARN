using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NavitasBeta
{
    public class HybridWebViewCommunication
    {
        public NavitasGeneralPage parentPage;
        HybridWebView hybridWebView;
        public bool cancelReadTimer = false;
        public HybridWebViewCommunication(NavitasGeneralPage parentPage, HybridWebView hybridWebView)
        {
            this.parentPage = parentPage;
            this.hybridWebView = hybridWebView;
            HybridWebViewRegisterCallback(hybridWebView);
        }

        bool cancelReadLocationTimer = false;
        void HybridWebViewRegisterCallback(HybridWebView hybridWebView)
        {
            hybridWebView.RegisterAction(async (string data) => //called from InvokeC# thing
            {
                try
                {
                    //PageParameters.Active = false; //main communication activity constantly running when page is visible
                    System.Diagnostics.Debug.WriteLine("StopContinuousReadsAndTimer started " + DateTime.Now.Millisecond.ToString());
                    await StopContinuousReadsAndTimer(); //queing communications would make more sense then inserting and removing lists of parameters?
                                                         //var debug = MainThread.IsMainThread; //just to make sure it is not the main UI thread
                    System.Diagnostics.Debug.WriteLine("diags Invoke recieved " + DateTime.Now.Millisecond.ToString() + " data = " + data);

                    PageParameterList pageParameters;
                    if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                        pageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, parentPage);
                    else
                        pageParameters = new PageParameterList(PageParameterList.ParameterListType.TSX, parentPage);

                    Dictionary<string, Dictionary<string, float>> anObject = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, float>>>(data);

                    //foreach( DeviceComunication.PageCommunicationsList pageList in DeviceComunication.PageCommunicationsListPointer)
                    //{
                    //    foreach(var paramlist in pageList.parametersGroupedto64bytesAndAddressRange)
                    //}
                    if (anObject.ContainsKey("ReadContinuously"))
                    {
                        //for speed
                        //PageParameters.Active = false; //main communication activity constantly running when page is visible

                        foreach (Dictionary<string, float> item in anObject.Values)
                            foreach (var parameter in item)
                                if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                                {
                                    pageParameters.parameterList.Add(App.ViewModelLocator.GetParameter(parameter.Key));
                                    if (parameter.Key == "ORIENTATIONX" && !parentPage.isOrientationEnabled)
                                    {
                                        parentPage.GetOrientation();
                                    }
                                }
                                else
                                {
                                    pageParameters.parameterList.Add(App.ViewModelLocator.GetParameterTSX(parameter.Key));
                                }

                        parentPage.communicationsReadWithoutWait(pageParameters); //non blocking continuous communications read packet sent setting htmlPageIndex
                        SendJsCommandToWebview(pageParameters, false); //respond to intial request

                        var ts = TimeSpan.FromMilliseconds(100);
                        cancelReadTimer = false;

                        //now start the next one
                        Device.StartTimer(ts, () =>
                        {
                            if (!cancelReadTimer)
                            {
                                SendJsCommandToWebview(pageParameters, true);
                            }
                            return !cancelReadTimer;
                        });

                    }
                    else if (anObject.ContainsKey("Read"))
                    {
                        //PageParameters.Active = true; //main communication activity constantly running when page is visible

                        foreach (Dictionary<string, float> item in anObject.Values)
                            foreach (var parameter in item)
                                if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                                {
                                    pageParameters.parameterList.Add(App.ViewModelLocator.GetParameter(parameter.Key));
                                    if(parameter.Key == "ORIENTATIONX" && !parentPage.isOrientationEnabled)
                                    {
                                        parentPage.GetOrientation();
                                    }
                                }
                                else
                                {
                                    pageParameters.parameterList.Add(App.ViewModelLocator.GetParameterTSX(parameter.Key));
                                }

                        await parentPage.communicationsReadWithWait(pageParameters); //blocking communications read packet sent
                        SendJsCommandToWebview(pageParameters);
                    }
                    else if (anObject.ContainsKey("Write"))
                    {
                        //PageParameters.Active = true; //main communication activity constantly running when page is visible

                        foreach (Dictionary<string, float> item in anObject.Values)
                        {
                            foreach (var parameter in item)
                            {
                                GoiParameter goiParameter;
                                if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                                    goiParameter = App.ViewModelLocator.GetParameter(parameter.Key);
                                else
                                    goiParameter = App.ViewModelLocator.GetParameterTSX(parameter.Key);

                                //remember parameterValue is a propety and will automatically update all GUI references to onPropertyChanged
                                goiParameter.parameterValue = parameter.Value;
                                pageParameters.parameterList.Add(goiParameter);
                            }
                        }
                        parentPage.communicationsWriteWithWait(pageParameters);
                        await parentPage.communicationsReadWithWait(pageParameters); //blocking communications read packet sent
                        SendJsCommandToWebview(pageParameters);
                    }
                    else if (anObject.ContainsKey("ReadScopeDataBlock"))
                    {
                        System.Diagnostics.Debug.WriteLine("ReadScopeDataBlock called");
                        //PageParameters.Active = true; //main communication activity constantly running when page is visible

                        foreach (Dictionary<string, float> item in anObject.Values)
                        {
                            foreach (var parameter in item)
                            {
                                GoiParameter goiParameter;
                                if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                                    goiParameter = App.ViewModelLocator.GetParameter(parameter.Key);
                                else
                                    goiParameter = App.ViewModelLocator.GetParameterTSX(parameter.Key);
                                //remember parameterValue is a propety and will automatically update all GUI references to onPropertyChanged
                                goiParameter.parameterValue = parameter.Value;
                                pageParameters.parameterList.Add(goiParameter);
                            }

                        }
                        var jsonData = await parentPage.communicationsReadScopeBlockWithWait(pageParameters); //hacks in a changed response
                        SendJsCommandToWebview(jsonData);
                    }
                    else if (anObject.ContainsKey("Close"))
                    {
                        Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
                        Authentication.SetUserWasShownVehicleOperatorVerification();
                        hybridWebView.EvaluateJavascript("App.NavitasMotorController.CompleteTheJsonCommand([]);"); //acknowledges javascript promise used for webview communications
                        await parentPage.Navigation.PopModalAsync(true);
                    }
                    else if (anObject.ContainsKey("ControlVerified"))
                    {
                        Authentication.SaveRegisteredVehicles(App.PresentConnectedController);
                        if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                            await App.ParseManagerAdapter.TransmitRegisteredUsers("Vehicles", App.PresentConnectedController);
                        else
                            await App.ParseManagerAdapter.TransmitRegisteredUsers("TSXVehicles", App.PresentConnectedController);

                        hybridWebView.EvaluateJavascript("App.NavitasMotorController.CompleteTheJsonCommand([]);"); //acknowledges javascript promise used for webview communications
                    }
                    else if (anObject.ContainsKey("GetAppParameters"))
                    {
                        StringWriter textWriter = new StringWriter();
                        if (App.PresentConnectedController.Contains("TAC"))
                        {
                            (new XmlSerializer(typeof(ObservableCollection<GoiParameter>))).Serialize(textWriter, App.ViewModelLocator.MainViewModel.GoiParameterList);
                            hybridWebView.EvaluateJavascript("App.PresentConnectedController = '" + App.PresentConnectedController + "';" + "TACDictionaryAsXmlString = `" + textWriter.ToString() + "`; " +
                                "App.NavitasMotorController.Model = '" + App.PresentConnectedController + "'; " +
                                "App.NavitasMotorController.CompleteTheJsonCommand([]);");
                        }
                        else
                        {
                            (new XmlSerializer(typeof(ObservableCollection<GoiParameter>))).Serialize(textWriter, App.ViewModelLocator.MainViewModelTSX.GoiParameterList);
                            hybridWebView.EvaluateJavascript("App.PresentConnectedController = '" + App.PresentConnectedController + "';" + "TSXDictionaryAsXmlString = `" + textWriter.ToString() + "`; " +
                                "App.NavitasMotorController.Model = '" + App.PresentConnectedController + "'; " +
                                "App.NavitasMotorController.CompleteTheJsonCommand([]);");
                        }
                    }
                    else if (anObject.ContainsKey("ChangeCurrentPage"))
                    {
                        System.Diagnostics.Debug.WriteLine("ChangeCurrentPage called");
                        foreach (Dictionary<string, float> item in anObject.Values)
                        {
                            foreach (var parameter in item)
                            {
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    App._MainFlyoutPage.userTabbedPage.CurrentPage = App._MainFlyoutPage.userTabbedPage.Children[Convert.ToInt32(parameter.Value)];

                                });
                            }
                        }
                        hybridWebView.EvaluateJavascript("App.NavitasMotorController.CompleteTheJsonCommand([]);"); //acknowledges javascript promise used for webview communications
                        var stack = parentPage.Navigation.ModalStack;
                    }
                    else if (anObject.ContainsKey("LoadWholeApp"))
                    {
                        string a = FileManager.GetExternalFirstOrInternalFileText(parentPage.htmlFilePathAndName);

                        //inject multiple scripts content
                        string b = "";
                        while (a.IndexOf("<script>") != -1)
                        {
                            var startOfScriptIndex = a.IndexOf("<script>"); //8 bytes <script>
                            var startOfMatchingSlashScriptIndex = a.IndexOf("</script>", startOfScriptIndex); //9 bytes in </script>
                            if ((startOfScriptIndex + 8) < startOfMatchingSlashScriptIndex)
                            {//TODO: generate an error for the developer if things like <script src=....> are formated differently
                                b += a.Substring(startOfScriptIndex + 8, startOfMatchingSlashScriptIndex - (startOfScriptIndex + 8));
                                a = a.Remove(startOfScriptIndex, (startOfMatchingSlashScriptIndex + 9) - (startOfScriptIndex));
                            }
                        }
                        //check if body exists incase we only want to execute code in future with existing body UI
                        string c = "";
                        if (a.IndexOf("<body") != -1)
                        {
                            //inject single body content, removing any text between <body ...and...>
                            var endOfBodyTag = a.IndexOf(">", a.IndexOf("<body"));
                            c = a.Substring(endOfBodyTag + 1, (a.IndexOf("</body>")) - (endOfBodyTag + 1));
                        }
                        //for script tags, put them in the body because below does not work somehow
                        //handle a single style content.
                        if (a.IndexOf("<style>") != -1)
                            c += a.Substring(a.IndexOf("<style>"), (a.IndexOf("</style>") + 8) - a.IndexOf("<style>"));
                        hybridWebView.EvaluateJavascript("document.body.innerHTML = `" + c + "`");
                        if (b != "")
                            hybridWebView.EvaluateJavascript(b);
                        //Below line seems to happen when injecting above body content with innerHTML?
                        //hybridWebView.EvaluateJavascript("window.document.dispatchEvent(new Event('DOMContentLoaded', {bubbles: true,cancelable: true}));");
                    }
                    else if (anObject.ContainsKey("LoadAllTestScripts"))
                    {
                        List<string> publicFileList = FileManager.GetNavitasDirectoryFiles().ToList();
                        List<string> localFileList = FileManager.GetInternalDirectoryFiles().ToList();
                        List<string> pathAndFileList = new List<string>();

                        foreach (var file in publicFileList)
                        {
                            if (file.Contains("Test") && file.Contains(".html"))
                                pathAndFileList.Add(System.IO.Path.Combine(FileManager.GetNavitasDirectoryPath(), file));
                        }
                        foreach (var file in localFileList)
                        {
                            //Sorry I'm getting lazy (file.Remove(0,file.IndexOf("NavitasBeta.CommonWebViews.") + 27))) is too hardcoded but works for this App
                            if (file.Contains("Test") && file.Contains(".html") && !publicFileList.Any(w => w.Contains(file.Remove(0, file.IndexOf("NavitasBeta.CommonWebViews.") + 27))))
                                pathAndFileList.Add(file);
                        }

                        foreach (var file in pathAndFileList)
                        {
                            string a = FileManager.GetExternalFirstOrInternalFileText(file);
                            //inject multiple scripts content
                            string b = "";
                            while (a.IndexOf("<script>") != -1)
                            {
                                var startOfScriptIndex = a.IndexOf("<script>"); //8 bytes <script>
                                var startOfMatchingSlashScriptIndex = a.IndexOf("</script>", startOfScriptIndex); //9 bytes in </script>
                                if ((startOfScriptIndex + 8) < startOfMatchingSlashScriptIndex)
                                {//TODO: generate an error for the developer if things like <script src=....> are formated differently
                                    b += a.Substring(startOfScriptIndex + 8, startOfMatchingSlashScriptIndex - (startOfScriptIndex + 8));
                                    a = a.Remove(startOfScriptIndex, (startOfMatchingSlashScriptIndex + 9) - (startOfScriptIndex));
                                }
                            }
                            //check if body exists incase we only want to execute code in future with existing body UI
                            //string c = "";
                            //if (a.IndexOf("<body") != -1)
                            //{
                            //    //inject single body content, removing any text between <body ...and...>
                            //    var endOfBodyTag = a.IndexOf(">", a.IndexOf("<body"));
                            //    c = a.Substring(endOfBodyTag + 1, (a.IndexOf("</body>")) - (endOfBodyTag + 1));
                            //}
                            ////for script tags, put them in the body because below does not work somehow
                            ////handle a single style content.
                            ////if (a.IndexOf("<style>") != -1)
                            ////c += a.Substring(a.IndexOf("<style>"), (a.IndexOf("</style>") + 8) - a.IndexOf("<style>"));
                            //hybridWebView.EvaluateJavascript("document.body.innerHTML = `" + c + "`");
                            if (b != "")
                                hybridWebView.EvaluateJavascript(b);
                            hybridWebView.EvaluateJavascript("App.NavitasMotorController.CompleteTheJsonCommand([]);");
                            //Below line seems to happen when injecting above body content with innerHTML?
                            //hybridWebView.EvaluateJavascript("window.document.dispatchEvent(new Event('DOMContentLoaded', {bubbles: true,cancelable: true}));");
                        }

                    }
                    else if (anObject.ContainsKey("FocusOnElement"))
                    {
                        System.Diagnostics.Debug.WriteLine("FocusOnElement called");
                        foreach (Dictionary<string, float> item in anObject.Values)
                        {
                            foreach (var parameter in item)
                            {
                                foreach (Element child1 in ((App._MainFlyoutPage.userTabbedPageDisplayed.CurrentPage as ContentPage).Content as Layout).Children)
                                {
                                    System.Diagnostics.Debug.WriteLine("child1 type is " + child1.GetType().Name);
                                    if (child1.GetType().Name == "StackLayout" || child1.GetType().Name == "ScrollView")
                                        foreach (Element child2 in (child1 as Layout).Children)
                                        {
                                            System.Diagnostics.Debug.WriteLine("child2 type is                     " + child2.GetType().Name + "   " + child2.BindingContext.ToString());
                                            if (child2.GetType().Name == "StackLayout")
                                                foreach (Element child3 in (child2 as Layout).Children)
                                                {
                                                    System.Diagnostics.Debug.WriteLine("child3 type is                                            " + child3.GetType().Name + "   " + child3.BindingContext.ToString());
                                                    if (child3.GetType().Name == "StackLayout")
                                                        foreach (Element child4 in (child3 as Layout).Children)
                                                        {
                                                            System.Diagnostics.Debug.WriteLine("child4 type is                                                                     " + child4.GetType().Name + "   " + child4.BindingContext.ToString());
                                                            if (child4.GetType().Name == "StackLayout" || child4.GetType().Name == "Frame")
                                                                foreach (Element child5 in (child4 as Layout).Children)
                                                                {
                                                                    System.Diagnostics.Debug.WriteLine("child5 type is                                                                                    " + child5.GetType().Name + "   " + child5.BindingContext.ToString());
                                                                    if (child5.GetType().Name == "StackLayout")
                                                                        foreach (Element child6 in (child5 as Layout).Children)
                                                                        {
                                                                            System.Diagnostics.Debug.WriteLine("child6 type is                                                                                             " + child6.GetType().Name + "   " + child6.BindingContext.ToString());
                                                                            foreach (Element child7 in (child6 as Layout).Children)
                                                                            {
                                                                                System.Diagnostics.Debug.WriteLine("child7 Property is   " + (child7.BindingContext as GoiParameter).PropertyName);
                                                                                if ((child7.BindingContext as GoiParameter).PropertyName == parameter.Key && child7.GetType().Name == "DoneEntry")
                                                                                {
                                                                                    (child7 as DoneEntry).Focus();
                                                                                    (child7 as DoneEntry).CursorPosition = (child7 as DoneEntry).Text.Length;
                                                                                    (child7 as DoneEntry).SelectionLength = 1;
                                                                                }
                                                                            }
                                                                        }
                                                                }
                                                        }
                                                }
                                        }
                                }
                            }
                        }
                        hybridWebView.EvaluateJavascript("App.NavitasMotorController.CompleteTheJsonCommand([]);"); //acknowledges javascript promise used for webview communications
                    }
                    else if (anObject.ContainsKey("OpenOrAppendOrCloseFIle"))
                    {
                        //PageParameters.Active = true; //main communication activity constantly running when page is visible
                        string fileName = anObject["OpenOrAppendOrCloseFIle"].Keys.First();
                        string fileData = anObject["OpenOrAppendOrCloseFIle"].Keys.Last();
                        FileManager.OpenOrAppendOrCloseFIle(fileName, fileData);
                        hybridWebView.EvaluateJavascript("App.NavitasMotorController.CompleteTheJsonCommand([]);"); //acknowledges javascript promise used for webview communications
                    }
                    else if (anObject.ContainsKey("DatabaseUpload"))
                    {
                        //PageParameters.Active = true; //main communication activity constantly running when page is visible
                        string databaseClass = anObject["DatabaseUpload"].Keys.First();
                        FileManager.CloudUpload(databaseClass);
                        ConnectivityService.CheckInternetStabilityAndCleanUpRemaningFileUpload();
                        hybridWebView.EvaluateJavascript("App.NavitasMotorController.CompleteTheJsonCommand([]);"); //acknowledges javascript promise used for webview communications
                    }                        // how do we know if it succesfully or not ??
                    //check for LiveReload
                    //now get the updated values
                    //if (!checkLiveReload(hTMLFileNameAndPath))
                    //this page reLoad when popup opens
                }
                catch (Exception ex)
                {
                    parentPage.DisplayAlert(parentPage.Title + " page error: ", ex.ToString(), "Close");
                }
            });

            App._devicecommunication.SetScopeBlockResponseHandler(parentPage.ResponseFromScopeBlock);
        }

        void SendJsCommandToWebview(string jsonData, bool unsolicited = false)
        {
            if (hybridWebView.EvaluateJavascript != null)// && App._MainPage.userTabbedPage.CurrentPage == this)
            {
                try
                {
                    if (!unsolicited)
                    {
                        var result = hybridWebView.EvaluateJavascript("App.PresentConnectedController = '" + App.PresentConnectedController + "';" + "App.NavitasMotorController.CompleteTheJsonCommand(" + jsonData + ");"); // CancelNavitasTacControllerSimulators();");
                        System.Diagnostics.Debug.WriteLine("EvaluateJavascript solicited sent" + DateTime.Now.Millisecond.ToString());
                    }
                    else
                    {
                        var result = hybridWebView.EvaluateJavascript("App.PresentConnectedController = '" + App.PresentConnectedController + "';" + "App.NavitasMotorController.UnsolicitedCompleteTheJsonCommand(" + jsonData + ");"); // CancelNavitasTacControllerSimulators();");
                        System.Diagnostics.Debug.WriteLine("EvaluateJavascript unsolicited sent" + DateTime.Now.Millisecond.ToString());
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Not loaded yet? : " + e.Message);
                }
            }

        }

        void SendJsCommandToWebview(PageParameterList pageParameters, bool unsolicited = false)
        {

            string jsonData;
            if (hybridWebView.EvaluateJavascript != null)// && App._MainPage.userTabbedPage.CurrentPage == this)
            {
                var parameterStream = new List<ParameterNameAndValue>();
                foreach (var item in pageParameters.parameterList)
                {
                    GoiParameter parameter;
                    if (ControllerTypeLocator.ControllerType == "TAC") //get a viewmodel pointer as a real locator so we don't have to do this
                        parameter = App.ViewModelLocator.GetParameter(item.PropertyName);
                    else
                        parameter = App.ViewModelLocator.GetParameterTSX(item.PropertyName);

                    parameterStream.Add(new ParameterNameAndValue
                    {
                        PropertyName = item.PropertyName,
                        parameterValue = parameter.parameterValue,
                    });
                }

                if (parameterStream.Count() != 0)
                    jsonData = JsonConvert.SerializeObject(parameterStream);
                else
                    jsonData = ""; //this will cause the webview to send its next request
                try
                {
                    if (!unsolicited)
                    {
                        var result = hybridWebView.EvaluateJavascript("App.PresentConnectedController = '" + App.PresentConnectedController + "';" + "App.NavitasMotorController.CompleteTheJsonCommand(" + jsonData + ");"); // CancelNavitasTacControllerSimulators();");
                        System.Diagnostics.Debug.WriteLine("EvaluateJavascript solicited sent" + DateTime.Now.Millisecond.ToString());
                    }
                    else
                    {
                        var result = hybridWebView.EvaluateJavascript("App.PresentConnectedController = '" + App.PresentConnectedController + "';" + "App.NavitasMotorController.UnsolicitedCompleteTheJsonCommand(" + jsonData + ");"); // CancelNavitasTacControllerSimulators();");
                        System.Diagnostics.Debug.WriteLine("EvaluateJavascript unsolicited sent" + DateTime.Now.Millisecond.ToString());
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Not loaded yet? : " + e.Message);
                }
            }
        }
        public async Task StopContinuousReadsAndTimer()
        {
            if (parentPage.htmlPageUniqueId != -1)
            {//cancel any pending communications
                while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                    await Task.Delay(10); //TODO:Flag this as an issue if it happens
                App._devicecommunication.SetUniqueIdToBeRemoved(parentPage.htmlPageUniqueId);
                while (App._devicecommunication.GetUniqueIdToBeRemoved() != 0)
                {
                    await Task.Delay(10); ; //be nice and free up the thread
                }
                parentPage.htmlPageUniqueId = -1;
                cancelReadTimer = true; //let the command restart this
            }
        }

    }
}