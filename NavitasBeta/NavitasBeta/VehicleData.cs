using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NavitasBeta
{
    public class VehicleData : NavitasGeneralPage
    {
        public VehicleData()
        {
            this.Title = "Vehicle Data";
        }

        public async Task SendVehicleInformationToDatabase()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //activityIndicator.IsRunning = true;
            });

            Dictionary<string, object> vehicleHealth = new Dictionary<string, object>();
            Dictionary<string, object> vehicleSettings = new Dictionary<string, object>();

            if (ControllerTypeLocator.ControllerType == "TSX")
            {
                ParameterVehicleItems parameterTSXVehicleSetting = FileManager.GetDeserializedObject<ParameterVehicleItems>("ParameterTSXVehicleSettings.xml");
                ParameterVehicleItems parameterTSXVehicleHealth = FileManager.GetDeserializedObject<ParameterVehicleItems>("TSXVehicleHealth.xml");

                PageParameterList pageParameters = new PageParameterList(PageParameterList.ParameterListType.TSX, this);

                //Below comments are for future idea of reading of parse vehicle class to get its list of parameters
                //the same way we get parameters from the webview interface, even think of future live gaming options
                //like a customer support guy issuing a read to the customer vehicle directly
                //List<ParameterNameAndValue> objectFromJson = JsonConvert.DeserializeObject<List<ParameterNameAndValue>>(Regex.Replace(data, "Read:", ""));
                //foreach (var item in objectFromJson)
                //    pageParameters.parameterList.Add(App.ViewModelLocator.GetParameterTSX(item.PropertyName));

                //Reading Vehicle Settings
                foreach (ParameterItem parameterItem in parameterTSXVehicleSetting.ParameterVehicleList)
                {
                    BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName));
                }

                //Reading Vehicle Health
                foreach (ParameterItem parameterItem in parameterTSXVehicleHealth.ParameterVehicleList)
                {
                    BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName));
                }

                await communicationsReadWithWait(pageParameters); //blocking communications read packet sent

                //Add parameter's values into vehicleSettings Dictionary
                foreach (ParameterItem parameterItem in parameterTSXVehicleSetting.ParameterVehicleList)
                {
                    vehicleSettings.Add(App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName).Name, App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName).parameterValue);
                }

                vehicleHealth.Add("MotorController", App.PresentConnectedController);

                foreach (ParameterItem parameterItem in parameterTSXVehicleHealth.ParameterVehicleList)
                {
                    if (parameterItem.ViewType == ParameterItem.ViewTypes.Enum)
                        vehicleHealth.Add(App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName).Name, App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName).parameterEnumString);
                    else if (parameterItem.ViewType == ParameterItem.ViewTypes.String)
                        vehicleHealth.Add(App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName).Name, App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName).parameterValueString);
                    else
                        vehicleHealth.Add(App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName).Name, App.ViewModelLocator.GetParameterTSX(parameterItem.PropertyName).parameterValue);
                }
            }
            else if (ControllerTypeLocator.ControllerType == "TAC")
            {
                ParameterVehicleItems parameterVehicleSetting = FileManager.GetDeserializedObject<ParameterVehicleItems>("ParameterVehicleSettings.xml");
                ParameterVehicleItems parameterVehicleHealth = FileManager.GetDeserializedObject<ParameterVehicleItems>("VehicleHealth.xml");

                PageParameterList pageParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, this);

                //Below comments are for future idea of reading of parse vehicle class to get its list of parameters
                //the same way we get parameters from the webview interface, even think of future live gaming options
                //like a customer support guy issuing a read to the customer vehicle directly
                //List<ParameterNameAndValue> objectFromJson = JsonConvert.DeserializeObject<List<ParameterNameAndValue>>(Regex.Replace(data, "Read:", ""));
                //foreach (var item in objectFromJson)
                //    pageParameters.parameterList.Add(App.ViewModelLocator.GetParameter(item.PropertyName));

                //Reading Vehicle Settings
                foreach (ParameterItem parameterItem in parameterVehicleSetting.ParameterVehicleList)
                {
                    BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameter(parameterItem.PropertyName));
                }

                //Reading Vehicle Health
                foreach (ParameterItem parameterItem in parameterVehicleHealth.ParameterVehicleList)
                {
                    BuildCommunicationsList(pageParameters.parameterList, App.ViewModelLocator.GetParameter(parameterItem.PropertyName));
                }

                await communicationsReadWithWait(pageParameters); //blocking communications read packet sent

                //Add parameter's values into vehicleSettings Dictionary
                foreach (ParameterItem parameterItem in parameterVehicleSetting.ParameterVehicleList)
                {
                    vehicleSettings.Add(App.ViewModelLocator.GetParameter(parameterItem.PropertyName).Name, App.ViewModelLocator.GetParameter(parameterItem.PropertyName).parameterValue);
                }

                vehicleHealth.Add("MotorController", App.PresentConnectedController);
                vehicleHealth.Add("odometer", App.ViewModelLocator.GetParameter("OdometerX100Miles_q0High").parameterValue * 65536 + (float)((uint)App.ViewModelLocator.GetParameter("OdometerX100Miles_q0Low").parameterValue));
                
                foreach (ParameterItem parameterItem in parameterVehicleHealth.ParameterVehicleList)
                {
                    if(parameterItem.ViewType == ParameterItem.ViewTypes.Enum)
                        vehicleHealth.Add(App.ViewModelLocator.GetParameter(parameterItem.PropertyName).Name, App.ViewModelLocator.GetParameter(parameterItem.PropertyName).parameterEnumString);
                    else if(parameterItem.ViewType == ParameterItem.ViewTypes.String)
                        vehicleHealth.Add(App.ViewModelLocator.GetParameter(parameterItem.PropertyName).Name, App.ViewModelLocator.GetParameter(parameterItem.PropertyName).parameterValueString);
                    else
                        vehicleHealth.Add(App.ViewModelLocator.GetParameter(parameterItem.PropertyName).Name, App.ViewModelLocator.GetParameter(parameterItem.PropertyName).parameterValue);
                }
            }
            try
            {
                App.ParseManagerAdapter.Initialize();
                App.ParseManagerAdapter.InitParametersList(App.PresentConnectedController);
                if (ControllerTypeLocator.ControllerType == "TAC")
                {
                    App.MemberOfLists = await App.ParseManagerAdapter.UpdateVehicleParseClass("Vehicles", vehicleHealth, vehicleSettings);
                    //await App.ParseManagerAdapter.TransmitSupplierRoleClass("OEMs", vehicleHealth);
                }
                else
                {
                    App.MemberOfLists = await App.ParseManagerAdapter.UpdateVehicleParseClass("TSXVehicles", vehicleHealth, vehicleSettings);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("VehcileData exception: " + e.ToString());
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                //activityIndicator.IsRunning = false;
            });
        }

    }
}
