using System;
using System.Collections.Generic;

namespace UniTube.Core
{
    public static partial class Client
    {
        public static readonly string authUri;
        public static readonly List<string> redirectUris;
        public static readonly Uri tokenUri;

        public static readonly string apiKey;
        public static readonly string clientId;
        public static readonly string clientSecret;
    }
}
