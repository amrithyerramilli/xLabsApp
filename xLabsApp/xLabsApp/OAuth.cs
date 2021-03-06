﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xLabsApp
{
    public class OAuth
    {
        public OAuth(
			string clientId,
			string scope,
			string authorizeUrl,
			string redirectUrl)
		{
			ClientId = clientId;
			Scope = scope;
			AuthorizeUrl = authorizeUrl;
			RedirectUrl = redirectUrl;
		}

		public string ClientId {get; private set;}
		public string Scope {get; private set;}
		public string AuthorizeUrl {get; private set;}
		public string RedirectUrl {get; private set;}
    }
}
