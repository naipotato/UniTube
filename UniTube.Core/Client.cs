using System;
using System.Collections.Generic;

namespace UniTube.Core
{
    public static partial class Client
    {
        public static readonly string AuthUri = "https://accounts.google.com/o/oauth2/auth";
        public static readonly List<string> RedirectUris = new List<string> { "urn:ietf:wg:oauth:2.0:oob", "http://localhost" };
        public static readonly Uri TokenUri = new Uri("https://accounts.google.com/o/oauth2/token");

        public static readonly string ApiKey;
        public static readonly string ClientId;
        public static readonly string ClientSecret;
    }
}
