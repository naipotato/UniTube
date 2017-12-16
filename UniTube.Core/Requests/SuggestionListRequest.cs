using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using UniTube.Framework.Utils;

namespace UniTube.Core.Requests
{
    public class SuggestionListRequest
    {
        private string _q;
        private ClientTypeEnum _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuggestionListRequest"/> class.
        /// </summary>
        /// <param name="q">Specifies the search to complete.</param>
        /// <param name="client">Determines the type of response you want.</param>
        public SuggestionListRequest(string q, ClientTypeEnum client)
        {
            _q = q;
            _client = client;
        }

        /// <summary>
        /// Specifies the name of the JSONP callback function.
        /// </summary>
        public string Jsonp { get; set; }

        /// <summary>
        /// Include this option to restrict the search to a particular site.
        /// </summary>
        public string Ds { get; set; }

        /// <summary>
        /// Chooses the language in which the search is being performed.
        /// </summary>
        public string Hl { get; set; }

        public override bool Equals(object obj)
        {
            var request = obj is SuggestionListRequest ? obj as SuggestionListRequest : null;
            return request != null &&
                   _q == request._q &&
                   _client == request._client &&
                   Jsonp == request.Jsonp &&
                   Ds == request.Ds &&
                   Hl == request.Hl;
        }

        public async Task<List<string>> ExecuteAsync()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("ds", Ds),
                new KeyValuePair<string, string>("hl", Hl),
                new KeyValuePair<string, string>("jsonp", Jsonp),
                new KeyValuePair<string, string>("client", _client.ToString()),
                new KeyValuePair<string, string>("q", _q)
            };

            var parameters = JoinPairs(pairs);

            var httpClient = new HttpClient();

            try
            {
                var httpResponse = await httpClient.GetAsync($"http://suggestqueries.google.com/complete/search?{parameters}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var listObject = await JsonUtils.ToObjectAsync<List<object>>(contentResponse);
                    var listString = (listObject[1] as JArray).Select((c) => (string)c).ToList();
                    return listString;
                }
                else
                    throw new Exception($"Error {httpResponse.StatusCode}\n{httpResponse.ReasonPhrase}\n{await httpResponse.Content.ReadAsStringAsync()}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Exception while request", ex);
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 1667496172;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_q);
            hashCode = hashCode * -1521134295 + _client.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Jsonp);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Ds);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Hl);
            return hashCode;
        }

        private string JoinPairs(List<KeyValuePair<string, string>> pairs)
        {
            var joinedPairs = new List<string>();

            foreach (var item in pairs)
            {
                if (item.Value != null)
                    joinedPairs.Add(WebUtility.UrlEncode(item.Key) + "=" + WebUtility.UrlEncode(item.Value));
            }

            return string.Join("&", joinedPairs);
        }

        public override string ToString()
            => $"{_q}\n" +
            $"Client: {_client}\n" +
            $"Jsonp: {Jsonp}\n" +
            $"Ds: {Ds}\n" +
            $"Hl: {Hl}";

        public enum ClientTypeEnum
        {
            /// <summary>
            /// Return a JSON.
            /// </summary>
            Firefox = 0,

            /// <summary>
            /// Return a XML
            /// </summary>
            Toolbar = 1,

            /// <summary>
            /// Return a JSONP.
            /// </summary>
            YouTube = 2
        }
    }
}
