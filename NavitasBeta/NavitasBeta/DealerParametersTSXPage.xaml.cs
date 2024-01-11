using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DealerParametersTSXPage : ContentPage
    {
        public event EventHandler<SetParameterEventArgs> AddParamValuesToQueue = delegate { };
        SetParameterEventArgs SetParameterEventArgs;

        string[] TopSpeeds = { "13 Miles/Hour", "19 Miles/Hour", "25 Miles/Hour", "35 Miles/Hour" };

        public DealerParametersTSXPage()
        {
            InitializeComponent();
            if (Device.OS == TargetPlatform.iOS)
            {
                MessagingCenter.Subscribe<DeviceComunication>(this, "Hi", (sender) =>
                {
                    // do something whenever the "Hi" message is sent
                    DisplayMessage();
                });
            }

        }

        void DisplayMessage()
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                this.DisplayAlert("Error", "Device Communication timeout", "OK");
            });
        }
        void OnButtonClicked(object sender, EventArgs args)
        {

            Button button = (Button)sender;

            if (button == VehilceSpeedLimitButton)
            {
                Task<string> task = DisplayActionSheet("Please Select Top Speed", "Cancel", null, TopSpeeds);

                task.ContinueWith(TireSizeActionSheetCallback);

            }
        }

        void TireSizeActionSheetCallback(Task<string> task)
        {
            string result = task.Result;

            switch (result)
            {
                case "13 Miles/Hour":
                    System.Diagnostics.Debug.WriteLine("13 Miles/Hour");
                    QueParameter(154, 13.0f);
                    break;
                case "19 Miles/Hour":
                    System.Diagnostics.Debug.WriteLine("19 Miles/Hour");
                    QueParameter(154, 19.0f);

                    break;
                case "25 Miles/Hour":
                    System.Diagnostics.Debug.WriteLine("25 Miles/Hour");
                    QueParameter(154, 25.0f);
                    break;
                case "35 Miles/Hour":
                    System.Diagnostics.Debug.WriteLine("35 Miles/Hour");
                    QueParameter(154, 35.0f);
                    break;
            }
        }

        private void QueParameter(byte ParameterNumber, float ParameterValue)
        {
            SetParameterEventArgs SetParameterEventArgs = new SetParameterEventArgs(ParameterNumber, ParameterValue, null);
            AddParamValuesToQueue(this, SetParameterEventArgs);

        }

#if OLD_CODE
        bool TireSizeNumeric_BlockFirstTime = true;

        private void TireSizeNumericUpDown_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            float value = Convert.ToDouble(e.Value);
            System.Diagnostics.Debug.WriteLine("TireSizeNumericUpDown_ValueChanged value = " + value);
            //       System.Diagnostics.Debug.WriteLine("TIRERADUIS = " + ViewModelLocator.MainViewModel.TIRERADIUS.ToString());

            //      if ((value == ViewModelLocator.MainViewModel.TIRERADIUS) && TireSizeNumeric_BlockFirstTime)
            //        {

            TireSizeNumericUpDown.RemoveBinding(Syncfusion.SfNumericUpDown.XForms.SfNumericUpDown.ValueProperty);
            //    TireSizeNumeric_BlockFirstTime = false;
            //        System.Diagnostics.Debug.WriteLine("binding removed");
            //       }

            if (!TireSizeNumeric_BlockFirstTime)
            {
                System.Diagnostics.Debug.WriteLine("value = " + value.ToString());
                //                System.Diagnostics.Debug.WriteLine("PreviousValue = " + PreviousValue.ToString());
                //                float RawValue = (float)GetRawValue(0, 105, 0, 4095, value);
                //              System.Diagnostics.Debug.WriteLine("RawValue = " + RawValue.ToString());
                    SetParameterEventArgs = new SetParameterEventArgs(150, (float)value / 2, null);
                AddParamValuesToQueue(this, SetParameterEventArgs);

            }

            TireSizeNumeric_BlockFirstTime = false;


        }



        bool RearAxleRatio_BlockFirstTime = true;

        private void RearAxleRatioNumericUpDown_ValueChanged(object sender, Syncfusion.SfNumericUpDown.XForms.ValueEventArgs e)
        {
            float value = Convert.ToDouble(e.Value);
            System.Diagnostics.Debug.WriteLine("RearAxleRatioNumericUpDown_ValueChanged value = " + value);

            RearAxleRatioNumericUpDown.RemoveBinding(Syncfusion.SfNumericUpDown.XForms.SfNumericUpDown.ValueProperty);
            if (!RearAxleRatio_BlockFirstTime)
            {
                System.Diagnostics.Debug.WriteLine("value = " + value.ToString());
                    SetParameterEventArgs = new SetParameterEventArgs(151, (float)value, null);

                AddParamValuesToQueue(this, SetParameterEventArgs);

            }

            RearAxleRatio_BlockFirstTime = false;
        }
#endif 

        private void SaveChangesButton_Clicked(object sender, EventArgs e)
        {
                QueParameter(199, 1.0f);
        }

        private void TireSizePlusButton_Clicked(object sender, EventArgs e)
        {

            if (ViewModelLocator.MainViewModelTSX.TIREDIAMETER < 30.0)
            {
                SetParameterEventArgs = new SetParameterEventArgs(150, (float)(ViewModelLocator.MainViewModelTSX.TIREDIAMETER + 0.1) / 2, null);
                AddParamValuesToQueue(this, SetParameterEventArgs);
            }
        }

        private void TireSizeMinusButton_Clicked(object sender, EventArgs e)
        {

            if (ViewModelLocator.MainViewModelTSX.TIREDIAMETER > 7.0)
            {
                SetParameterEventArgs = new SetParameterEventArgs(150, (float)(ViewModelLocator.MainViewModelTSX.TIREDIAMETER - 0.1) / 2, null);
                AddParamValuesToQueue(this, SetParameterEventArgs);
            }

        }

        private void RearAxlePlusButton_Clicked(object sender, EventArgs e)
        {
            if (ViewModelLocator.MainViewModelTSX.REARAXLERATIO < 20.0)
            {
                SetParameterEventArgs = new SetParameterEventArgs(151, (float)(ViewModelLocator.MainViewModelTSX.REARAXLERATIO + 0.1), null);
                AddParamValuesToQueue(this, SetParameterEventArgs);
            }

        }

        private void RearAxleMinusButton_Clicked(object sender, EventArgs e)
        {
            if (ViewModelLocator.MainViewModelTSX.REARAXLERATIO > 6.0)
            {
                SetParameterEventArgs = new SetParameterEventArgs(151, (float)(ViewModelLocator.MainViewModelTSX.REARAXLERATIO - 0.1), null);
                AddParamValuesToQueue(this, SetParameterEventArgs);
            }

        }
    }
}