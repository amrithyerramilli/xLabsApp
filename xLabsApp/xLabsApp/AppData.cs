using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace xLabsApp
{
    public class AppData
    {
        private static AppData _instance = null;
        private static readonly object padlock = new object();
        private AppData()
        {
            OAuth = new OAuth(clientId: "1639469262962972",  		// your OAuth2 client id 
                                scope: "public_profile,email,user_friends,user_about_me,user_status",  		// The scopes for the particular API you're accessing. The format for this will vary by API.
                                authorizeUrl: "https://m.facebook.com/dialog/oauth/",  	// the auth URL for the service
                                redirectUrl: "http://www.facebook.com/connect/login_success.html");
            User = new User();
        }
        public static AppData Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                        _instance = new AppData();
                    return _instance;
                }
            }
        }
        public bool IsAuthenticated { get {  return !String.IsNullOrEmpty(AccessToken);} }
        public string AccessToken;
        public OAuth OAuth;
        public User User;
    }
}
