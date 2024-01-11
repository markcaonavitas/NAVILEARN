using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace NavitasBeta
{
    public partial class ReadOnlyPageOld : ContentPage
    {
        //        public event EventHandler<System.EventArgs> StartDatalogging = delegate { };
        OxyPlotPage OxyPlotPage;
        public ReadOnlyPageOld()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                //throw ex;
            }
            BindingContext = ViewModelLocator.MainViewModel;
            OxyPlotPage = new OxyPlotPage();
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
            //        StartDatalogging(sender, null);

            StartDatalogging();
        }

        async void StartDatalogging()
        {

            OxyPlotPage.RemoveAllGraphs();
            if (ViewModelLocator.MainViewModel.ISRAWTHROTTLECHECKED)
            {
                OxyPlotPage.AddGraph("Raw Throttle");
            }
            if (ViewModelLocator.MainViewModel.ISVBATVDCCHECKED)
            {
                OxyPlotPage.AddGraph("Vbat (Vdc)");
            }
            if (ViewModelLocator.MainViewModel.ISIBATADCCHECKED)
            {
                OxyPlotPage.AddGraph("Ibat (Adc)");
            }
            if (ViewModelLocator.MainViewModel.ISIDACHECKED)
            {
                OxyPlotPage.AddGraph("Id (A)");
            }
            if (ViewModelLocator.MainViewModel.ISIQACHECKED)
            {
                OxyPlotPage.AddGraph("Iq (A)");
            }
            if (ViewModelLocator.MainViewModel.ISVDVCHECKED)
            {
                OxyPlotPage.AddGraph("Vd (V)");
            }
            if (ViewModelLocator.MainViewModel.ISVQVCHECKED)
            {
                OxyPlotPage.AddGraph("Vq (V)");
            }
            if (ViewModelLocator.MainViewModel.ISVECTORCURRENTCHECKED)
            {
                OxyPlotPage.AddGraph("Vector Current");
            }
            if (ViewModelLocator.MainViewModel.ISPHASECURRENTLIMITERCHECKED)
            {
                OxyPlotPage.AddGraph("Phase Current Limiter");
            }
            if (ViewModelLocator.MainViewModel.ISBATTERYCURRENTLIMITERCHECKED)
            {
                OxyPlotPage.AddGraph("Battery Current Limiter");
            }
            if (ViewModelLocator.MainViewModel.ISSLIPREFSNAP1CHECKED)
            {
                OxyPlotPage.AddGraph("Slip Ref Snap 1");
            }
            if (ViewModelLocator.MainViewModel.ISSLIPREFSNAP2CHECKED)
            {
                OxyPlotPage.AddGraph("Slip Ref Snap 2");
            }
            if (ViewModelLocator.MainViewModel.ISSLIPREFSNAP3CHECKED)
            {
                OxyPlotPage.AddGraph("Slip Ref Snap 3");
            }
            if (ViewModelLocator.MainViewModel.ISIAACHECKED)
            {
                OxyPlotPage.AddGraph("Ia (A)");
            }
            if (ViewModelLocator.MainViewModel.ISIBACHECKED)
            {
                OxyPlotPage.AddGraph("Ib (A)");
            }
            if (ViewModelLocator.MainViewModel.ISICACHECKED)
            {
                OxyPlotPage.AddGraph("Ic (A)");
            }
#if OLD_CODE
            if (ViewModelLocator.MainViewModel.ISIAARMSCHECKED)
            {
                OxyPlotPage.AddGraph("Ia (Arms)");
            }

            if (ViewModelLocator.MainViewModel.ISIBARMSCHECKED)
            {
                OxyPlotPage.AddGraph("Ib (Arms)");
            }
            if (ViewModelLocator.MainViewModel.ISICARMSCHECKED)
            {
                OxyPlotPage.AddGraph("Ic (Arms)");
            }
#endif 
            if (ViewModelLocator.MainViewModel.ISVABVRMSCHECKED)
            {
                OxyPlotPage.AddGraph("Vab (Vrms)");
            }
            if (ViewModelLocator.MainViewModel.ISVBCVRMSCHECKED)
            {
                OxyPlotPage.AddGraph("Vbc (Vrms)");
            }
            if (ViewModelLocator.MainViewModel.ISVCAVRMSCHECKED)
            {
                OxyPlotPage.AddGraph("Vca (Vrms)");
            }
            if (ViewModelLocator.MainViewModel.ISROTORRPMCHECKED)
            {
                OxyPlotPage.AddGraph("Rotor RPM");
            }
            if (ViewModelLocator.MainViewModel.ISROTORHZCHECKED)
            {
                OxyPlotPage.AddGraph("RotorHz");
            }
            if (ViewModelLocator.MainViewModel.ISSTATORHZCHECKED)
            {
                OxyPlotPage.AddGraph("StatorHz");
            }
            //if (ViewModelLocator.MainViewModel.ISJERKTIMEFORBRAKETOZEROVELOCITY31CHECKED)
            //{
            //    OxyPlotPage.AddGraph("Speed ref RPM");
            //}
            if (ViewModelLocator.MainViewModel.ISSLIPFREQHZCHECKED)
            {
                OxyPlotPage.AddGraph("Slip Freq (Hz)");
            }
            if (ViewModelLocator.MainViewModel.ISRATEDRPMCALCCHECKED)
            {
                OxyPlotPage.AddGraph("Rated RPM (calc)");
            }
            if (ViewModelLocator.MainViewModel.ISVTHROTTLEVCHECKED)
            {
                OxyPlotPage.AddGraph("Throttle Voltage");
            }
            if (ViewModelLocator.MainViewModel.ISVBRAKEVCHECKED)
            {
                OxyPlotPage.AddGraph("V brake (V)");
            }
            if (ViewModelLocator.MainViewModel.ISVPROG1VCHECKED)
            {
                OxyPlotPage.AddGraph("V prog1 (V)");
            }
            if (ViewModelLocator.MainViewModel.ISVPROG2VCHECKED)
            {
                OxyPlotPage.AddGraph("V prog2 (V)");
            }
            if (ViewModelLocator.MainViewModel.ISVPROG3VCHECKED)
            {
                OxyPlotPage.AddGraph("V prog3 (V)");
            }
            if (ViewModelLocator.MainViewModel.ISMTTEMPVCHECKED)
            {
                OxyPlotPage.AddGraph("MT temp (V)");
            }
            if (ViewModelLocator.MainViewModel.ISMTTEMPCCHECKED)
            {
                OxyPlotPage.AddGraph("MT temp (C)");
            }
            if (ViewModelLocator.MainViewModel.ISPBTEMPVCHECKED)
            {
                OxyPlotPage.AddGraph("PB temp (V)");
            }
            if (ViewModelLocator.MainViewModel.ISPBTEMPCCHECKED)
            {
                OxyPlotPage.AddGraph("PB temp (C)");
            }
            if (ViewModelLocator.MainViewModel.ISFORWARDSWCHECKED)
            {
                OxyPlotPage.AddGraph("Forward SW");
            }
            if (ViewModelLocator.MainViewModel.ISREVERSESWCHECKED)
            {
                OxyPlotPage.AddGraph("Reverse SW");
            }
            if (ViewModelLocator.MainViewModel.ISKEYINCHECKED)
            {
                OxyPlotPage.AddGraph("Key In");
            }
            if (ViewModelLocator.MainViewModel.ISFOOTSWCHECKED)
            {
                OxyPlotPage.AddGraph("Foot SW");
            }
            if (ViewModelLocator.MainViewModel.ISPROGINCHECKED)
            {
                OxyPlotPage.AddGraph("Prog In");
            }
            if (ViewModelLocator.MainViewModel.ISTOWINCHECKED)
            {
                OxyPlotPage.AddGraph("Tow In");
            }
            if (ViewModelLocator.MainViewModel.ISBRAKESWCHECKED)
            {
                OxyPlotPage.AddGraph("Brake SW");
            }
            if (ViewModelLocator.MainViewModel.ISCHARGERINTERLOCKINCHECKED)
            {
                OxyPlotPage.AddGraph("Charger Interlock In");
            }
            if (ViewModelLocator.MainViewModel.ISCOILFAULTINCHECKED)
            {
                OxyPlotPage.AddGraph("Coil Fault In");
            }
            if (ViewModelLocator.MainViewModel.ISENCODERACHECKED)
            {
                OxyPlotPage.AddGraph("Encoder A");
            }
            if (ViewModelLocator.MainViewModel.ISENCODERBCHECKED)
            {
                OxyPlotPage.AddGraph("Encoder B");
            }
            if (ViewModelLocator.MainViewModel.ISREVERSEBUZZERCHECKED)
            {
                OxyPlotPage.AddGraph("Reverse Buzzer");
            }
            if (ViewModelLocator.MainViewModel.ISBRAKERESCHECKED)
            {
                OxyPlotPage.AddGraph("Brake Res");
            }
            if (ViewModelLocator.MainViewModel.ISMAINCTCHECKED)
            {
                OxyPlotPage.AddGraph("Main CT");
            }
            if (ViewModelLocator.MainViewModel.ISBRAKECTCHECKED)
            {
                OxyPlotPage.AddGraph("Brake CT");
            }
            if (ViewModelLocator.MainViewModel.ISRUNINVERTERCHECKED)
            {
                OxyPlotPage.AddGraph("Run Inverter");
            }
            if (ViewModelLocator.MainViewModel.ISMODINDEXCHECKED)
            {
                OxyPlotPage.AddGraph("Mod index");
            }
            if (ViewModelLocator.MainViewModel.ISFREQCHECKED)
            {
                OxyPlotPage.AddGraph("Freq");
            }
#if OLD_CODE
            if (ViewModelLocator.MainViewModel.ISAMODULATIONCHECKED)
            {
                OxyPlotPage.AddGraph("A modulation");
            }
            if (ViewModelLocator.MainViewModel.ISBMODULATIONCHECKED)
            {
                OxyPlotPage.AddGraph("B modulation");
            }
            if (ViewModelLocator.MainViewModel.ISCMODULATIONCHECKED)
            {
                OxyPlotPage.AddGraph("C modulation");
            }
#endif 
            if (ViewModelLocator.MainViewModel.ISFAULTCODECHECKED)
            {
                OxyPlotPage.AddGraph("Fault code");
            }
            if (ViewModelLocator.MainViewModel.ISSPEEDCMDRPMCHECKED)
            {
                OxyPlotPage.AddGraph("Speed Cmd RPM");
            }
            if (ViewModelLocator.MainViewModel.ISBRAKEOUTPUTCHECKED)
            {
                OxyPlotPage.AddGraph("Brake Output");
            }
            if (ViewModelLocator.MainViewModel.ISTHROTTLEBRAKECHECKED)
            {
                OxyPlotPage.AddGraph("Throttle Brake");
            }
            if (ViewModelLocator.MainViewModel.ISFORWARDSPEEDCHECKED)
            {
                OxyPlotPage.AddGraph("Forward Speed");
            }
            if (ViewModelLocator.MainViewModel.ISREVERSESPEEDCHECKED)
            {
                OxyPlotPage.AddGraph("Reverse Speed");
            }
#if WHEN_THROTTLE_WAS_READ_ONLY
            if (ViewModelLocator.MainViewModel.ISTHROTTLEMINCHECKED)
            {
                OxyPlotPage.AddGraph("Throttle Min");
            }
            if (ViewModelLocator.MainViewModel.ISTHROTTLEMAXCHECKED)
            {
                OxyPlotPage.AddGraph("Throttle Max");
            }
#endif
            if (ViewModelLocator.MainViewModel.ISSTATECHECKED)
            {
                OxyPlotPage.AddGraph("State");
            }
            if (ViewModelLocator.MainViewModel.ISSTATEINDEXCHECKED)
            {
                OxyPlotPage.AddGraph("State Index");
            }
            if (ViewModelLocator.MainViewModel.ISCONFIGURINGTHROTTLECHECKED)
            {
                OxyPlotPage.AddGraph("Configuring Throttle");
            }
            if (ViewModelLocator.MainViewModel.ISOVERCURRENTERRORSCHECKED)
            {
                OxyPlotPage.AddGraph("Over Current Errors");
            }
            if (ViewModelLocator.MainViewModel.ISENCODERISRCHECKED)
            {
                OxyPlotPage.AddGraph("Encoder ISR");
            }
            if (ViewModelLocator.MainViewModel.ISPWMISRCHECKED)
            {
                OxyPlotPage.AddGraph("PWM ISR");
            }
            if (ViewModelLocator.MainViewModel.ISENCODERISRCOUNTSCHECKED)
            {
                OxyPlotPage.AddGraph("Encoder ISR counts");
            }
            if (ViewModelLocator.MainViewModel.ISENCODERBADSEQCNTCHECKED)
            {
                OxyPlotPage.AddGraph("Encoder bad seq cnt");
            }
            if (ViewModelLocator.MainViewModel.ISENCODERDIRCHNGCNTCHECKED)
            {
                OxyPlotPage.AddGraph("Encoder dir chng cnt");
            }
            if (ViewModelLocator.MainViewModel.ISENCODERTIMEOUTSCHECKED)
            {
                OxyPlotPage.AddGraph("Encoder Timeouts cnt");
            }
            if (ViewModelLocator.MainViewModel.ISONEMSTASKCHECKED)
            {
                OxyPlotPage.AddGraph("Onems task");
            }

            OxyPlotPage.ShowAllGraphs(6000); // 60 is the number of samples. 
                                           // The server role should decide this. 

            System.Diagnostics.Debug.WriteLine("Pushing oxyplot page");
            await Navigation.PushAsync(OxyPlotPage);

        }

        public void AddPointsToGraphForPacket1()
        {
            if (OxyPlotPage.HasGraphingStarted)
            {
                if (ViewModelLocator.MainViewModel.ISIBATADCCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Ibat (Adc)", ViewModelLocator.MainViewModel.IBATADC);

                }
                if (ViewModelLocator.MainViewModel.ISIDACHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Id (A)", ViewModelLocator.MainViewModel.IDA);

                }
                if (ViewModelLocator.MainViewModel.ISIQACHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Iq (A)", ViewModelLocator.MainViewModel.IQA);

                }
                if (ViewModelLocator.MainViewModel.ISVDVCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Vd (V)", ViewModelLocator.MainViewModel.VDV);

                }
                if (ViewModelLocator.MainViewModel.ISVQVCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Vq (V)", ViewModelLocator.MainViewModel.VQV);

                }
                if (ViewModelLocator.MainViewModel.ISVECTORCURRENTCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Motor Current", ViewModelLocator.MainViewModel.VECTORCURRENT);
                }
                if (ViewModelLocator.MainViewModel.ISPHASECURRENTLIMITERCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Phase Current Limiter", ViewModelLocator.MainViewModel.PHASECURRENTLIMITER);
                }
                if (ViewModelLocator.MainViewModel.ISBATTERYCURRENTLIMITERCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Battery Current Limiter", ViewModelLocator.MainViewModel.BATTERYCURRENTLIMITER);
                }
                if (ViewModelLocator.MainViewModel.ISSLIPREFSNAP1CHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Slip Ref Snap 1", ViewModelLocator.MainViewModel.SLIP_REF_SNAP_1);
                }
                if (ViewModelLocator.MainViewModel.ISSLIPREFSNAP2CHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Slip Ref Snap 2", ViewModelLocator.MainViewModel.SLIP_REF_SNAP_2);
                }
                if (ViewModelLocator.MainViewModel.ISSLIPREFSNAP3CHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Slip Ref Snap 3", ViewModelLocator.MainViewModel.SLIP_REF_SNAP_3);
                }
                if (ViewModelLocator.MainViewModel.ISIAACHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Ia (A)", ViewModelLocator.MainViewModel.IAA);
                }
                if (ViewModelLocator.MainViewModel.ISIBACHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Ib (A)", ViewModelLocator.MainViewModel.IBA);
                }

                if (ViewModelLocator.MainViewModel.ISIBACHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Ic (A)", ViewModelLocator.MainViewModel.ICA);
                }
#if OLD_CODE
                if (ViewModelLocator.MainViewModel.ISIAARMSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Ia (Arms)", ViewModelLocator.MainViewModel.IAARMS);
                }

                if (ViewModelLocator.MainViewModel.ISIBARMSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Ib (Arms)", ViewModelLocator.MainViewModel.IBARMS);
                }
                if (ViewModelLocator.MainViewModel.ISICARMSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Ic (Arms)", ViewModelLocator.MainViewModel.ICARMS);
                }
#endif
                if (ViewModelLocator.MainViewModel.ISVABVRMSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Vab (Vrms)", ViewModelLocator.MainViewModel.VABVRMS);
                }
                if (ViewModelLocator.MainViewModel.ISVBCVRMSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Vbc (Vrms)", ViewModelLocator.MainViewModel.VBCVRMS);
                }
                if (ViewModelLocator.MainViewModel.ISVCAVRMSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Vca (Vrms)", ViewModelLocator.MainViewModel.VCAVRMS);
                }
                if (ViewModelLocator.MainViewModel.ISROTORRPMCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Rotor RPM", ViewModelLocator.MainViewModel.ROTORRPM);
                }
                if (ViewModelLocator.MainViewModel.ISROTORHZCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("RotorHz", ViewModelLocator.MainViewModel.ROTORHZ);
                }
                if (ViewModelLocator.MainViewModel.ISSTATORHZCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("StatorHz", ViewModelLocator.MainViewModel.STATORHZ);
                }
                //if (ViewModelLocator.MainViewModel.ISJERKTIMEFORBRAKETOZEROVELOCITY31CHECKED)
                //{
                //    OxyPlotPage.AddPointToGraph("Speed ref RPM", ViewModelLocator.MainViewModel.JERKTIMEFORBRAKETOZEROVELOCITY31);
                //}
                if (ViewModelLocator.MainViewModel.ISSLIPFREQHZCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Slip Freq (Hz)", ViewModelLocator.MainViewModel.SLIPFREQHZ);
                }
                if (ViewModelLocator.MainViewModel.ISRATEDRPMCALCCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Rated RPM (calc)", ViewModelLocator.MainViewModel.RATEDRPMCALC);
                }
                if (ViewModelLocator.MainViewModel.ISVTHROTTLEVCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("V throttle (V)", ViewModelLocator.MainViewModel.VTHROTTLEV);
                }
                if (ViewModelLocator.MainViewModel.ISVBRAKEVCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("V brake (V)", ViewModelLocator.MainViewModel.VBRAKEV);
                }
                if (ViewModelLocator.MainViewModel.ISVPROG1VCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("V prog1 (V)", ViewModelLocator.MainViewModel.VPROG1V);
                }
                if (ViewModelLocator.MainViewModel.ISVPROG2VCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("V prog2 (V)", ViewModelLocator.MainViewModel.VPROG2V);
                }

            }
        }

        public void AddPointsToGraphForPacket2()
        {
            if (OxyPlotPage.HasGraphingStarted)
            {
                if (ViewModelLocator.MainViewModel.ISRAWTHROTTLECHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Raw Throttle", ViewModelLocator.MainViewModel.RAWTHROTTLE);
                }
                if (ViewModelLocator.MainViewModel.ISVBATVDCCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Vbat (Vdc)", ViewModelLocator.MainViewModel.VBATVDC);
                }
                if (ViewModelLocator.MainViewModel.ISVPROG3VCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("V prog3 (V)", ViewModelLocator.MainViewModel.VPROG3V);
                }
                if (ViewModelLocator.MainViewModel.ISMTTEMPVCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("MT temp (V)", ViewModelLocator.MainViewModel.MTTEMPV);
                }
                if (ViewModelLocator.MainViewModel.ISMTTEMPCCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("MT temp (C)", ViewModelLocator.MainViewModel.MTTEMPC);
                }
                if (ViewModelLocator.MainViewModel.ISPBTEMPVCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("PB temp (V)", ViewModelLocator.MainViewModel.PBTEMPV);
                }
                if (ViewModelLocator.MainViewModel.ISPBTEMPCCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("PB temp (C)", ViewModelLocator.MainViewModel.PBTEMPC);
                }
                if (ViewModelLocator.MainViewModel.ISFORWARDSWCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Forward SW", ViewModelLocator.MainViewModel.FORWARDSW);
                }
                if (ViewModelLocator.MainViewModel.ISREVERSESWCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Reverse SW", ViewModelLocator.MainViewModel.REVERSESW);
                }
                if (ViewModelLocator.MainViewModel.ISKEYINCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Key In", ViewModelLocator.MainViewModel.KEYIN);
                }
                if (ViewModelLocator.MainViewModel.ISFOOTSWCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Foot SW", ViewModelLocator.MainViewModel.FOOTSW);
                }
                if (ViewModelLocator.MainViewModel.ISPROGINCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Prog In", ViewModelLocator.MainViewModel.PROGIN);
                }
                if (ViewModelLocator.MainViewModel.ISTOWINCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Tow In", ViewModelLocator.MainViewModel.TOWIN);
                }
                if (ViewModelLocator.MainViewModel.ISBRAKESWCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Brake SW", ViewModelLocator.MainViewModel.BRAKESW);
                }
                if (ViewModelLocator.MainViewModel.ISCHARGERINTERLOCKINCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Charger Interlock In", ViewModelLocator.MainViewModel.BRAKESW);
                }
                if (ViewModelLocator.MainViewModel.ISCOILFAULTINCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Coil Fault In", ViewModelLocator.MainViewModel.COILFAULTIN);
                }
                if (ViewModelLocator.MainViewModel.ISENCODERACHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Encoder A", ViewModelLocator.MainViewModel.ENCODERA);
                }
                if (ViewModelLocator.MainViewModel.ISENCODERBCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Encoder B", ViewModelLocator.MainViewModel.ENCODERB);
                }
                if (ViewModelLocator.MainViewModel.ISREVERSEBUZZERCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Reverse Buzzer", ViewModelLocator.MainViewModel.REVERSEBUZZER);
                }
                if (ViewModelLocator.MainViewModel.ISBRAKERESCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Brake Res", ViewModelLocator.MainViewModel.BRAKERES);
                }
                if (ViewModelLocator.MainViewModel.ISMAINCTCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Main CT", ViewModelLocator.MainViewModel.MAINCT);
                }
                if (ViewModelLocator.MainViewModel.ISBRAKECTCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Brake CT", ViewModelLocator.MainViewModel.BRAKECT);
                }
                if (ViewModelLocator.MainViewModel.ISRUNINVERTERCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Run Inverter", ViewModelLocator.MainViewModel.RUNINVERTER);
                }
                if (ViewModelLocator.MainViewModel.ISMODINDEXCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Mod index", ViewModelLocator.MainViewModel.MODINDEX);
                }
                if (ViewModelLocator.MainViewModel.ISFREQCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Freq", ViewModelLocator.MainViewModel.FREQ);
                }
#if OLD_CODE
                if (ViewModelLocator.MainViewModel.ISAMODULATIONCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("A modulation", ViewModelLocator.MainViewModel.AMODULATION);
                }
                if (ViewModelLocator.MainViewModel.ISBMODULATIONCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("B modulation", ViewModelLocator.MainViewModel.BMODULATION);
                }
                if (ViewModelLocator.MainViewModel.ISCMODULATIONCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("C modulation", ViewModelLocator.MainViewModel.CMODULATION);
                }
#endif
                if (ViewModelLocator.MainViewModel.ISFAULTCODECHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Fault code", ViewModelLocator.MainViewModel.FAULTCODE);
                }
                if (ViewModelLocator.MainViewModel.ISSPEEDCMDRPMCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Speed Cmd RPM", ViewModelLocator.MainViewModel.SPEEDCMDRPM);
                }
                if (ViewModelLocator.MainViewModel.ISBRAKEOUTPUTCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Brake Output", ViewModelLocator.MainViewModel.BRAKEOUTPUT);
                }
                if (ViewModelLocator.MainViewModel.ISTHROTTLEBRAKECHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Throttle Brake", ViewModelLocator.MainViewModel.THROTTLEBRAKE);
                }
                if (ViewModelLocator.MainViewModel.ISFORWARDSPEEDCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Forward Speed", ViewModelLocator.MainViewModel.FORWARDSPEED);
                }
                if (ViewModelLocator.MainViewModel.ISREVERSESPEEDCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Reverse Speed", ViewModelLocator.MainViewModel.REVERSESPEED);
                }
#if WHEN_THROTTLE_WAS_READ_ONLY
                if (ViewModelLocator.MainViewModel.ISTHROTTLEMINCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Throttle Min", ViewModelLocator.MainViewModel.THROTTLEMIN);
                }

                if (ViewModelLocator.MainViewModel.ISTHROTTLEMAXCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Throttle Max", ViewModelLocator.MainViewModel.THROTTLEMAX);
                }
#endif
                if (ViewModelLocator.MainViewModel.ISSTATECHECKED)
                {
                    OxyPlotPage.AddPointToGraph("State", ViewModelLocator.MainViewModel.STATE);
                }
                if (ViewModelLocator.MainViewModel.ISSTATEINDEXCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("State Index", ViewModelLocator.MainViewModel.STATEINDEX);
                }
                if (ViewModelLocator.MainViewModel.ISCONFIGURINGTHROTTLECHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Configuring Throttle", ViewModelLocator.MainViewModel.CONFIGURINGTHROTTLE);
                }
                if (ViewModelLocator.MainViewModel.ISOVERCURRENTERRORSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Over Current Errors", ViewModelLocator.MainViewModel.OVERCURRENTERRORS);
                }
                if (ViewModelLocator.MainViewModel.ISENCODERISRCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Encoder ISR", ViewModelLocator.MainViewModel.ENCODERISR);
                }
                if (ViewModelLocator.MainViewModel.ISPWMISRCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("PWM ISR", ViewModelLocator.MainViewModel.PWMISR);
                }
                if (ViewModelLocator.MainViewModel.ISENCODERISRCOUNTSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Encoder ISR counts", ViewModelLocator.MainViewModel.ENCODERISRCOUNTS);
                }
                if (ViewModelLocator.MainViewModel.ISENCODERBADSEQCNTCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Encoder bad seq cnt", ViewModelLocator.MainViewModel.ENCODERBADSEQCNT);
                }
                if (ViewModelLocator.MainViewModel.ISENCODERDIRCHNGCNTCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Encoder dir chng cnt", ViewModelLocator.MainViewModel.ENCODERDIRCHNGCNT);
                }
                if (ViewModelLocator.MainViewModel.ISENCODERTIMEOUTSCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Encoder Timeouts cnt", ViewModelLocator.MainViewModel.ENCODERTIMEOUTS);
                }
                if (ViewModelLocator.MainViewModel.ISONEMSTASKCHECKED)
                {
                    OxyPlotPage.AddPointToGraph("Onems task", ViewModelLocator.MainViewModel.ONEMSTASK);
                }

            }

        }



        void OnCheckBoxChanged(object sender, bool isChecked)
        {

            if (isChecked)
            {
            }
            else
            {
            }
        }
    }
}
