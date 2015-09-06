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
                clientId: App._AppDataInstance.OAuth.ClientId, // your OAuth2 client id
                scope: App._AppDataInstance.OAuth.Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                authorizeUrl: new Uri(App._AppDataInstance.OAuth.AuthorizeUrl), // the auth URL for the service
                redirectUrl: new Uri(App._AppDataInstance.OAuth.RedirectUrl)); // the redirect URL for the service

            auth.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    // Use eventArgs.Account to do wonderful things
                    App.SuccessfulLoginAction.Invoke();
                    App.SaveToken(eventArgs.Account.Properties["access_token"]);
                    App.GetUserInfo();
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