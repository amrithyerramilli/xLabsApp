using System;
using Android.App;
using xLabsApp;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;
using Newtonsoft.Json;

[assembly: ExportRenderer(typeof(xLabsApp.LoginPage), typeof(xLabsApp.Droid.LoginPageRenderer))]
namespace xLabsApp.Droid
{
    public class LoginPageRenderer : PageRenderer
    {
        protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: App.OAuth.ClientId, // your OAuth2 client id
                scope: App.OAuth.Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                authorizeUrl: new Uri(App.OAuth.AuthorizeUrl), // the auth URL for the service
                redirectUrl: new Uri(App.OAuth.RedirectUrl)); // the redirect URL for the service

            auth.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    App.SuccessfulLoginAction.Invoke();
                    App.SaveToken(eventArgs.Account.Properties["access_token"]);
                    App.GetUserInfo();
                    // Use eventArgs.Account to do wonderful things
                    //var parameters = new Dictionary<string,string>();
                    //parameters.Add("fields","id,name,about,bio,address,age_range,birthday,posts");
                    //var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), parameters, eventArgs.Account);
                    //request.GetResponseAsync().ContinueWith(t =>
                    //{
                    //    if (t.IsFaulted)
                    //        Console.WriteLine("Error: " + t.Exception.InnerException.Message);
                    //    else
                    //    {
                    //        string json = t.Result.GetResponseText();
                    //        //AccountStore.Create(this).Save(eventArgs.Account, "Facebook");
                    //        Console.WriteLine(json);
                    //        var user = JsonConvert.DeserializeObject<User>(json);
                    //        App.user = user;
                    //        var profilePage = new ProfilePage()
                    //        {
                    //            Content = new Label()
                    //                {
                    //                    Text = "Profile Page - " + App.user.id,
                    //                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    //                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    //                }
                    //        };
                    //        App.profilePage = profilePage;
                    //        App._NavPage.Navigation.RemovePage(App.profilePage);
                    //        App._NavPage.Navigation.PushAsync(profilePage);
                    //    }
                    //});
                    
                }
                else
                {
                    // The user cancelled
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}