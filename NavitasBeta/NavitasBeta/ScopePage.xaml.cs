using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScopePage : NavitasGeneralPage
    {
        LineSeries CH1_L1;
        LineSeries L1;
        LineSeries L2;
        LineSeries L3;
        LineSeries L4;

        PlotView pv;
        LinearAxis A1;
        LinearAxis A2;
        LinearAxis B1;
        LinearAxis B2;
        LinearAxis C1;
        LinearAxis C2;
        LinearAxis D1;
        LinearAxis D2;

        PlotView pvch1;
        LinearAxis ch1_A1;
        LinearAxis ch1_A2;
        PlotView pvch2;
        LinearAxis ch2_A1;
        LinearAxis ch2_A2;
        PlotView pvch3;
        LinearAxis ch3_A1;
        LinearAxis ch3_A2;
        PlotView pvch4;
        LinearAxis ch4_A1;
        LinearAxis ch4_A2;

        float ch1Scale = 1;
        float ch2Scale = 1;
        float ch3Scale = 1;
        float ch4Scale = 1;
        float triggerScale = 1;

        ObservableCollection<ParameterFileItem> FileParameters;
        ActivityIndicator activityIndicator;

        public ScopePage()
        {
            try
            {
                InitializeComponent();
                LoadCommunicationItemsTAC();
                AddActivityPopUp();
                activityIndicator = new ActivityIndicator();
                App.ViewModelLocator.GetParameter("DATASCOPECH1SELECT").ReCalculate += ((parameter) => { UpdateChannelPointerParameter("DATASCOPECH1SELECT", parameter); });
                App.ViewModelLocator.GetParameter("DATASCOPECH2SELECT").ReCalculate += ((parameter) => { UpdateChannelPointerParameter("DATASCOPECH2SELECT", parameter); });
                App.ViewModelLocator.GetParameter("DATASCOPECH3SELECT").ReCalculate += ((parameter) => { UpdateChannelPointerParameter("DATASCOPECH3SELECT", parameter); });
                App.ViewModelLocator.GetParameter("DATASCOPECH4SELECT").ReCalculate += ((parameter) => { UpdateChannelPointerParameter("DATASCOPECH4SELECT", parameter); });
                App.ViewModelLocator.GetParameter("DATASCOPETRIGGERADDRESS").ReCalculate += ((parameter) => { UpdateChannelPointerParameter("DATASCOPETRIGGERADDRESS", parameter); });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: ScopePage.xaml.cs" + ex.Message);
            }

            BuildDoneButtonWarningTextBox();
            OnFirstAppearing();
        }

        Grid grid_oxyplot;
        Grid grid_channels;


        protected void OnFirstAppearing()
        {
            System.Diagnostics.Debug.WriteLine("Scope OnAppearing");
            System.Diagnostics.Debug.WriteLine("Scope OnAppearing stacklayout_oxyplot.Children.Count = " + stacklayout_oxyplot.Children.Count.ToString());

            Device.BeginInvokeOnMainThread(() =>
            {

                pv = new PlotView();
                pv.VerticalOptions = LayoutOptions.Fill;
                pv.HorizontalOptions = LayoutOptions.Fill;
                pv.Model = new PlotModel();
                pv.Model.Title = "Scope";
                A1 = new LinearAxis();
                A1.Position = AxisPosition.Bottom;
                A1.Title = "Time (s)";
                A1.Minimum = 0;
                //A1.Maximum = 255;


                A2 = new LinearAxis();
                A2.Position = AxisPosition.Left;
                //A2.Title = "Value1";
                //  A2.TitleColor = OxyColors.Green;
                A2.Minimum = 1;
                A2.Maximum = 65536;
                //        A2.AxisTickToLabelDistance = 150;
                //     A2.TicklineColor = OxyColors.Green;
                A2.TextColor = OxyColors.Green;
                //     A2.AxislineColor = OxyColors.Green;
                A1.IsZoomEnabled = true;
                A2.IsZoomEnabled = false;


                pv.Model.Axes.Add(A1);
                pv.Model.Axes.Add(A2);
                L1 = new LineSeries();
                L1.Title = "Channel 1";
                A2.Key = "Value1";
                L1.YAxisKey = "Value1";
                pv.Model.Series.Add(L1);

                B1 = new LinearAxis();
                B1.Position = AxisPosition.Bottom;
                B1.Title = "Time b";
                B1.Minimum = 0;
                //B1.Maximum = 255;

                B2 = new LinearAxis();
                B2.Position = AxisPosition.Left;
                //B2.Title = "Value2";
                B2.Minimum = 1;
                B2.Maximum = 65536;
                B2.AxisTickToLabelDistance = 50;
                B2.TextColor = OxyColors.DarkOrange;
                //      B2.AxislineColor = OxyColors.Orange;
                //pv.Model.Axes.Add(B1);
                pv.Model.Axes.Add(B2);

                L2 = new LineSeries();
                L2.Title = "Channel 2";
                B2.Key = "Value2";
                L2.YAxisKey = "Value2";
                pv.Model.Series.Add(L2);
                B1.IsZoomEnabled = true;
                B2.IsZoomEnabled = false;


                C1 = new LinearAxis();
                C1.Position = AxisPosition.Bottom;
                C1.Title = "Time c";
                C1.Minimum = 0;
                //C1.Maximum = 255;

                C2 = new LinearAxis();
                C2.Position = AxisPosition.Left;
                //C2.Title = "Value3";
                C2.Minimum = 1;
                C2.Maximum = 65536;
                C2.AxisTickToLabelDistance = 100;
                C2.TextColor = OxyColors.Red;
                //       C2.AxislineColor = OxyColors.Red;
                //pv.Model.Axes.Add(C1);
                pv.Model.Axes.Add(C2);
                C1.IsZoomEnabled = true;
                C2.IsZoomEnabled = false;

                L3 = new LineSeries();
                L3.Title = "Channel 3";
                C2.Key = "Value3";
                L3.YAxisKey = "Value3";
                pv.Model.Series.Add(L3);


                D1 = new LinearAxis();
                D1.Position = AxisPosition.Bottom;
                D1.Title = "Time d";
                D1.Minimum = 0;
                //D1.Maximum = 255;

                D2 = new LinearAxis();
                D2.Position = AxisPosition.Left;
                //D2.Title = "Value4";
                D2.Minimum = 1;
                D2.Maximum = 65536;
                D2.TextColor = OxyColors.Navy;
                D2.AxisTickToLabelDistance = 150;
                //      D2.AxislineColor = OxyColors.Blue;
                //pv.Model.Axes.Add(D1);
                pv.Model.Axes.Add(D2);
                D1.IsZoomEnabled = true;
                D2.IsZoomEnabled = false;

                L4 = new LineSeries();
                L4.Title = "Channel 4";
                D2.Key = "Value4";
                L4.YAxisKey = "Value4";
                pv.Model.Series.Add(L4);
                grid_oxyplot = new Grid();

                grid_oxyplot.RowDefinitions.Add(new RowDefinition { Height = new GridLength(600, GridUnitType.Absolute) });
                //         grid_oxyplot.ColumnDefinitions.Add(new ColumnDefinition());
                //          grid_oxyplot.ColumnDefinitions.Add(new ColumnDefinition());
                //         grid_oxyplot.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Auto);
                TransparentView v = new TransparentView();

                grid_oxyplot.Children.Add(pv, 0, 0);
                grid_oxyplot.Children.Add(v, 0, 0);
                //          grid_oxyplot.Children.Add(new Label { Text = "+" }, 0, 0);

                stacklayout_oxyplot.Children.Insert(0, grid_oxyplot);

                pvch1 = new PlotView();
                pvch1.VerticalOptions = LayoutOptions.Fill;
                pvch1.HorizontalOptions = LayoutOptions.Fill;
                pvch1.Model = new PlotModel();
                pvch1.Model.Title = "Channel 1";
                ch1_A1 = new LinearAxis();
                ch1_A1.Position = AxisPosition.Bottom;
                ch1_A1.Title = "Time";
                ch1_A1.Minimum = 0;
                //ch1_A1.Maximum = 255;
                ch1_A1.TextColor = OxyColors.Green;
                ch1_A2 = new LinearAxis();
                ch1_A2.Position = AxisPosition.Left;
                ch1_A2.Minimum = 1;
                ch1_A2.Maximum = 65536;
                // ch1_A2.Title = "Value";
                ch1_A2.TextColor = OxyColors.Green;
                ch1_A2.IsZoomEnabled = true;
                pvch1.Model.Axes.Add(ch1_A1);
                pvch1.Model.Axes.Add(ch1_A2);
                CH1_L1 = new LineSeries();
                CH1_L1.Title = "Channel 1";
                pvch1.Model.Series.Add(CH1_L1);
                grid_channels = new Grid();
                grid_channels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300, GridUnitType.Absolute) });



                //       grid_channels.ColumnDefinitions.Add(new ColumnDefinition());
                //        grid_channels.ColumnDefinitions.Add(new ColumnDefinition());
                //      grid_channels.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Auto);
                grid_channels.Children.Add(pvch1, 0, 0);
                TransparentView v1 = new TransparentView();
                grid_channels.Children.Add(v1, 0, 0);
                //        grid_channels.Children.Add(new Label { Text = "+" }, 0, 0);


                pvch2 = new PlotView();
                pvch2.VerticalOptions = LayoutOptions.Fill;
                pvch2.HorizontalOptions = LayoutOptions.Fill;
                pvch2.Model = new PlotModel();
                pvch2.Model.Title = "Channel 2";
                ch2_A1 = new LinearAxis();
                ch2_A1.Position = AxisPosition.Bottom;
                ch2_A1.Title = "Time";
                ch2_A1.Minimum = 0;
                //ch2_A1.Maximum = 255;
                ch2_A1.TextColor = OxyColors.DarkOrange;
                ch2_A2 = new LinearAxis();
                ch2_A2.Position = AxisPosition.Left;
                ch2_A2.Minimum = 1;
                ch2_A2.Maximum = 65536;
                //      ch2_A2.Title = "Value";
                ch2_A2.TextColor = OxyColors.DarkOrange;
                ch2_A2.IsZoomEnabled = true;
                pvch2.Model.Axes.Add(ch2_A1);
                pvch2.Model.Axes.Add(ch2_A2);
                LineSeries CH2_L1 = new LineSeries();
                CH2_L1.Title = "Channel 2";
                CH2_L1.Color = OxyColors.DarkOrange;

                pvch2.Model.Series.Add(CH2_L1);
                grid_channels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300, GridUnitType.Absolute) });



                grid_channels.Children.Add(pvch2, 0, 1);
                TransparentView v2 = new TransparentView();
                grid_channels.Children.Add(v2, 0, 1);
                //      grid_channels.Children.Add(new Label { Text = "+" }, 0, 1);


                pvch3 = new PlotView();
                pvch3.VerticalOptions = LayoutOptions.Fill;
                pvch3.HorizontalOptions = LayoutOptions.Fill;
                pvch3.Model = new PlotModel();
                pvch3.Model.Title = "Channel 3";
                ch3_A1 = new LinearAxis();
                ch3_A1.Position = AxisPosition.Bottom;
                ch3_A1.Title = "Time";
                ch3_A1.Minimum = 0;
                //ch3_A1.Maximum = 255;
                ch3_A1.TextColor = OxyColors.Red;
                ch3_A2 = new LinearAxis();
                ch3_A2.Position = AxisPosition.Left;
                ch3_A2.Minimum = 1;
                ch3_A2.Maximum = 65536;
                //    ch3_A2.Title = "Value";
                ch3_A2.TextColor = OxyColors.Red;
                ch3_A2.IsZoomEnabled = true;
                pvch3.Model.Axes.Add(ch3_A1);
                pvch3.Model.Axes.Add(ch3_A2);
                LineSeries CH3_L1 = new LineSeries();
                CH3_L1.Color = OxyColors.Red;
                CH3_L1.Title = "Channel 3";
                pvch3.Model.Series.Add(CH3_L1);
                grid_channels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300, GridUnitType.Absolute) });



                grid_channels.Children.Add(pvch3, 0, 2);
                TransparentView v3 = new TransparentView();
                grid_channels.Children.Add(v3, 0, 2);

                //        grid_channels.Children.Add(new Label { Text = "+" }, 0, 2);

                pvch4 = new PlotView();
                pvch4.VerticalOptions = LayoutOptions.Fill;
                pvch4.HorizontalOptions = LayoutOptions.Fill;
                pvch4.Model = new PlotModel();
                pvch4.Model.Title = "Channel 4";
                ch4_A1 = new LinearAxis();
                ch4_A1.Position = AxisPosition.Bottom;
                ch4_A1.Title = "Time";
                ch4_A1.Minimum = 0;
                //ch4_A1.Maximum = 255;
                ch4_A1.TextColor = OxyColors.Navy;
                ch4_A2 = new LinearAxis();
                ch4_A2.Position = AxisPosition.Left;
                ch4_A2.Minimum = 1;
                ch4_A2.Maximum = 65536;
                //    ch4_A2.Title = "Value";
                ch4_A2.TextColor = OxyColors.Navy;
                ch4_A2.IsZoomEnabled = true;

                pvch4.Model.Axes.Add(ch4_A1);
                pvch4.Model.Axes.Add(ch4_A2);
                LineSeries CH4_L1 = new LineSeries();
                CH4_L1.Title = "Channel 4";
                CH4_L1.Color = OxyColors.Navy;

                pvch4.Model.Series.Add(CH4_L1);
                grid_channels.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300, GridUnitType.Absolute) });
                grid_channels.Children.Add(pvch4, 0, 3);
                TransparentView v4 = new TransparentView();
                grid_channels.Children.Add(v4, 0, 3);
                //      grid_channels.Children.Add(new Label { Text = "+" }, 0, 3);

                stacklayout_oxyplot.Children.Add(grid_channels);
                //     System.Diagnostics.Debug.WriteLine("OnAppearing stacklayout_oxyplot.Children.Count = " + stacklayout_oxyplot.Children.Count.ToString());

            });


            //base.OnAppearing();
        }
        protected override void OnAppearing()
        {
            App._devicecommunication.SetScopeBlockResponseHandler(this.ResponseFromScopeBlock);
            base.OnAppearing();
        }

        int countblocks = 0;

        private void FORCETRIGGER_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                (sender as Button).IsEnabled = true;
                return false;
            });
            GoiParameter parameter = (sender as VisualElement).BindingContext as GoiParameter;
            QueParameter(new SetParameterEventArgs(parameter.Address, (float)17, "never used so get rid of this"));
        }

        private void RUNTRIGGER_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                (sender as Button).IsEnabled = true;
                return false;
            });
            GoiParameter parameter = (sender as VisualElement).BindingContext as GoiParameter;
            QueParameter(new SetParameterEventArgs(parameter.Address, (float)1, "never used so get rid of this"));
        }

        int _NumberOfSamples = 256;
        string[] XPoints;
        double[][] YPoints = new double[4][];


        private void START_Clicked(object sender, EventArgs e)
        {
            //Disable this button
            (sender as Button).IsEnabled = false;
            countblocks = 0;
            //Initialze
            XPoints = new string[_NumberOfSamples];
            for (int i = 0; i < 4; i++)
            {
                YPoints[i] = new double[_NumberOfSamples];
            }
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    (pv.Model.Series[0] as LineSeries).Points.Clear();
                    (pv.Model.Series[1] as LineSeries).Points.Clear();
                    (pv.Model.Series[2] as LineSeries).Points.Clear();
                    (pv.Model.Series[3] as LineSeries).Points.Clear();
                    pv.Model.InvalidatePlot(true);

                    (pvch1.Model.Series[0] as LineSeries).Points.Clear();
                    pvch1.Model.InvalidatePlot(true);
                    (pvch2.Model.Series[0] as LineSeries).Points.Clear();
                    pvch2.Model.InvalidatePlot(true);
                    (pvch3.Model.Series[0] as LineSeries).Points.Clear();
                    pvch3.Model.InvalidatePlot(true);
                    (pvch4.Model.Series[0] as LineSeries).Points.Clear();
                    pvch4.Model.InvalidatePlot(true);


                });


                LargestChannel0YPoint = -1000000;
                SmallestChannel0YPoint = 1000000;
                LargestChannel1YPoint = -1000000;
                SmallestChannel1YPoint = 1000000;
                LargestChannel2YPoint = -1000000;
                SmallestChannel2YPoint = 1000000;
                LargestChannel3YPoint = -1000000;
                SmallestChannel3YPoint = 1000000;

            }
            catch (System.ObjectDisposedException ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: ScopePage.xaml.cs" + ex.Message);
            }
            QueParameter(new SetParameterEventArgs(0x22, 0.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 1.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 2.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 3.0f, null));


            QueParameter(new SetParameterEventArgs(0x22, 4.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 5.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 6.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 7.0f, null));

            QueParameter(new SetParameterEventArgs(0x22, 8.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 9.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 10.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 11.0f, null));

            QueParameter(new SetParameterEventArgs(0x22, 12.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 13.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 14.0f, null));
            QueParameter(new SetParameterEventArgs(0x22, 15.0f, null));
        }

        int iPointIndex = 0;
        int Channel = 0;
        float LargestChannel0YPoint = -1000000;
        float SmallestChannel0YPoint = 1000000;
        float LargestChannel1YPoint = -1000000;
        float SmallestChannel1YPoint = 1000000;
        float LargestChannel2YPoint = -1000000;
        float SmallestChannel2YPoint = 1000000;
        float LargestChannel3YPoint = -1000000;
        float SmallestChannel3YPoint = 1000000;

        public void ResponseFromScopeBlock(object sender, ScopeGetBlockResponseEventArgs e)//List<byte> listOfBytes)
        {
            List<byte> listOfBytes = e.listOfBytesFromResponse;
            countblocks++;
            // write to graph. 
            if (countblocks <= 4)
            {
                if (countblocks == 1)
                    iPointIndex = 0;
                Channel = 0;
            }
            else if ((countblocks > 4) && (countblocks <= 8))
            {
                if (countblocks == 5)
                    iPointIndex = 0;
                Channel = 1;
            }
            else if ((countblocks > 8) && (countblocks <= 12))
            {
                if (countblocks == 9)
                    iPointIndex = 0;
                Channel = 2;
            }
            else if ((countblocks > 12) && (countblocks <= 16))
            {
                if (countblocks == 13)
                    iPointIndex = 0;
                Channel = 3;
            }


            //       Device.BeginInvokeOnMainThread(() =>
            //       {
            float ypoint = 0;
            for (int i = 0; i < listOfBytes.Count; i += 2)
            {
                //#if OUT           
                //          System.Diagnostics.Debug.WriteLine("Channel = " + Channel.ToString() + " ypoint = " + ypoint.ToString());
                switch (Channel)
                {
                    case 0:
                        ypoint = ((((int)((sbyte)listOfBytes[i])) << 8) | (uint)listOfBytes[i + 1]) / ch1Scale;
                        if (ypoint > LargestChannel0YPoint)
                        {
                            LargestChannel0YPoint = ypoint;
                        }
                        if (ypoint < SmallestChannel0YPoint)
                        {
                            SmallestChannel0YPoint = ypoint;
                        }
                        (pvch1.Model.Series[0] as LineSeries).Points.Add(new DataPoint(((float)iPointIndex * ((App.ViewModelLocator.GetParameter("DATASCOPETIMEBASE").parameterValue) / 15.0 / 1000)), ypoint));
                        break;
                    case 1:
                        ypoint = ((((int)((sbyte)listOfBytes[i])) << 8) | (uint)listOfBytes[i + 1]) / ch2Scale;
                        if (ypoint > LargestChannel1YPoint)
                        {
                            LargestChannel1YPoint = ypoint;
                        }
                        if (ypoint < SmallestChannel1YPoint)
                        {
                            SmallestChannel1YPoint = ypoint;
                        }
                        (pvch2.Model.Series[0] as LineSeries).Points.Add(new DataPoint(((float)iPointIndex * ((App.ViewModelLocator.GetParameter("DATASCOPETIMEBASE").parameterValue) / 15.0 / 1000)), ypoint));

                        break;
                    case 2:
                        ypoint = ((((int)((sbyte)listOfBytes[i])) << 8) | (uint)listOfBytes[i + 1]) / ch3Scale;
                        if (ypoint > LargestChannel2YPoint)
                        {
                            LargestChannel2YPoint = ypoint;
                        }
                        if (ypoint < SmallestChannel2YPoint)
                        {
                            SmallestChannel2YPoint = ypoint;
                        }
                        (pvch3.Model.Series[0] as LineSeries).Points.Add(new DataPoint(((float)iPointIndex * ((App.ViewModelLocator.GetParameter("DATASCOPETIMEBASE").parameterValue) / 15.0 / 1000)), ypoint));

                        break;
                    case 3:
                        ypoint = ((((int)((sbyte)listOfBytes[i])) << 8) | (uint)listOfBytes[i + 1]) / ch4Scale;
                        if (ypoint > LargestChannel3YPoint)
                        {
                            LargestChannel3YPoint = ypoint;
                        }
                        if (ypoint < SmallestChannel3YPoint)
                        {
                            SmallestChannel3YPoint = ypoint;
                        }
                        (pvch4.Model.Series[0] as LineSeries).Points.Add(new DataPoint(((float)iPointIndex * ((App.ViewModelLocator.GetParameter("DATASCOPETIMEBASE").parameterValue) / 15.0 / 1000)), ypoint));
                        break;
                }
                //#endif
                if (iPointIndex <= 255)
                {
                    XPoints[iPointIndex] = ((float)iPointIndex * ((App.ViewModelLocator.GetParameter("DATASCOPETIMEBASE").parameterValue)) / 15.0 /1000).ToString("{0.00}");
                    YPoints[Channel][iPointIndex] = ypoint;
                }

                //Re-enable Get log button after Channel 4 plotting done
                if (Channel == 3 && iPointIndex == 255)
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        START.IsEnabled = true;
                    });

                (pv.Model.Series[Channel] as LineSeries).Points.Add(new DataPoint(((float)iPointIndex * ((App.ViewModelLocator.GetParameter("DATASCOPETIMEBASE").parameterValue) / 15.0 / 1000)), ypoint));
                iPointIndex++;
                if (iPointIndex > 256)
                    iPointIndex = 0;
            }



            //this will put a constant value in the middle of the screen
            if (LargestChannel0YPoint == SmallestChannel0YPoint)
            {
                LargestChannel0YPoint += 1;
                SmallestChannel0YPoint -= 1;
            }
            if (LargestChannel1YPoint == SmallestChannel1YPoint)
            {
                LargestChannel1YPoint += 1;
                SmallestChannel1YPoint -= 1;
            }
            if (LargestChannel2YPoint == SmallestChannel2YPoint)
            {
                LargestChannel2YPoint += 1;
                SmallestChannel2YPoint -= 1;
            }
            if (LargestChannel3YPoint == SmallestChannel3YPoint)
            {
                LargestChannel3YPoint += 1;
                SmallestChannel3YPoint -= 1;
            }

            ch1_A2.Maximum = A2.Maximum = LargestChannel0YPoint;
            ch1_A2.Minimum = A2.Minimum = SmallestChannel0YPoint;
            ch2_A2.Maximum = B2.Maximum = LargestChannel1YPoint;
            ch2_A2.Minimum = B2.Minimum = SmallestChannel1YPoint;
            ch3_A2.Maximum = C2.Maximum = LargestChannel2YPoint;
            ch3_A2.Minimum = C2.Minimum = SmallestChannel2YPoint;
            ch4_A2.Maximum = D2.Maximum = LargestChannel3YPoint;
            ch4_A2.Minimum = D2.Minimum = SmallestChannel3YPoint;
            if (iPointIndex == 0)
                A1.Maximum = B1.Maximum = C1.Maximum = D1.Maximum = ch1_A1.Maximum = ch2_A1.Maximum = ch3_A1.Maximum = ch4_A1.Maximum = (float)256 * ((App.ViewModelLocator.GetParameter("DATASCOPETIMEBASE").parameterValue) / 15.0 / 1000);

            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    pv.Model.InvalidatePlot(true);
                });

                switch (Channel)
                {
                    case 0:
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            pvch1.Model.InvalidatePlot(true);
                        });
                        break;
                    case 1:
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            pvch2.Model.InvalidatePlot(true);
                        });
                        break;
                    case 2:
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            pvch3.Model.InvalidatePlot(true);
                        });
                        break;
                    case 3:
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            pvch4.Model.InvalidatePlot(true);
                        });
                        break;
                }
            }
            catch (System.ObjectDisposedException ex)
            {

                System.Diagnostics.Debug.WriteLine("Debug this exception: ScopePage.xaml.cs" + ex.Message);
            }
        }

        public class EventArgs<T> : EventArgs
        {
            public T Value { get; private set; }

            public EventArgs(T val)
            {
                Value = val;
            }
        }

        private void SAVECHANGES_Clicked(object sender, EventArgs e)
        {

            Task<bool> task = DisplayAlert("Save Changes", "Settings will be permanently saved to controller flash", "Yes", "Cancel");

            task.ContinueWith(SaveDismissedCallback);
        }

        void SaveDismissedCallback(Task<bool> task)
        {
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    activityMessage.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                    activityMessage.Text = "Saving...";
                });
                QueParameter(new SetParameterEventArgs(50, 1.0f, null));
            }
        }

        private void SaveScopeSettingsToFile_Clicked(object sender, EventArgs e)
        {

            Task<bool> task = DisplayAlert("Save Changes", "Settings will saved to the Navitas directory of your device", "Yes", "Cancel");

            task.ContinueWith(SaveScopeDismissedCallback);
        }

        void SaveScopeDismissedCallback(Task<bool> task)
        {
            string Result = task.Result ? "Yes" : "Cancel";
            if (Result == "Yes")
            {
                //Keep commented out writer below incase you would like to know how to
                //create a new XML format while developing
                FileParameters = new ObservableCollection<ParameterFileItem>();
                foreach (var v in PageParameters.parameterList)
                {
                    ParameterFileItem fileParameter = new ParameterFileItem
                    {
                        Address = v.Address,
                        PropertyName = v.PropertyName,
                        ParameterValue = v.parameterValue,
                    };

                    FileParameters.Add(fileParameter);
                }
                string fileNamingRevision = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(x => x.PropertyName == "SOFTWAREREVISION").parameterValue.ToString("0.000");

                StreamWriter streamToWrite = new StreamWriter(FileManager.CreateStream(Path.Combine(FileManager.GetNavitasDirectoryPath(), "ScopeSettings_FwRev_" + fileNamingRevision + "_saved_" + DateTime.Now.ToString("s").Replace(":", ".") + ".xml")));
                new XmlSerializer(typeof(ObservableCollection<ParameterFileItem>)).Serialize(streamToWrite, FileParameters);//Writes to the file
                streamToWrite.Dispose();
            }
        }

        private void LoadScopeSettingsFromFile_Clicked(object sender, EventArgs e)
        {
            fileListPopUp.IsVisible = true;
            AddExternalFilesAsButtons();
        }

        private async void UploadToCloud_Clicked(object sender, EventArgs e)
        {
            int childrenCounter = 0;
            Dictionary<string, object> vehicleSettings = new Dictionary<string, object>();
            if (YPoints[0] != null)
            {
                try
                {
                    stacklayout_oxyplot.Children.Add(activityIndicator);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        activityIndicator.IsRunning = true;
                    });
                    App.ParseManagerAdapter.InitParametersList(App.PresentConnectedController);
                    for (int i = 0; i < stacklayout_oxyplot.Children.Count; i++)
                    {
                        //it is ideal if the parameter name were place in order
                        if (stacklayout_oxyplot.Children[i] is Frame)
                        {
                            string logTitle = "";
                            var parameter = (stacklayout_oxyplot.Children[i] as Frame).Content.BindingContext as GoiParameter;
                            if (parameter != null)
                            {//lazy here, there are other stacklayouts with buttons not parameters
                                switch (parameter.Name)
                                {
                                    //Ch1 prefixed incase someone has same parameter in two different channels, Parse server would fail
                                    //on identical keys in an object
                                    case "DATASCOPECH1SELECT":
                                        logTitle = "Ch1 " + L1.Title;
                                        break;
                                    case "DATASCOPECH2SELECT":
                                        logTitle = "Ch2 " + L2.Title;
                                        break;
                                    case "DATASCOPECH3SELECT":
                                        logTitle = "Ch3 " + L3.Title;
                                        break;
                                    case "DATASCOPECH4SELECT":
                                        logTitle = "Ch4 " + L4.Title;
                                        break;
                                }

                            if (childrenCounter < 4)
                                {
                                App.ParseManagerAdapter.AddLoggedParameterData(logTitle, XPoints, YPoints[childrenCounter]);
                                    childrenCounter++;
                                }
                                else if (parameter.Name == "DATASCOPETRIGGERADDRESS")
                                {//actual names for trigger address
                                 //Add the rest of stacklayout_oxyplot's children to vehicle settings object
                                    vehicleSettings.Add(parameter.PropertyName, (this.FindByName<VisualElement>("DATASCOPETRIGGERADDRESS_PARAMETER_POINTER").BindingContext as GoiParameter).Name);
                                }
                                //else if (parameter.Name == "DATASCOPETRIGGERLEVEL")
                                //{//actual scaled value for for trigger level
                                // //Add the rest of stacklayout_oxyplot's children to vehicle settings object
                                //    vehicleSettings.Add(parameter.PropertyName, (this.FindByName<VisualElement>("DATASCOPETRIGGERADDRESS_PARAMETER_POINTER").BindingContext as GoiParameter).parameterValue.ToString());
                                //}
                                else
                                {
                                    //Add the rest of stacklayout_oxyplot's children to vehicle settings object
                                    vehicleSettings.Add(parameter.PropertyName, parameter.parameterValue);
                                }
                            }
                        }
                    }

                    //Upload to cloud
                    if (await App.ParseManagerAdapter.UploadScopeLog(vehicleSettings))
                    {
                        await DisplayAlert("Success", "Data has been uploaded successfully", "Ok");
                    }
                    else
                        await DisplayAlert("Failed", "Data upload failed ", "Ok");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Debug TransmitToParse exception caught");
                    System.Diagnostics.Debug.WriteLine(ex.ToString());

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        activityIndicator.IsRunning = false;
                        //IF something wrong, we could raise user's awareness
                        //rather than telling them transmit complete message
                        if (ex.Message.Contains("Object reference not set to an instance of an object"))
                            DisplayAlert("Connection Error", "Please check your internet connection and try again", "OK");
                        else if (ex.Message == "Could not store file.")
                            DisplayAlert("Error", "Could not store file", "OK");
                        else
                            DisplayAlert("Datalog", "Transmit Complete", "OK");
                    });
                }
            }
            else
                DisplayAlert("Empty Data", $"Can not upload empty data to the cloud. Please press Get Log button before uploading", "OK");
        }

        void AddExternalFilesAsButtons()
        {
            string[] fileList = FileManager.GetNavitasDirectoryFiles();
            //clear old list otherwise it just keeps adding same file items
            while (FileButtonList.Children.Count > 1)
                FileButtonList.Children.RemoveAt(1);
            foreach (var fileName in fileList)
            {
                bool fileNeedsToBeAdded = true;
                //foreach (var button in listOfFrames.Where(x => (x as VisualElement).GetType() == typeof(Button)))
                //{
                //    if ((button as Button).Text.Contains(fileName))
                //        fileNeedsToBeAdded = false;
                //}

                if (fileNeedsToBeAdded && fileName.Contains("ScopeSettings"))
                {
                    Frame publicFile = new Frame
                    {
                        BorderColor = Color.Black,
                        Padding = new Thickness(5, 0, 5, 0),
                        Margin = new Thickness(5, 0, 5, 0),
                        IsVisible = true,
                    };
                    Button button = new Button
                    {
                        Text = fileName,
                        //BackgroundColor = Color.FromHex("FF5A5F"),
                        //FontSize = Font.Default;
                    };
                    button.Clicked += (sender, args) => OnButtonClicked(button, new EventArgs<string>(fileName));
                    publicFile.Content = button;
                    FileButtonList.Children.Insert(2, publicFile); //skip first to labels in xaml
                }
            }
        }
        void OnButtonClicked(object sender, EventArgs args)
        {
            string strFileName = (args as EventArgs<string>).Value;
            FileParameters = new ObservableCollection<ParameterFileItem>();

            FileParameters = FileManager.GetDeserializedObject<ObservableCollection<ParameterFileItem>>(strFileName);

            foreach (var parameterFileItem in FileParameters)
            {
                GoiParameter parameter = App.ViewModelLocator.MainViewModel.GoiParameterList.FirstOrDefault(i => i.PropertyName == parameterFileItem.PropertyName);
                SetParameterEventArgs = new SetParameterEventArgs(parameter.Address, (float)parameterFileItem.ParameterValue, "never used so get rid of this");
                QueParameter(SetParameterEventArgs);
            }
            fileListPopUp.IsVisible = false;
        }

        PageParameterList WatchParameters;
        long WatchParametersUniqueId = 0;
        async void UpdateChannelPointerParameter(string parentElement, GoiParameter parameter)
        {
            try
            {
                VisualElement pointerElement = this.FindByName<VisualElement>(parentElement + "_PARAMETER_POINTER");
                GoiParameter watchParameter = App.ViewModelLocator.GetParameter(parameter.parameterValue);
                await Device.InvokeOnMainThreadAsync(() => { pointerElement.BindingContext = watchParameter;});
                WatchParameters = new PageParameterList(PageParameterList.ParameterListType.TAC, this);
                BuildCommunicationsList(WatchParameters.parameterList, this.FindByName<VisualElement>("DATASCOPECH1SELECT_PARAMETER_POINTER").BindingContext as GoiParameter);
                BuildCommunicationsList(WatchParameters.parameterList, this.FindByName<VisualElement>("DATASCOPECH2SELECT_PARAMETER_POINTER").BindingContext as GoiParameter);
                BuildCommunicationsList(WatchParameters.parameterList, this.FindByName<VisualElement>("DATASCOPECH3SELECT_PARAMETER_POINTER").BindingContext as GoiParameter);
                BuildCommunicationsList(WatchParameters.parameterList, this.FindByName<VisualElement>("DATASCOPECH4SELECT_PARAMETER_POINTER").BindingContext as GoiParameter);
                BuildCommunicationsList(WatchParameters.parameterList, this.FindByName<VisualElement>("DATASCOPETRIGGERADDRESS_PARAMETER_POINTER").BindingContext as GoiParameter);
                if (WatchParametersUniqueId != 0)
                {//skipped first removal since nothing has been added
                    RemovePacketList(WatchParametersUniqueId);
                }
                WatchParametersUniqueId = App._devicecommunication.AddToPacketList(WatchParameters);
                switch(parentElement)
                {
                    case "DATASCOPECH1SELECT":
                        ch1Scale = watchParameter.Scale;                                             
                        CH1_L1.Title = pvch1.Model.Title = L1.Title = watchParameter.Name;
                        break;
                    case "DATASCOPECH2SELECT":
                        ch2Scale = watchParameter.Scale;
                        pvch2.Model.Title = L2.Title = watchParameter.Name;
                        break;
                    case "DATASCOPECH3SELECT":
                        ch3Scale = watchParameter.Scale;
                        pvch3.Model.Title = L3.Title = watchParameter.Name;
                        break;
                    case "DATASCOPECH4SELECT":
                        ch4Scale = watchParameter.Scale;
                        pvch4.Model.Title = L4.Title = watchParameter.Name;
                        break;
                    case "DATASCOPETRIGGERADDRESS":
                        App.ViewModelLocator.GetParameter("DATASCOPETRIGGERLEVEL").Scale = triggerScale = watchParameter.Scale;
                        //SetParameterEventArgs = new SetParameterEventArgs(App.ViewModelLocator.GetParameter("DATASCOPETRIGGERLEVEL").Address, App.ViewModelLocator.GetParameter("DATASCOPETRIGGERLEVEL").parameterValue, "never used so get rid of this");
                        //QueParameter(SetParameterEventArgs);

                        break;
                }


            }
            catch (Exception ex)
            {

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

    }
}