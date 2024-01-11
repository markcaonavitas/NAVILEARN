using ParseManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAccountPage : ContentPage
    {
        public ObservableCollection<Driver> Drivers {get; set;}
        public ObservableCollection<Vehicle> Vehicles { get; set; }

        public string currentUsername { get { return Authentication.GetUserCredentials()["UserName"]; } }

        public string accessLevel { get { return Authentication.GetUserCredentials()["AccessLevel"]; } }

        private string controllerName = App.PresentConnectedController;
        public bool IsRegistered { get { return Authentication.GetUserCredentials()["ListOfRegisterControllers"].Contains(App.PresentConnectedController); } }
        public bool IsRealController { get { return !(App._MainFlyoutPage._DeviceListPage._device is DemoDevice); } }
        public bool IsRegisterePersonalVehicleEnabled
        {
            get
            {
                return (App.ViewModelLocator.MainViewModel.UserLevelEqualOrGreaterThanDealerProperty ||
                        App.ViewModelLocator.MainViewModelTSX.UserLevelEqualOrGreaterThanDealerProperty) &&
                      !IsRegistered &&
                      !App._MainFlyoutPage._DeviceListPage._device.HasRegisteredUsersButNotYou;
            }
        }
        public string NewUserInput { get; set; }
        public ICommand RemoveUsernameCommand => new Command(RemoveUser);
        public ICommand RemoveUserOwnershipCommand => new Command(RemoveUserOwnership);
        
        public ICommand EditVehicleCommand => new Command(EditVehicle);
        public ICommand SaveVehicleCommand => new Command(SaveVehicle);
        public bool isDataEmpty { get; set; } = Authentication.GetUserCredentials()["ListOfRegisterControllers"] == "";

        public MyAccountPage()
        {
            InitializeComponent();
            if (Authentication.GetUserCredentials()["ListOfRegisterControllers"].Length != 0)
            {
                GetMotorControllerList();
            }
            GetRegisteredMotorControllerUsersList();
            if (IsRegisterePersonalVehicleEnabled)
            {
                AddRegisteredUsersStackLayout.IsVisible = true;
                usernameEntry.IsVisible = false;
                dealerRegisterFrame.IsVisible = true;
            }
            else
            {
                if (IsRegistered)
                {
                    AddRegisteredUsersStackLayout.IsVisible = true;
                    dealerRegisterFrame.IsVisible = false;
                    usernameEntry.IsVisible = true;
                    usernameEntry.Placeholder = "Enter new username";
                }
            }
        }

        private void GetMotorControllerList()
        {
            try
            {
                Vehicles = new ObservableCollection<Vehicle>();
                string vehicleName = Regex.Replace(Authentication.GetUserCredentials()["ListOfRegisterControllers"], @"(?<=\d)(\B|,)(?=[T])", ",");
                string[] vehicles = Regex.Split(vehicleName, @"[,]");
                var nicknames = Authentication.GetUserCredentials().ContainsKey("NickName") ? Authentication.GetUserCredentials()["NickName"].Split(',').ToList() : new List<string>();

                for (int i = 0; i < vehicles.Count(); i++)
                {
                    var nickname = "";
                    
                    if (i < nicknames.Count)
                        nickname = (nicknames[i] != "") ? nicknames[i] : "";

                    Vehicles.Add(new Vehicle() { Name = vehicles[i], NickName = nickname});
                }

                if (vehicles.Count() != 0)
                {
                    VehicleNameListView.IsVisible = true;
                    VehicleNameListView.ItemsSource = Vehicles;
                    VehicleNameListView.HeightRequest = 80 * vehicles.Count();
                }   
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetMotorControllerList: " + e.Message);
            }
        }

        private async Task GetRegisteredMotorControllerUsersList()
        {
            try
            {
                Drivers = new ObservableCollection<Driver>();
                IList<string> RegisteredMotorControllerUsersList = await App.ParseManagerAdapter.GetRegisteredMotorControllerUsersList(App.PresentConnectedController);
                foreach (var username in RegisteredMotorControllerUsersList)
                {
                    Drivers.Add(new Driver() { DisplayName = username });
                }
                AprovedDriversListView.ItemsSource = Drivers;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("GetRegisteredMotorControllerUsersList: No Internet Connection " + e.Message);
            }
        }

        public void AddUsernameCommandAsTapGesture(object sender, System.EventArgs e)
        {
            AddUser();
        }

        public void AddUserCompleted(object sender, EventArgs e)
        {
            //Coming here before the text was wiped out
            //Debug.WriteLine("Add user Completed");
            hasDoneButtonBeenPressed = true;
            AddUser();
        }

        private async void AddUser()
        {
            bool isUpdate = false;
            bool isExist = false;
            try
            {
                if(IsRegisterePersonalVehicleEnabled)
                {
                    OnRegisterPersonalVehicleClicked();
                }
                else
                {
                    string userName = NewUserInput;
                    if (userName != null && userName != "")
                    {
                        //first find a user if he exist in db
                        isExist = await App.ParseManagerAdapter.CheckUsernameExist(userName);

                        if (isExist)
                        {
                            //then add him into the registerdControllerUser list
                            isUpdate = await App.ParseManagerAdapter.AddUsernameToRegisteredMotorControllerUsers(controllerName, userName);
                            if (isUpdate)
                            {
                                await DisplayAlert("Success", $"{userName} has been added successfully to approved user list !", "Continue");
                                Drivers.Add(new Driver() { DisplayName = userName });
                            }
                            else
                            {
                                await DisplayAlert("Warning", $"{userName} has already been added", "OK");
                            }
                            usernameEntry.Text = "";
                        }
                        else
                        {
                            await DisplayAlert("Failed", $"Username {userName} does not exist. Check the spelling. Username is case sensitive", "Try again");
                            return;
                        }
                    }
                    else
                    {
                        await DisplayAlert("Empty Field Error", "Username is empty", "Close");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("System Error", "Something went wrong when connecting to the server", "OK");
            }
        }

        private async void RemoveUser(object o)
        {
            var driverBeingRemoved = o as Driver;
            bool confirmDelete = false;
            bool isRemoved = false;
            bool isSuccess = false;

            try
            {
                confirmDelete = await DisplayAlert("Confirm Delete", $"Are you sure you want to delete {driverBeingRemoved.DisplayName} ?", "Yes", "No");
                if (confirmDelete)
                    isRemoved = Drivers.Remove(Drivers.SingleOrDefault(d => d.DisplayName == driverBeingRemoved.DisplayName));
                else
                    return;

                if (isRemoved)
                    isSuccess = await App.ParseManagerAdapter.DeleteUserInfoFromVehicleRecord(controllerName, driverBeingRemoved.DisplayName);

                if (isSuccess)
                    await DisplayAlert("Successful", $"Driver {driverBeingRemoved.DisplayName} was deleted sucessfully", "OK");
                else
                    await DisplayAlert("Fail", $"Delete Driver {driverBeingRemoved.DisplayName} was failed", "Close");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("RemoveUser exception: " + e.Message);
            }
        }

        bool hasDoneButtonBeenPressed = false;
        private void InputEntryUnfocused(object sender, FocusEventArgs e)
        {
            hasDoneButtonBeenPressed = false; //reset
            //Debug.WriteLine("Entry input unfoceused");
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                //Wipe out the uncompleted text after 1 seconds
                if (!hasDoneButtonBeenPressed)
                {
                    (sender as Entry).Text = ""; 
                }
                return false;
            });
        }

        public async void AddNickNameCompleted(object sender, EventArgs e)
        {
            hasDoneButtonBeenPressed = true;
            var vehicle = Vehicles.FirstOrDefault(v => v.Name == ((sender as Entry).BindingContext as Vehicle).Name);
            vehicle.couldBeDirtyBecauseKBDoneWasNotUsed = false;
            vehicle.NickName = (sender as Entry).Text;
            await DisplayAlert("Success", $"The nickname {vehicle.NickName} has been added", "OK");
            (sender as Entry).IsVisible= false;
            await ModifyInternalAndExternalVehicleInfo(vehicle);
        }
        private void OnRegisterPersonalVehicleClicked()
        {
            MessagingCenter.Send<MyAccountPage>(this, "ShowGaugePage");
        }
        private async void RemoveUserOwnership(object o)
        {
            var vehicleNameBeingRemoved = o as Vehicle;
            bool confirmDelete = false;
            bool isSuccess = false;

            try
            {
                confirmDelete = await DisplayAlert("Confirm Delete", $"Are you sure you want to remove your ownership of this vehicle {vehicleNameBeingRemoved.Name} ?", "Yes", "No");
                if (confirmDelete)
                {
                    if (await App.ParseManagerAdapter.DeleteUserInfoFromVehicleRecord(vehicleNameBeingRemoved.Name, currentUsername))
                    {
                        var persistentRegisterControllers = Regex.Replace(Authentication.GetUserCredentials()["ListOfRegisterControllers"], @"(?<=\d)(\B|,)(?=[T])", ",").Split(',').ToList();
                        var persistentControllerNicknames = Authentication.GetUserCredentials().ContainsKey("NickName") ? Authentication.GetUserCredentials()["NickName"].Split(',').ToList() : new List<string>();

                        var index = persistentRegisterControllers.Select((name, i) => new { name, i }).FirstOrDefault(obj => obj.name == vehicleNameBeingRemoved.Name)?.i ?? -1;

                        if(index != -1)
                        {
                            persistentRegisterControllers.RemoveAt(index);
                            Authentication.SaveUserListOfRegisterControllers(FileManager.ConcatenateString(persistentRegisterControllers));
                            
                            persistentControllerNicknames.RemoveAt(index);
                            Authentication.SaveNickName(FileManager.ConcatenateString(persistentControllerNicknames));
                        }
                       
                        isSuccess = Vehicles.Remove(Vehicles.SingleOrDefault(d => d.Name == vehicleNameBeingRemoved.Name));
                  
                        VehicleNameListView.HeightRequest -= 50;
                    }
                }
                else
                    return;

                if (isSuccess)
                    await DisplayAlert("Successful", $"Your ownership of this vehicle {vehicleNameBeingRemoved.Name} was removed sucessfully", "OK");
                else
                    await DisplayAlert("Fail", $"Remove ownership of this vehicle {vehicleNameBeingRemoved.Name} was failed", "Close");

                //var result = await App.ParseManagerAdapter.DeleteKey(vehicleNameBeingRemoved.Name, "NickName");
                //if (result)
                //    await DisplayAlert("Success", "Key deleted", "Close");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("RemoveUserOwnership exception: " + e.Message);
            }
        }
        private void EditVehicle(object o)
        {
            var vehicle = o as Vehicle;
            popupEditNickName.IsVisible = true;
            popupEditNickName.BindingContext= vehicle;
        }

        private async void SaveVehicle(object o)
        {
            var vehicle = (o as Vehicle);
            var newNickName = entEditNickName.Text;

            if (newNickName.Length <= 30)
            {
                if (newNickName != vehicle.NickName)
                {
                    if (newNickName == "")
                    {
                        //Delete nickname by an empty space
                        var isDeletionConfirmed = await DisplayAlert("Delete NickName", $"Are you sure you want to delete this nick name", "Yes", "No");
                        if (!isDeletionConfirmed)
                        {
                            popupEditNickName.IsVisible = false;
                            return;
                        }
                    }

                    vehicle.couldBeDirtyBecauseKBDoneWasNotUsed = false;
                    vehicle.NickName = newNickName;
                    popupEditNickName.IsVisible = false;
                    await ModifyInternalAndExternalVehicleInfo(vehicle);
                }
                else
                {
                    await DisplayAlert("Warning", $"The nick name that you have entered is identical with the existing one", "Try again");
                }
                
            }else
                await DisplayAlert("String length too long", $"Your nickname has exceeded the maxium number of 30 characters", "Try again");
        }
        private void CancelEditVehicle(object sender, EventArgs e)
        {
            popupEditNickName.IsVisible = false;
        }
        private async Task<bool> ModifyInternalAndExternalVehicleInfo(Vehicle vehicle)
        {
            int index = Vehicles.Select((v, i) => new { v, i }).
                    FirstOrDefault(obj => obj.v.Name == vehicle.Name)?.i ?? -1;

            var nicknames = Authentication.GetUserCredentials().ContainsKey("NickName") ? Authentication.GetUserCredentials()["NickName"].Split(',').ToList() : new List<string>();
            if (nicknames.Count > index)
                nicknames[index] = vehicle.NickName;
            else
                nicknames.Add(vehicle.NickName);

            Authentication.SaveNickName(FileManager.ConcatenateString(nicknames));

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                //Call parse API to save nick name
                var isNickNameSaved = await App.ParseManagerAdapter.SaveNickName(vehicle.Name, vehicle.NickName);
                if (isNickNameSaved)
                {
                    await DisplayAlert("Success", "Your nick name has been updated!", "Close");
                    return true;
                }
                else
                    await DisplayAlert("Error", "Nick name failed to save. Pleas try again!", "OK");
            }

            return false;
        }
        
        private void AddNickNameFocused(object sender, FocusEventArgs e)
        {
            ((sender as Entry).BindingContext as Vehicle).couldBeDirtyBecauseKBDoneWasNotUsed = true;
        }
        private void EditNickNameFocused(object sender, FocusEventArgs e)
        {
            ((sender as Entry).BindingContext as Vehicle).couldBeDirtyBecauseKBDoneWasNotUsed = true;
        }
        private async void BackbuttonClicked(object sender, EventArgs e)
        {
            Authentication.AlreadyCheckingCredentials = false; //in case it was dismissed then set this
            await Navigation.PopAsync();
        }
    }

    public class Driver : ViewModelBase
    {
        private string _displayName = "";
        public string DisplayName 
        { 
            get { return _displayName; }
            set { SetProperty(ref _displayName, value); } 
        }
    }

    public class Vehicle : ViewModelBase
    {
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value);}
        }

        private string _nickName = "";
        public string NickName
        {
            get { return _nickName; }
            set
            {
               if (!couldBeDirtyBecauseKBDoneWasNotUsed)
                {
                    SetProperty(ref _nickName, value);
                }
            }
        }
        public bool couldBeDirtyBecauseKBDoneWasNotUsed { get; set; } = false;
    }

    public class VehicleComparer : IEqualityComparer<Vehicle>
    {
        // Vehicles are equal if their names and vehicle nicknames are equal.
        public bool Equals(Vehicle x, Vehicle y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the vehicles' properties are equal.
            return x.Name == y.Name && x.NickName == y.NickName;
        }

        public int GetHashCode(Vehicle vehicle)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(vehicle, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashVehicleName = vehicle.Name == null ? 0 : vehicle.Name.GetHashCode();

            //Get hash NickName for the NickName field.
            int hashVehicleNickName = vehicle.NickName == null ? 0 : vehicle.NickName.GetHashCode();

            //Calculate the hash code for the vehicle.
            return hashVehicleName ^ hashVehicleNickName;
        }
    }
}