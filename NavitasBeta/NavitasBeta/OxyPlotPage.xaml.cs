using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using System.Windows.Input;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace NavitasBeta
{
    public partial class OxyPlotPage : ContentPage
    {
        List<PlotView> PlotViews;
        volatile public bool HasGraphingStarted = false;
        public ICommand ToolBarMenuSelectionCommand { private set; get; }
        int _Scale = 0;
        public DateTime StartTime;

        public OxyPlotPage(int Scale)
        {
            _Scale = Scale;
            ToolBarMenuSelectionCommand = new Command<string>(ExecuteSelection, CanExecuteSelection);
            try
            {
                InitializeComponent();
                this.Content = new AbsoluteLayout();
                AddActivityPopUp();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Debug this exception: OxyPlotPage.xaml.cs" + ex.Message);
            }
            this.BindingContext = this;

        }
        protected override void OnDisappearing()
        {
            foreach (PlotView pv in PlotViews)
            {
                pv.Model = null;

            }
            HasGraphingStarted = false;
            base.OnDisappearing();
        }


        bool isDatalogging = false;

        void ExecuteSelection(string selection)
        {
            switch (selection)
            {
                case "Play":
                    HasGraphingStarted ^= true;
                    if (HasGraphingStarted == true)
                        Play.IconImageSource = "ionstop.png";
                    else
                        Play.IconImageSource = "ionplay.png";

                    System.Diagnostics.Debug.WriteLine("Play button pressed");
                    break;
                case "Transmit":
                    try
                    {
                        //      activityIndicator.Running = true;
                        isDatalogging = true;
                        RefressCanExecutes();
                        TransmitToParse();
                        //    activityIndicator.Running = false;
                    }
                    catch
                    {
                        //             activityIndicator.Running = false;
                        this.DisplayAlert("Error", "No Internet", "OK");
                    }
                    System.Diagnostics.Debug.WriteLine("Transmit button pressed");
                    break;
            }
        }

        void RefressCanExecutes()
        {
            ((Command)ToolBarMenuSelectionCommand).ChangeCanExecute();
        }

        //Stop user pressing multiple times when data was transmitting
        bool CanExecuteSelection(string selection)
        {
            return !isDatalogging;
        }

        async void TransmitToParse()
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    activityMessage.Text = "Sending data...";
                    this.PageIsBusy = true;
                });

                if (PlotViews.Count() > 0)
                {
                    await PackAllLoggedParameterData();
                }
                else
                {
                    //Since we had hidden parameter selected for datalogging
                    //Is popup still correct ?
                    DisplayAlert("Datalog Not Sent", "No Values Selected", "OK");
                }
                System.Diagnostics.Debug.WriteLine("Dons Calling  App.ParseManagerAdapter.Transmit(Test)");

                await App.ParseManagerAdapter.Transmit("Test");
                await FileTransmitHandler();
                //System.Diagnostics.Debug.WriteLine("file transmission completed");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Dons Debug TransmitToParse exception caught");
                System.Diagnostics.Debug.WriteLine(e.ToString());
                //IF something wrong, we would raise user's awareness
                //rather than telling them transmit complete message
                if (e.Message.Contains("Object reference not set to an instance of an object"))
                {
                    DisplayAlert("Connection Error", "Please Check Your Internet Connection and try again", "OK");
                }
                else if (e.Message == "Could not store file.")
                {
                    // This error came once in a while
                    DisplayAlert("Error", "Could not store file. Please try again", "OK");
                }
                else
                {
                    //Unknown errors
                    System.Diagnostics.Debug.WriteLine("TransmitToParse exception - Unkonwn Error: " + e.Message);
                    await FileTransmitHandler();
                }
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                activityMessage.Text = "";
                PageIsBusy = false;
            });
            isDatalogging = false;
            RefressCanExecutes();
        }

        private async Task PackAllLoggedParameterData()
        {
            System.Diagnostics.Debug.WriteLine("Dons Calling  App.ParseManagerAdapter.InitParametersList()");
            App.ParseManagerAdapter.InitParametersList(App.PresentConnectedController);
            foreach (PlotView pv in PlotViews)
            {
                string parameterName = pv.Model.Series[0].Title;
                int indexOfPlotView = GetIndexOfPlotViewFromParameterName(parameterName);
                System.Diagnostics.Debug.WriteLine($"Dons Calling  App.ParseManagerAdapter.AddLoggedParameterData({parameterName},PointTimeStamp[{indexOfPlotView}],YPoints[{indexOfPlotView}])");
                App.ParseManagerAdapter.AddLoggedParameterData(parameterName, PointTimeStamp[indexOfPlotView], YPoints[indexOfPlotView]);
            }
        }

        public async Task FileTransmitHandler()
        {
            // Save text file locally
            var datalogId = App.ParseManagerAdapter.GetDatalogUniqueId();
            //Local file was named same as the uploaded one

            var filePath = Path.Combine(FileManager.GetNavitasDirectoryPath(), $"{datalogId}_NavitasPhoneAppLog.txt");
            Dictionary<string, object> vehicleDatalog = App.ParseManagerAdapter.GetDatalogObject();
            FileManager.WriteJsonFile(filePath, vehicleDatalog);
            // ------------------------

            if (datalogId != "")
            {
                //Verify if the fie exist !!
                int checkDateTimeCounter = 0;
                bool isFileTransmissionSuccess = false;
                //DateTime starttime = DateTime.Now;
                while (!isFileTransmissionSuccess && checkDateTimeCounter < 5)
                {
                    DateTime getUrlStartTime = DateTime.Now;
                    var stringTemplate = $"https://goi-608.nodechef.com/parse/files/5df4cd98e64a66c35c8282f69bf7472a/{datalogId}_NavitasPhoneAppLog.txt";
                    DateTime dateTimeFileLastModified = await FileManager.GetUrlDateAndTime(stringTemplate);

                    //System.Diagnostics.Debug.WriteLine($"Time get back reponse: {(DateTime.Now - getUrlStartTime).TotalSeconds} - {checkDateTimeCounter} attempt(s)");
                    if (DateTime.Now - getUrlStartTime >= TimeSpan.FromSeconds(10))
                    {
                        //We found out the issue early so break the loop before more unhelpful attempts
                        break;
                    }

                    if (dateTimeFileLastModified == DateTime.MinValue || checkDateTimeCounter > 0)
                    {
                        activityMessage.Text = "Retrying";
                    }

                    if (dateTimeFileLastModified > DateTime.MinValue)
                    {
                        //System.Diagnostics.Debug.WriteLine("DataLogFile updated at: " + dateTimeFileExist);
                        //System.Diagnostics.Debug.WriteLine("Transmit Complete");
                        DisplayAlert("Datalog", "Transmit Complete", "OK");
                        isFileTransmissionSuccess = true;
                        //Delete Datalog back up file
                        FileManager.DeleteFile(filePath);
                    }
                    checkDateTimeCounter++;
                }
                //System.Diagnostics.Debug.WriteLine($"TimeSpan: {(DateTime.Now - starttime).TotalMilliseconds} - {checkDateTimeCounter} attempt(s)");
                if (!isFileTransmissionSuccess)
                {
                    //System.Diagnostics.Debug.WriteLine("DataLogFile not found");
                    bool isBtnEmailSelected = await DisplayAlert("Tranfer failed", "Press send email and we will add this datalog as an attachment instead", "Send Email", "Cancel");
                    if (isBtnEmailSelected)
                    {
                        await FileManager.SendEmailWithOptionalAttachment("", filePath);
                        //Because the line above did not actually await, so do not delete this file.
                    }
                    //Redirect to settings page after sending email
                    Navigation.PopAsync();
                }
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine("Parse File failed to save and failed the transmit function");
                await DisplayAlert("Internal Server Error", "The server encountered an internal error and was unable to send your datalog", "Ok");
            }
        }

        public void RemoveAllGraphs()
        {
            //   HasGraphingStarted = true;
            PlotViews = new List<PlotView>();
        }


        public void AddGraph(string ParameterName, bool isGraphingVisible)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                PlotView pv = new PlotView();
                pv.VerticalOptions = LayoutOptions.Fill;
                pv.HorizontalOptions = LayoutOptions.Fill;
                pv.Model = new PlotModel();
                pv.Model.Title = ParameterName;
                pv.IsVisible = isGraphingVisible;

                LinearAxis A1 = new LinearAxis();
                A1.Position = AxisPosition.Bottom;
                A1.Title = "Time";
                A1.Minimum = 1;
                A1.Maximum = 10;

                LinearAxis A2 = new LinearAxis();
                A2.Position = AxisPosition.Left;
                A2.Title = "Parameter Value";
                A2.Minimum = 1;
                A2.Maximum = 10;

                pv.Model.Axes.Add(A1);
                pv.Model.Axes.Add(A2);
                LineSeries L1 = new LineSeries();
                L1.Title = ParameterName;

                pv.Model.Series.Add(L1);

                PlotViews.Add(pv);

            });

        }

        public void RemoveGraph(string ParameterName)
        {
            PlotViews.RemoveAll(x => x.Model.Series[0].Title == ParameterName);
        }
        public PlotView GetPlotViewFromParameterName(string ParameterName)
        {
            PlotView pvfound = null;
            foreach (PlotView pv in PlotViews)
            {
                if (pv.Model == null)
                    throw (new Exception("GetPlotViewFromParameterNameException"));
                if (pv.Model.Series[0].Title == ParameterName)
                {
                    pvfound = pv;
                }
            }
            return pvfound;
        }

        public int GetIndexOfPlotViewFromParameterName(string ParameterName)
        {
            int iIndex = 0;

            foreach (PlotView pv in PlotViews)
            {
                if (pv.Model == null)
                    throw (new Exception("GetIndexOfPlotViewFromParameterNameException"));

                if (pv.Model.Series[0].Title == ParameterName)
                {
                    break;
                }
                iIndex++;
            }
            return iIndex;
        }

        public bool ClearGraph(string ParameterName)
        {
            try
            {
                PlotView pvfound = GetPlotViewFromParameterName(ParameterName);
                if (pvfound == null)
                    return false;
                (pvfound.Model.Series[0] as LineSeries).Points.Clear();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void AddPointToGraph(string ParameterName, float yPoint)
        {
            if (HasGraphingStarted == false)
                throw (new Exception("AddPointToGraphException"));
            PlotView pvfound = GetPlotViewFromParameterName(ParameterName);
            if (pvfound != null)
            {
                timeelapsed = (double)(System.DateTime.Now - StartTime).Ticks / 10000000f;
                if (PointIndex == null)
                    return;
                var a = PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)];

                System.Diagnostics.Debug.WriteLine(timeelapsed.ToString() + " a = " + a.ToString());



                if (PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)] < _NumberOfSamples)
                {
                    //            XPoints[GetIndexOfPlotViewFromParameterName(ParameterName)][PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)]] = PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)] + 1;
                    double timeelapsedms = timeelapsed;
                    XPoints[GetIndexOfPlotViewFromParameterName(ParameterName)][PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)]] = timeelapsedms;
                    pvfound.Model.Axes[0].Maximum = XPoints[GetIndexOfPlotViewFromParameterName(ParameterName)][PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)]];
                    YPoints[GetIndexOfPlotViewFromParameterName(ParameterName)][PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)]] = yPoint;
                    PointTimeStamp[GetIndexOfPlotViewFromParameterName(ParameterName)][PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)]] = timeelapsedms.ToString("{0.000}");
                    PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)]++;
                }
                else
                {

                    ShiftPoints(YPoints[GetIndexOfPlotViewFromParameterName(ParameterName)], XPoints[GetIndexOfPlotViewFromParameterName(ParameterName)], PointTimeStamp[GetIndexOfPlotViewFromParameterName(ParameterName)], yPoint);
                }
                int index = GetIndexOfPlotViewFromParameterName(ParameterName);
                if (pvfound.IsVisible)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {

                        if (ClearGraph(ParameterName) == false)
                            return;
                        for (int i = 0; i < PointIndex[index]; i++)
                        {
                            if (bTriggerOnFirstTime[index])
                            {
                                LargestYPoints[index] = yPoint;
                                SmallestYPoints[index] = yPoint;
                                bTriggerOnFirstTime[index] = false;
                            }
                            if (yPoint >= LargestYPoints[index])
                            {
                                LargestYPoints[index] = yPoint;
                                pvfound.Model.Axes[1].Maximum = LargestYPoints[index] + (float)Math.Abs(SmallestYPoints[index]);
                                //                System.Diagnostics.Debug.WriteLine("pvfound.Model.Axes[1].Maximum " +  pvfound.Model.Axes[1].Maximum.ToString());
                            }
                            if (yPoint <= SmallestYPoints[index])
                            {
                                SmallestYPoints[index] = yPoint;
                                pvfound.Model.Axes[1].Minimum = SmallestYPoints[index] - (float)Math.Abs(LargestYPoints[index]);
                                //          System.Diagnostics.Debug.WriteLine("pvfound.Model.Axes[1].Minimum " + pvfound.Model.Axes[1].Minimum.ToString());
                            }
                            if (pvfound.Model.Axes[1].Minimum == pvfound.Model.Axes[1].Maximum)
                            {
                                pvfound.Model.Axes[1].Minimum -= 1;
                                pvfound.Model.Axes[1].Maximum += 1;
                            }
                        }



                        pvfound.Model.Axes[0].Minimum = XPoints[GetIndexOfPlotViewFromParameterName(ParameterName)][0];
                        //           pvfound.Model.Axes[0].Minimum = 1;
                        //         pvfound.Model.Axes[0].Maximum = PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)];
                        pvfound.Model.Axes[0].Maximum = XPoints[GetIndexOfPlotViewFromParameterName(ParameterName)][PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)] - 1];

                        //          System.Diagnostics.Debug.WriteLine("Min = {0}", pvfound.Model.Axes[0].Minimum.ToString());
                        //        System.Diagnostics.Debug.WriteLine("Max = {0}", pvfound.Model.Axes[0].Maximum.ToString());

                        for (int i = 0; i < PointIndex[GetIndexOfPlotViewFromParameterName(ParameterName)]; i++)
                        {
                            (pvfound.Model.Series[0] as LineSeries).Points.Add(new DataPoint(XPoints[GetIndexOfPlotViewFromParameterName(ParameterName)][i], (YPoints[GetIndexOfPlotViewFromParameterName(ParameterName)][i])));
                        }
                        try
                        {
                            pvfound.Model.InvalidatePlot(true);
                        }
                        catch
                        {
                            // throw this error away.
                            // It just means some hit the back button when graphing was still int progress
                            System.Diagnostics.Debug.WriteLine("Exception disposed has been handled");
                        }
                    });
                }
            }
        }

        void ShiftPoints(double[] YPoints, double[] XPoints, string[] PointTimeStamp, double NewPoint)
        {
            int i;
            for (i = 0; i < YPoints.Length - 1; i++)
            {
                YPoints[i] = YPoints[i + 1];
                XPoints[i] = XPoints[i + 1];
                PointTimeStamp[i] = PointTimeStamp[i + 1];

            }
            YPoints[i] = NewPoint;
            XPoints[i] = timeelapsed;
            PointTimeStamp[i] = timeelapsed.ToString("{0.000}");
        }

        double[] LargestYPoints;
        double[] SmallestYPoints;
        bool[] bTriggerOnFirstTime;
        double[][] XPoints;
        double[][] YPoints;
        string[][] PointTimeStamp;
        int[] PointIndex;
        int _NumberOfSamples;

        double timeelapsed = 0;
        public void ShowAllGraphs(int iNumberOfSamples)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                HasGraphingStarted = true;
                _NumberOfSamples = iNumberOfSamples;
                Grid grid_oxyplot = new Grid();
                ScrollView scrollview = new ScrollView();

                int index = 0;
                int reverseIndex = PlotViews.Count();
                foreach (PlotView pv in PlotViews)
                {
                    grid_oxyplot.RowDefinitions.Add(new RowDefinition { Height = new GridLength(300, GridUnitType.Absolute) });
                    if (pv.IsVisible)
                    {
                        System.Diagnostics.Debug.WriteLine("plotview IsVisible: true ");
                        grid_oxyplot.Children.Add(pv, 0, index);
                        TransparentView v = new TransparentView();
                        grid_oxyplot.Children.Add(v, 0, index);  // a transparent view allow scrolling on IOS
                        index++;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("plotview IsVisible: false ");
                        grid_oxyplot.Children.Add(pv, 0, reverseIndex);
                        TransparentView v = new TransparentView();
                        grid_oxyplot.Children.Add(v, 0, reverseIndex);
                        reverseIndex--;
                    }

                }
                index = PlotViews.Count();
                scrollview.Content = grid_oxyplot;

                LargestYPoints = new double[index];
                SmallestYPoints = new double[index];
                bTriggerOnFirstTime = new bool[index];
                for (int i = 0; i < index; i++)
                    bTriggerOnFirstTime[i] = true;

                PointIndex = new int[index];
                XPoints = new double[index][];
                YPoints = new double[index][];
                PointTimeStamp = new string[index][];
                for (int i = 0; i < index; i++)  // index is the number of graphs 
                {
                    XPoints[i] = new double[_NumberOfSamples];
                    YPoints[i] = new double[_NumberOfSamples];
                    PointTimeStamp[i] = new string[_NumberOfSamples];
                    PointIndex[i] = 0;
                }
                timeelapsed = 0;
                AbsoluteLayout.SetLayoutFlags(scrollview, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(scrollview, new Rectangle(0, 0, 1, 1));
                (this.Content as AbsoluteLayout).Children.Add(scrollview);
            });
        }

        public static readonly BindableProperty PageIsBusyProperty = BindableProperty.Create(nameof(PageIsBusy),
            typeof(bool),
            typeof(OxyPlotPage),
            false);

        public bool PageIsBusy
        {
            get { return (bool)GetValue(PageIsBusyProperty); }
            //couldn't get property binding to work so....
            set
            {
                SetValue(PageIsBusyProperty, value);
                disableBackgroundOverlay.IsVisible = value;
                activityIndicator.IsRunning = value;
                if (value)
                    (this.Content as AbsoluteLayout).RaiseChild(disableBackgroundOverlay);
                else
                    (this.Content as AbsoluteLayout).LowerChild(disableBackgroundOverlay);
            }
        }

        ContentView disableBackgroundOverlay;
        ActivityIndicator activityIndicator;
        Label activityMessage;
        public void AddActivityPopUp()
        {
            AbsoluteLayout overlayArea = this.Content as AbsoluteLayout;
            var halfViewHeight = ((DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density) / 2);

            disableBackgroundOverlay = new ContentView
            {
                IsVisible = false,
                Padding = new Thickness(0, halfViewHeight * 0.40),
                BackgroundColor = Color.FromHex("#C0202020"),
            };

            AbsoluteLayout.SetLayoutFlags(disableBackgroundOverlay, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(disableBackgroundOverlay, new Rectangle(0, 0, 1, 1));

            //Size size = Device.Info.PixelScreenSize;
            //var screenBoxSize = (float)Math.Min(size.Height, size.Width);
            //var scaleSinceIOSDoesNotSizeWithHorWRequests = screenBoxSize / 30 / 5;

            StackLayout activityStacklayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            activityIndicator = new ActivityIndicator
            {
                //Use the same color as the school text color as it should contrast the background the same.
                Color = Color.DodgerBlue,
                //Scale = scaleSinceIOSDoesNotSizeWithHorWRequests
            };

            var halfViewWidth = ((DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) / 2);

            activityMessage = new Label
            {
                Padding = new Thickness(halfViewWidth * 0.4, 0, 0, 0),
                TextColor = Color.Black,
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                LineBreakMode = LineBreakMode.WordWrap,
                MaxLines = 2
            };

            activityStacklayout.Children.Add(activityIndicator);
            activityStacklayout.Children.Add(activityMessage);

            Frame activityFrame = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = halfViewWidth * 0.6,
                Margin = new Thickness(halfViewWidth * 0.15, 0),
                HasShadow = true,
                Content = activityStacklayout,
            };

            disableBackgroundOverlay.Content = activityFrame;
            overlayArea.Children.Add(disableBackgroundOverlay);
        }
    }
}
