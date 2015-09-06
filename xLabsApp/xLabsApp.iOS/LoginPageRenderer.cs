using System;
using Xamarin.Auth;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using OAuthTwoDemo.XForms;
using OAuthTwoDemo.XForms.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace OAuthTwoDemo.XForms.iOS
{
    public class LoginPageRenderer : PageRenderer
    {

        bool IsShown;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (!IsShown)
            {

                IsShown = true;

                var auth = new OAuth2Authenticator(
                    clientId: App.OAuth.ClientId, // your OAuth2 client id
                    scope: App.OAuth.Scope, // The scopes for the particular API you're accessing. The format for this will vary by API.
                    authorizeUrl: new Uri(App.OAuth.AuthorizeUrl), // the auth URL for the service
                    redirectUrl: new Uri(App.OAuth.RedirectUrl)); // the redirect URL for the service

                auth.Completed += (sender, eventArgs) =>
                {
                    // We presented the UI, so it's up to us to dimiss it on iOS.
                    App.SuccessfulLoginAction.Invoke();

                    if (eventArgs.IsAuthenticated)
                    {
                        // Use eventArgs.Account to do wonderful things
                        App.SaveToken(eventArgs.Account.Properties["access_token"]);
                    }
                    else
                    {
                        // The user cancelled
                    }
                };

                PresentViewController(auth.GetUI(), true, null);

            }

        }
    }
}

