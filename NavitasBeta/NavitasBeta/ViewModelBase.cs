using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NavitasBeta
{
    //from https://daedtech.com/wpf-and-notifying-property-change/
    //By way of background, for anyone not familiar or looking for a refresher, 
    //this interface allows XAML bindings to keep in sync with the C# class. 
    //For instance, if you bound a XAML text box to a class property called “MyText”, 
    //the class containing MyText would implement INotifyPropertyChanged. 
    //This interface consists of just a single event, and properties fire the event when set. 
    //This is the mechanism by which the model (the thing being bound to) notifies the GUI 
    //that it should ask for an updated value. Without this, the GUI would display 
    //whatever value MyText had when it was loaded, and it would ignore subsequent changes 
    //that came from anywhere but the user modifying the text.
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T storage, T value, bool couldBeDirtyBecauseKBDoneWasNotUsed = false, [CallerMemberName] string propertyName = null)
        {
            //Wow,ideally we don't want to issue property change when the value has not
            //ultimately things like entering a number without hitting done will just stay
            //as the number you entered and since the property was not actually changed
            //we will never overwrite it with the correct value, so....
            //RG Oct 2020, enabled if changed only statement
            //otherwise huge callbacks like battery type changed are called all the time
            if (Object.Equals(storage, value) && !couldBeDirtyBecauseKBDoneWasNotUsed)
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            try
            {
                if (handler != null)
                {
#if CONSOLE_WRITE
                System.Diagnostics.Debug.WriteLine("OnPropertyChanged");
#endif
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Debug this exception: ViewModelBase.xaml.cs" + ex.Message);
            }
        }
    }
}
