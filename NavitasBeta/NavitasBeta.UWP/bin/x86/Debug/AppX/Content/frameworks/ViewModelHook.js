//EventTarget is used like c#/xaml INotifyPropertyChanged is a C# interface forcing this class to implement the following PropertyChangedEventHandler
//from https://daedtech.com/wpf-and-notifying-property-change/
//By way of background, for anyone not familiar or looking for a refresher, 
//this interface allows XAML or HTML bindings to keep in sync with the C# or Javascript class. 
//For instance, if you bound a XAML or HTML text box to a class property called “MyText”, 
//the class containing MyText would implement INotifyPropertyChanged. 
//This interface consists of just a single event, and properties fire the event when set. 
//This is the mechanism by which the model (the thing being bound to) notifies the GUI 
//that it should ask for an updated value. Without this, the GUI would display 
//whatever value MyText had when it was loaded, and it would ignore subsequent changes 
//that came from anywhere but the user modifying the text.
class ViewModelHook extends EventTarget //public class ViewModelBase : INotifyPropertyChanged
{
    constructor()
    { //name) {
        super();
        var _this = this;
        var event = new CustomEvent("PropertyChanged",
            { //event PropertyChangedEventHandler PropertyChanged;
                detail:
                {
                    Property: _this.PropertyName + "changed and not used yet",
                    time: new Date(),
                },
                bubbles: true,
                cancelable: true
            });

        this.SetProperty = function (objReference, value, objPropertyNameString) //protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            //if (Object.Equals(storage, value))
            //  return false;
            objReference["_" + objPropertyNameString] = value;
            this.OnPropertyChanged(objPropertyNameString);
            return true;
        }

        this.OnPropertyChanged = function (objPropertyNameString) //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = 1; //PropertyChangedEventHandler handler = PropertyChanged;
            try
            {
                if (handler != null)
                {

                    //console.log("OnPropertyChanged");
                    this.dispatchEvent(event); //PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }

            }
            catch (ex)
            {

                console.log("Debug this exception: ViewModelHook.xaml.cs" + ex.Message); //System.Diagnostics.Debug.WriteLine("Debug this exception: ViewModelBase.xaml.cs" + ex.Message);
            }
        }
    }
}
