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
    public partial class CustomOverlay : ContentView
    {
        public CustomOverlay()
        {
            InitializeComponent();
        }

        public AbsoluteLayout _parent;
        public void Show(AbsoluteLayout parent)
        {
            _parent = parent;
            if (_parent != null)
            {
                AbsoluteLayout.SetLayoutFlags(this, AbsoluteLayoutFlags.All);
                AbsoluteLayout.SetLayoutBounds(this, new Rectangle(0, 0, 1, 1));
                _parent.Children.Add(this);
            }

        }

        Animation swipeDownAnimation;
        public void StartAnimation()
        {
            if(swipeDownAnimation == null)
                swipeDownAnimation = new Animation(v => SwipeDownFinger.TranslationY = v, 0, 60);

            Device.BeginInvokeOnMainThread(() =>
            {
                swipeDownAnimation.Commit(this,
                    "SwipeDownAnimation",
                    16,
                    3000,
                    Easing.CubicInOut,
                    null,
                    () => true);
            });
        }
        Animation animation;
        public void StartAnimation(View view, string animationName)
        {
            if (animation == null)
                animation = new Animation(v => view.Scale = v, 1, 1.2);

            Device.BeginInvokeOnMainThread(() =>
            {
                animation.Commit(this,
                   animationName,
                    16,
                    1500,
                    Easing.CubicInOut,
                    null,
                    () => true);
            });
        }

        public void StopAnimation()
        {
            this.AbortAnimation("SwipeDownAnimation");
        }

        public void StopAnimation(string animationName)
        {
            this.AbortAnimation(animationName);
        }

        public async void Hide()
        {
            if (_parent != null)
            {
                await Task.Delay(100);
                _parent.Children.Remove(this);
            }
        }
    }
}