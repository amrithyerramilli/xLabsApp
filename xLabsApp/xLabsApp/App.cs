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
        public App()
        {            
            OAuth = new OAuth (
                                    clientId: "1639469262962972",  		// your OAuth2 client id 
                                    scope: "public_profile,email,user_friends,user_about_me,user_status",  		// The scopes for the particular API you're accessing. The format for this will vary by API.
                                    authorizeUrl: "https://m.facebook.com/dialog/oauth/",  	// the auth URL for the service
                                    redirectUrl: "http://www.facebook.com/connect/login_success.html");
            user = new User() { name = "" };            
            _NavPage = new NavigationPage(new ProfilePage());
            MainPage = _NavPage;
        }
        public static NavigationPage _NavPage;        
        public static bool IsAuthenticated;
        public static string AccessToken;
        public static OAuth OAuth;
        public static User user;
        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() => _NavPage.Navigation.PopModalAsync());
            }
        }
        public static void SaveToken(string token)
        {
            AccessToken = token;

            // broadcast a message that authentication was successful
            //MessagingCenter.Send<App>(this, "Authenticated");
        }        
        public static async void GetUserInfo()
        {
            var client = new HttpClient();
            var access_token = AccessToken;
            var apiUrl = "https://graph.facebook.com/v2.4/me?fields=id,name,posts{message},about,bio&access_token=" + access_token;            
            
            var task = await client.GetAsync(apiUrl);
            if (null == task)
                throw new Exception("");
            else
            {
                try
                {
                    var jsonString = await task.Content.ReadAsStringAsync();
                    var x = JsonConvert.DeserializeObject<User>(jsonString);
                    App.user = x;

                    // make api call to watson to fetch personality profile

                    JObject jsonDoc;
                    //var postData = new JObject();
                    //postData.Add("text", "Hi, I am Amrith Yerramilli. Currently in the middle of a hackathon.");
                    //string text = postData.ToString();

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
                        App.user.personality = y.ToString();
                    }
                    //using ()
                    //{
                    //    using ()
                    //    {
                    //        //        client2.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
                    //        //Convert.ToBase64String(
                    //        //    System.Text.ASCIIEncoding.ASCII.GetBytes(
                    //        //        string.Format("{0}:{1}", username, password))));
                    //        //client2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            
                            
                            
                    //    }
                    //}                    
                    
                }
                catch (Exception ex)
                {
                    // log deserialization error;
                    throw;
                }

            }
        }
        private static void ParseAndDisplay(JObject json)
        {

            var a = json.ToString();

        }
        //public static async Task<User> GetData()
        //{
            
        //}
        //public static async Task<int> GetData()
        //{
        //    try
        //        {
        //            //JObject jsonDoc = null;
        //            var client = new HttpClient();
        //            var access_token = AccessToken;
        //            var apiUrl = "https://graph.facebook.com/v2.4/me?fields=id,name,posts{message},about,bio&access_token=" + access_token;
        //            var task = await  client.GetAsync(apiUrl).ConfigureAwait(false);
        //            string xyz = await task;
        //            return xyz.Length;
                    
                
                
                
                
                
        //        //if (response.IsSuccessStatusCode)
        //            //{
        //            //    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            //    //return await Task.Run (() => JObject.Parse (jsonOut));
        //            //}
        //            //return jsonDoc;

        //        //    HttpResponseMessage response = client.GetAsync(apiUrl).ConfigureAwait(false);
        //        //    string contents = await response.;
        //        //var blah = contents.Length;
        //        //return blah;
        //            //return JsonConvert.DeserializeObject<User>(x);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }  
        //}

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
