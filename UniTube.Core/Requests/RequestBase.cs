using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UniTube.Core.Requests
{
    public abstract class RequestBase<T>
    {
        /// <summary>
        /// OAuth 2.0 token for the current user.
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
        /// API key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Returns response with indentations and line breaks.
        /// </summary>
        public string PrettyPrint { get; set; }

        /// <summary>
        /// Alternative to <see cref="UserIp"/>.
        /// </summary>
        public string QuotaUser { get; set; }

        /// <summary>
        /// IP address of the end user for whom the API call is being made.
        /// </summary>
        public string UserIp { get; set; }

        public override bool Equals(object obj)
        {
            var @base = obj is RequestBase<T> ? obj as RequestBase<T> : null;
            return @base != null &&
                   Access_Token == @base.Access_Token &&
                   Callback == @base.Callback &&
                   Fields == @base.Fields &&
                   Key == @base.Key &&
                   PrettyPrint == @base.PrettyPrint &&
                   QuotaUser == @base.QuotaUser &&
                   UserIp == @base.UserIp;
        }

        public abstract Task<T> ExecuteAsync();

        public override int GetHashCode()
        {
            var hashCode = -1436095278;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Access_Token);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Callback);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Fields);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Key);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PrettyPrint);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(QuotaUser);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserIp);
            return hashCode;
        }

        protected string JoinPairs(List<KeyValuePair<string, string>> pairs)
        {
            var joinedPairs = new List<string>();

            foreach (var item in pairs)
            {
                if (!string.IsNullOrEmpty(item.Value))
                    joinedPairs.Add(WebUtility.UrlEncode(item.Key) + "=" + WebUtility.UrlEncode(item.Value));
            }

            return string.Join("&", joinedPairs);
        }
    }
}
