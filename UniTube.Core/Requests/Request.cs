namespace UniTube.Core.Requests
{
    public class Request
    {
        /// <summary>
        /// OAuth 2.0 token for the current user.
        /// </summary>
        public string Access_Token { get; set; }
        /// <summary>
        /// Callback function.
        /// </summary>
        public string Callback { get; set; }
        /// <summary>
        /// Selector specifying a subset of fields to include in the response.
        /// </summary>
        public string Fields { get; set; }
        /// <summary>
        /// API key. Required unless you provide an OAuth 2.0 token.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Returns response with indentations and line breaks.
        /// </summary>
        public string PrettyPrint { get; set; }
        /// <summary>
        /// Alternative to userIp.
        /// </summary>
        public string QuotaUser { get; set; }
        /// <summary>
        /// IP address of the end user for whom the API call is being made.
        /// </summary>
        public string UserIp { get; set; }
    }
}
