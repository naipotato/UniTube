using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using UniTube.Core.Responses;
using UniTube.Framework.Utils;

namespace UniTube.Core.Requests
{
    public class VideoListRequest : RequestBase<VideoListResponse>
    {
        private string _part;

        internal VideoListRequest(string part) => _part = part;

        /// <summary>
        /// Identifies the chart that you want to retrieve.
        /// </summary>
        public ChartEnum? Chart { get; set; }

        /// <summary>
        /// Specifies a comma-separated list of the YouTube video ID(s) for the resource(s) that are being retrieved.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Set this parameter's value to <see cref="MyRatingEnum.Like"/> or <see cref="MyRatingEnum.Dislike"/> to
        /// instruct the API to only return videos liked or disliked by the authenticated user.
        /// </summary>
        public MyRatingEnum? MyRating { get; set; }

        /// <summary>
        /// Instructs the API to retrieve localized resource metadata for a specific application language that the
        /// YouTube website supports.
        /// </summary>
        public string Hl { get; set; }

        /// <summary>
        /// Specifies the maximum height of the embedded player returned in the
        /// <see cref="Resources.Video.Player.EmbedHtml"/> property.
        /// </summary>
        public uint? MaxHeight { get; set; }

        /// <summary>
        /// Specifies the maximum number of items that should be returned in the result set.
        /// </summary>
        public uint? MaxResults { get; set; }

        /// <summary>
        /// Specifies the maximum width of the embedded player returned in the
        /// <see cref="Resources.Video.Player.EmbedHtml"/> property.
        /// </summary>
        public uint? MaxWidth { get; set; }

        /// <summary>
        /// Indicates that the request's authorization credentials identify a YouTube CMS user who is acting on behalf
        /// of the content owner specified in the parameter value.
        /// </summary>
        public string OnBehalfOfContentOwner { get; set; }

        /// <summary>
        /// Identifies a specific page in the result set that should be returned.
        /// </summary>
        public string PageToken { get; set; }

        /// <summary>
        /// Instructs the API to select a video chart available in the specified region.
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Identifies the video category for which the chart should be retrieved.
        /// </summary>
        public string VideoCategoryId { get; set; }

        public override bool Equals(object obj)
        {
            var request = obj is VideoListRequest ? obj as VideoListRequest : null;
            return request != null &&
                   base.Equals(obj) &&
                   _part == request._part &&
                   EqualityComparer<ChartEnum?>.Default.Equals(Chart, request.Chart) &&
                   Id == request.Id &&
                   EqualityComparer<MyRatingEnum?>.Default.Equals(MyRating, request.MyRating) &&
                   Hl == request.Hl &&
                   EqualityComparer<uint?>.Default.Equals(MaxHeight, request.MaxHeight) &&
                   EqualityComparer<uint?>.Default.Equals(MaxResults, request.MaxResults) &&
                   EqualityComparer<uint?>.Default.Equals(MaxWidth, request.MaxWidth) &&
                   OnBehalfOfContentOwner == request.OnBehalfOfContentOwner &&
                   PageToken == request.PageToken &&
                   RegionCode == request.RegionCode &&
                   VideoCategoryId == request.VideoCategoryId;
        }

        public override async Task<VideoListResponse> ExecuteAsync()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("part", _part),
                new KeyValuePair<string, string>("chart", Chart.ToString()),
                new KeyValuePair<string, string>("id", Id),
                new KeyValuePair<string, string>("myRating", MyRating.ToString()),
                new KeyValuePair<string, string>("hl", Hl),
                new KeyValuePair<string, string>("maxHeight", MaxHeight.ToString()),
                new KeyValuePair<string, string>("maxResults", MaxResults.ToString()),
                new KeyValuePair<string, string>("maxWidth", MaxWidth.ToString()),
                new KeyValuePair<string, string>("onBehalfOfContentOwner", OnBehalfOfContentOwner),
                new KeyValuePair<string, string>("pageToken", PageToken),
                new KeyValuePair<string, string>("regionCode", RegionCode),
                new KeyValuePair<string, string>("videoCategoryId", VideoCategoryId),
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
                var httpResponse = await httpClient.GetAsync($"https://www.googleapis.com/youtube/v3/videos?{parameters}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var videoListResponse = await JsonUtils.ToObjectAsync<VideoListResponse>(contentResponse);
                    return videoListResponse;
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
            var hashCode = 1761685064;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_part);
            hashCode = hashCode * -1521134295 + EqualityComparer<ChartEnum?>.Default.GetHashCode(Chart);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<MyRatingEnum?>.Default.GetHashCode(MyRating);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Hl);
            hashCode = hashCode * -1521134295 + EqualityComparer<uint?>.Default.GetHashCode(MaxHeight);
            hashCode = hashCode * -1521134295 + EqualityComparer<uint?>.Default.GetHashCode(MaxResults);
            hashCode = hashCode * -1521134295 + EqualityComparer<uint?>.Default.GetHashCode(MaxWidth);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OnBehalfOfContentOwner);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PageToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RegionCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(VideoCategoryId);
            return hashCode;
        }

        public enum ChartEnum
        {
            /// <summary>
            /// Return the most popular videos for the specified content region and video category.
            /// </summary>
            MostPopular = 0
        }

        public enum MyRatingEnum
        {
            /// <summary>
            /// Returns only videos disliked by the authenticated user.
            /// </summary>
            Dislike = 0,

            /// <summary>
            /// Returns only video liked by the authenticated user.
            /// </summary>
            Like = 1
        }
    }
}