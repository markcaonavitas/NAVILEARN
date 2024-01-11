using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using NavitasBeta.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NavitasBeta.CustomSwitch), typeof(CustomSwitchRenderer))]


namespace NavitasBeta.Droid
{
    class CustomSwitchRenderer : SwitchRenderer
    {

     

        public override bool DispatchTouchEvent(MotionEvent e)
        {
            var view = (CustomSwitch)Element;
            if (e.Action == MotionEventActions.Down)
            {
      //          System.Diagnostics.Debug.WriteLine("Switch view.Focus()");
                view.Focus();
            }
            if(e.Action == MotionEventActions.Up)
            {
    //            System.Diagnostics.Debug.WriteLine("Switch view.Unfocus()");
                view.Unfocus();
            }

            return base.DispatchTouchEvent(e);
        }



#if OLD_CODE
         public override IOnFocusChangeListener OnFocusChangeListener
        {
            get => base.OnFocusChangeListener;
            set => base.OnFocusChangeListener = value;
        }
           public override bool CallOnClick()
        {
            return base.CallOnClick();
        }

        public override bool Activated
        {
            get => base.Activated; set => base.Activated = value;

        }

        public override void ClearFocus()
        {
            base.ClearFocus();
        }

        public override void ClearChildFocus(Android.Views.View child)
        {
            base.ClearChildFocus(child);
        }
        public override void OnWindowFocusChanged(bool hasWindowFocus)
        {
            var view = (CustomSwitch)Element;
            if (hasWindowFocus)
            {
                
                view.Focus();

            }
            else
            {
                view.Unfocus();
            }
            base.OnWindowFocusChanged(hasWindowFocus);
        }

        public override IOnFocusChangeListener OnFocusChangeListener
        {
            get => base.OnFocusChangeListener;
            set => base.OnFocusChangeListener = value;
        }
#endif
    }
}