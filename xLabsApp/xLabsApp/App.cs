using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace xLabsApp
{
    public class App : Application
    {
        public static NavigationPage _NavPage;
        public static AppData _AppDataInstance;
        public App()
        {
            _AppDataInstance = AppData.Instance;
            _NavPage = new NavigationPage(new MainPage());
            MainPage = _NavPage;
        }
        public static void SaveToken(string token)
        {
            _AppDataInstance.AccessToken = token;
            // broadcast a message that authentication was successful
            //MessagingCenter.Send<App>(this, "Authenticated");
        }
        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() => _NavPage.Navigation.PopModalAsync());
            }
        }
        public static async void GetUserInfo()
        {
            //Make api call to fetch user details from Facebook.
            var client = new HttpClient();
            var access_token = _AppDataInstance.AccessToken;
            var apiUrl = "https://graph.facebook.com/v2.4/me?fields=id,name,posts{message},about,bio&access_token=" + access_token;

            var getUserDetailsTask = await client.GetAsync(apiUrl);
            if (getUserDetailsTask.IsSuccessStatusCode)
            {
                var responseJsonString = await getUserDetailsTask.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<User>(responseJsonString);
                _AppDataInstance.User = jsonData;

                //Make api call to xLabsApi - Watson interface to fetch user's personality insights
                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("text", @"Call me Ishmael. Some years ago-never mind how long precisely-having little or no money in my purse,
and nothing particular to interest me on shore, I thought I would sail about a little and see the watery
part of the world. It is a way I have of driving off the spleen and regulating the circulation. Whenever
I find myself growing grim about the mouth; whenever it is a damp, drizzly November in my soul; whenever
I find myself involuntarily pausing before coffin warehouses, and bringing up the rear of every funeral
I meet; and especially whenever my hypos get such an upper hand of me, that it requires a strong moral Call me Ishmael. Some years ago-never mind how long precisely-having little or no money in my purse,
and nothing particular to interest me on shore, I thought I would sail about a little and see the watery
part of the world. It is a way I have of driving off the spleen and regulating the circulation. Whenever
I find myself growing grim about the mouth; whenever it is a damp, drizzly November in my soul; whenever
I find myself involuntarily pausing before coffin warehouses, and bringing up the rear of every funeral
I meet; and especially whenever my hypos get such an upper hand of me, that it requires a strong moral"));
                var content = new FormUrlEncodedContent(values);
                var client2 = new HttpClient();
                var response = await client2.PostAsync("http://xlab.mybluemix.net/map", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var jsonOut = await response.Content.ReadAsStringAsync();
                    var y = JsonConvert.DeserializeObject(jsonOut);
                    _AppDataInstance.User.personality = y.ToString();
                }
            }
            else
                throw new Exception("Oops. Something went wrong while fetching the user details from Facebook.");
        }
        
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
