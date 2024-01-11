using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace NavitasBeta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        private ListView listView;
        public ListView ListView { get { return listView; } }

        //ListView listView;
        public ObservableCollection<MasterPageItem> masterPageItems;

        public MasterPage()
        {
            InitializeComponent();

            masterPageItems = new ObservableCollection<MasterPageItem>
            {
                new MasterPageItem
                {
                    Title = "Dash Board",
                    //IconSource = "contacts.png",
                    TargetType = typeof(UserTabbedPage)
                },
                new MasterPageItem
                {
                    Title = "Communications",
                    //IconSource = "contacts.png",
                    TargetType = typeof(DeviceListPage)
                },
            };

            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };


            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { listView }
            };
        }

        //private void tapped(object sender, EventArgs e)
        //{
        //    tapHandled = false;
        //    Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 0, 0, 300), taptimer);
        //}
        //private void doubletapped(object sender, EventArgs e)
        //{
        //    tapHandled = true;
        //    // do stuff here
        //}
        //private bool taptimer()
        //{
        //    if (!tapHandled)
        //    {
        //        tapHandled = true;
        //        // do stuff here
        //    }
        //    return false;
        //}
    }
}

