using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace xLabsApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        async void DoLogin(object sender, EventArgs e)
        {
            App._NavPage.Navigation.PushModalAsync(new LoginPage());
            loginButton.IsVisible = false;
        }
        public async void SeeReviews(object sender, EventArgs e)
        {
            //ListView lv = new ListView();            
            //lv.ItemsSource = new [] { "a", "b", "c" };

            //var reviewPage = new ContentPage
            //{
            //    Content = lv
            //};
            //reviewPage.Title = "Personalized Reviews";
            //if (Device.OS == TargetPlatform.iOS)
            //{
            //    reviewPage.Padding = new Thickness (0, 35, 0, 0);
            //}

            await Navigation.PushAsync(new ReviewPage());
            DisplayData();
        }
        async void DisplayData()
        {

        }
    }
}
