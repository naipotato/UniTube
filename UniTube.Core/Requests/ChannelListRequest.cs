using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using UniTube.Core.Responses;

namespace UniTube.Core.Requests
{
    public class ChannelListRequest : RequestBase<ChannelListResponse>
    {
        private string _part;

        internal ChannelListRequest(string part) => _part = part;

        /// <summary>
        /// Specifies a YouTube guide category, thereby requesting YouTube channels asssociated with that category.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Specifies a YouTube username, thereby requesting the channel associated with that username.
        /// </summary>
        public string ForUsername { get; set; }

        /// <summary>
        /// Specifies a comma-separated list of the YouTube channel ID(s) for the resource(s) that are being retrieved.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Set this parameter to true to instruct the API to only return channels managed by the content owner that
        /// the <see cref="OnBehalfOfContentOwner"/> parameter specifies.
        /// </summary>
        public bool? ManagedByMe { get; set; }

        /// <summary>
        /// Set this parameter's value to true to instruct the API to only return channels owned by the authenticated
        /// user.
        /// </summary>
        public bool? Mine { get; set; }

        /// <summary>
        /// Instructs the API to retrieve localized resource metadata for a specific application language that the
        /// YouTube website supports.
        /// </summary>
        public string Hl { get; set; }

        /// <summary>
        /// Specifies the maximum number of items that should be returned in the result set.
        /// </summary>
        public uint? MaxResults { get; set; }

        /// <summary>
        /// Indicates that the request's authorization credentials identify a YouTube CMS user who is acting on behalf
        /// of the content owner specified in the parameter value.
        /// </summary>
        public string OnBehalfOfContentOwner { get; set; }

        /// <summary>
        /// Identifies a specific page in the result set that should be returned.
        /// </summary>
        public string PageToken { get; set; }

        public override bool Equals(object obj)
        {
            var request = obj is ChannelListRequest ? obj as ChannelListRequest : null;
            return request != null &&
                   _part == request._part &&
                   CategoryId == request.CategoryId &&
                   ForUsername == request.ForUsername &&
                   Id == request.Id &&
                   EqualityComparer<bool?>.Default.Equals(ManagedByMe, request.ManagedByMe) &&
                   EqualityComparer<bool?>.Default.Equals(Mine, request.Mine) &&
                   Hl == request.Hl &&
                   EqualityComparer<uint?>.Default.Equals(MaxResults, request.MaxResults) &&
                   OnBehalfOfContentOwner == request.OnBehalfOfContentOwner &&
                   PageToken == request.PageToken;
        }

        public override async Task<ChannelListResponse> ExecuteAsync()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("part", _part),
                new KeyValuePair<string, string>("categoryId", CategoryId),
                new KeyValuePair<string, string>("forUsername", ForUsername),
                new KeyValuePair<string, string>("id", Id),
                new KeyValuePair<string, string>("managedByMe", ManagedByMe.ToString()),
                new KeyValuePair<string, string>("mine", Mine.ToString()),
                new KeyValuePair<string, string>("maxResults", MaxResults.ToString()),
                new KeyValuePair<string, string>("onBehalfOfContentOwner", OnBehalfOfContentOwner),
                new KeyValuePair<string, string>("pageToken", PageToken),
                new KeyValuePair<string, string>("access_token", Access_Token),
                new KeyValuePair<string, string>("callback", Callback),
                new KeyValuePair<string, string>("fields", Fields),
                new KeyValuePair<string, string>("key", Key),
                new KeyValuePair<string, string>("prettyPrint", PrettyPrint),
                new KeyValuePair<string, string>("quotaUser", QuotaUser),
                new KeyValuePair<string, string>("userIp", UserIp)
            };

            var parameters = JoinPairs(pairs);

            var httpClient = new HttpClient();

            try
            {
                var httpResponse = await httpClient.GetAsync($"https://www.googleapis.com/youtube/v3/channels?{parameters}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var channelListResponse = JsonConvert.DeserializeObject<ChannelListResponse>(contentResponse);
                    return channelListResponse;
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
            var hashCode = 454406104;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_part);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ForUsername);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ManagedByMe);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(Mine);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Hl);
            hashCode = hashCode * -1521134295 + EqualityComparer<uint?>.Default.GetHashCode(MaxResults);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OnBehalfOfContentOwner);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PageToken);
            return hashCode;
        }
    }
}