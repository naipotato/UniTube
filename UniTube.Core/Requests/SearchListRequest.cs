using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using UniTube.Core.Responses;
using UniTube.Framework.Utils;

namespace UniTube.Core.Requests
{
    public class SearchListRequest : RequestBase<SearchListResponse>
    {
        private string _part;

        internal SearchListRequest(string part) => _part = part;

        /// <summary>
        /// Restricts the search to only retrieve videos owned by the content owner identified by the
        /// <see cref="OnBehalfOfContentOwner"/> parameter.
        /// </summary>
        public string ForContentOwner { get; set; }

        /// <summary>
        /// Restricts the search to only retrieve videos uploaded via the developer's application or website.
        /// </summary>
        public bool? ForDeveloper { get; set; }

        /// <summary>
        /// Restricts the search to only retrieve videos owned by the authenticated user.
        /// </summary>
        public bool? ForMine { get; set; }

        /// <summary>
        /// Retrieves a list of videos that are related to the video that the parameter value identifies.
        /// </summary>
        public string RelatedToVideoId { get; set; }

        /// <summary>
        /// Indicates that the API response should only contain resources created by the channel.
        /// </summary>
        public string ChannelId { get; set; }

        /// <summary>
        /// Lets you restrict a search to a particular type of channel.
        /// </summary>
        public ChannelTypeEnum? ChannelType { get; set; }

        /// <summary>
        /// Restricts a search to broadcast events.
        /// </summary>
        public EventTypeEnum? EventType { get; set; }

        /// <summary>
        /// In conjunction with the <see cref="LocationRadius"/> parameter, defines a circular geographic area and also
        /// restricts a search to videos that specify, in their metadata, a geographic location that falls within that
        /// area.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// In conjunction with the <see cref="Location"/> parameter, defines a circular geographic area.
        /// </summary>
        public string LocationRadius { get; set; }

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
        /// Specifies the method that will be used to order resources in the API response.
        /// </summary>
        public OrderEnum? Order { get; set; }

        /// <summary>
        /// Identifies a specific page in the result set that should be returned.
        /// </summary>
        public string PageToken { get; set; }

        /// <summary>
        /// Indicates that the API response should only contain resources created at or after the specified time.
        /// </summary>
        public DateTime? PublishedAfter { get; set; }

        /// <summary>
        /// Indicates that the API response should only contain resources created before or at the specified time.
        /// </summary>
        public DateTime? PublishedBefore { get; set; }

        /// <summary>
        /// Specifies the query term to search for.
        /// </summary>
        public string Q { get; set; }

        /// <summary>
        /// Instructs the API to return search for videos that can be viewed in the specified country.
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// Instructs the API to return search results that are most relevant to the specified language.
        /// </summary>
        public string RelevanceLangugage { get; set; }

        /// <summary>
        /// Indicates whether the search results should include restricted content as well as standard content.
        /// </summary>
        public SafeSearchEnum? SafeSearch { get; set; }

        /// <summary>
        /// Indicates that the API response should only contain resources associated with the specified topic.
        /// </summary>
        public string TopicId { get; set; }

        /// <summary>
        /// Restricts a search query to only retrieve a particular type of resource.
        /// </summary>
        public TypeEnum? Type { get; set; }

        /// <summary>
        /// Indicates whether the API should filter video search results based on whether they have captions.
        /// </summary>
        public VideoCaptionEnum? VideoCaption { get; set; }

        /// <summary>
        /// Filters video search results based on their category.
        /// </summary>
        public string VideoCategoryId { get; set; }

        /// <summary>
        /// Lets you restrict a search to only include either high definition (HD) or standard definition (SD) videos.
        /// </summary>
        public VideoDefinitionEnum? VideoDefinition { get; set; }

        /// <summary>
        /// Lets you restrict a search to only retrieve 2D or 3D videos.
        /// </summary>
        public VideoDimensionEnum? VideoDimension { get; set; }

        /// <summary>
        /// Filters video search results based on their duration.
        /// </summary>
        public VideoDurationEnum? VideoDuration { get; set; }

        /// <summary>
        /// Lets you to restrict a search to only videos that can be embedded into a webpage.
        /// </summary>
        public VideoEmbeddableEnum? VideoEmbeddable { get; set; }

        /// <summary>
        /// Filters search results to only include videos with a particular license.
        /// </summary>
        public VideoLicenseEnum? VideoLicense { get; set; }

        /// <summary>
        /// Lets you to restrict a search to only videos that can be played outside youtube.com.
        /// </summary>
        public VideoSyndicatedEnum? VideoSyndicated { get; set; }

        /// <summary>
        /// Lets you restrict a search to a particular type of videos.
        /// </summary>
        public VideoTypeEnum? VideoType { get; set; }

        public override bool Equals(object obj)
        {
            var request = obj is SearchListRequest ? obj as SearchListRequest : null;
            return request != null &&
                   base.Equals(obj) &&
                   _part == request._part &&
                   ForContentOwner == request.ForContentOwner &&
                   EqualityComparer<bool?>.Default.Equals(ForDeveloper, request.ForDeveloper) &&
                   EqualityComparer<bool?>.Default.Equals(ForMine, request.ForMine) &&
                   RelatedToVideoId == request.RelatedToVideoId &&
                   ChannelId == request.ChannelId &&
                   EqualityComparer<ChannelTypeEnum?>.Default.Equals(ChannelType, request.ChannelType) &&
                   EqualityComparer<EventTypeEnum?>.Default.Equals(EventType, request.EventType) &&
                   Location == request.Location &&
                   LocationRadius == request.LocationRadius &&
                   EqualityComparer<uint?>.Default.Equals(MaxResults, request.MaxResults) &&
                   OnBehalfOfContentOwner == request.OnBehalfOfContentOwner &&
                   EqualityComparer<OrderEnum?>.Default.Equals(Order, request.Order) &&
                   PageToken == request.PageToken &&
                   EqualityComparer<DateTime?>.Default.Equals(PublishedAfter, request.PublishedAfter) &&
                   EqualityComparer<DateTime?>.Default.Equals(PublishedBefore, request.PublishedBefore) &&
                   Q == request.Q &&
                   RegionCode == request.RegionCode &&
                   RelevanceLangugage == request.RelevanceLangugage &&
                   EqualityComparer<SafeSearchEnum?>.Default.Equals(SafeSearch, request.SafeSearch) &&
                   TopicId == request.TopicId &&
                   EqualityComparer<TypeEnum?>.Default.Equals(Type, request.Type) &&
                   EqualityComparer<VideoCaptionEnum?>.Default.Equals(VideoCaption, request.VideoCaption) &&
                   VideoCategoryId == request.VideoCategoryId &&
                   EqualityComparer<VideoDefinitionEnum?>.Default.Equals(VideoDefinition, request.VideoDefinition) &&
                   EqualityComparer<VideoDimensionEnum?>.Default.Equals(VideoDimension, request.VideoDimension) &&
                   EqualityComparer<VideoDurationEnum?>.Default.Equals(VideoDuration, request.VideoDuration) &&
                   EqualityComparer<VideoEmbeddableEnum?>.Default.Equals(VideoEmbeddable, request.VideoEmbeddable) &&
                   EqualityComparer<VideoLicenseEnum?>.Default.Equals(VideoLicense, request.VideoLicense) &&
                   EqualityComparer<VideoSyndicatedEnum?>.Default.Equals(VideoSyndicated, request.VideoSyndicated) &&
                   EqualityComparer<VideoTypeEnum?>.Default.Equals(VideoType, request.VideoType);
        }

        public override async Task<SearchListResponse> ExecuteAsync()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("part", _part),
                new KeyValuePair<string, string>("forContentOwner", ForContentOwner),
                new KeyValuePair<string, string>("forDeveloper", ForDeveloper.ToString()),
                new KeyValuePair<string, string>("forMine", ForMine.ToString()),
                new KeyValuePair<string, string>("relatedToVideoId", RelatedToVideoId),
                new KeyValuePair<string, string>("channelId", ChannelId),
                new KeyValuePair<string, string>("channelType", ChannelType.ToString()),
                new KeyValuePair<string, string>("eventType", EventType.ToString()),
                new KeyValuePair<string, string>("location", Location),
                new KeyValuePair<string, string>("locationRadius", LocationRadius),
                new KeyValuePair<string, string>("maxResults", MaxResults.ToString()),
                new KeyValuePair<string, string>("onBehalfOfContentOwner", OnBehalfOfContentOwner),
                new KeyValuePair<string, string>("order", Order.ToString()),
                new KeyValuePair<string, string>("pageToken", PageToken),
                new KeyValuePair<string, string>("publishedAfter", PublishedAfter.ToString()),
                new KeyValuePair<string, string>("publishedBefore", PublishedBefore.ToString()),
                new KeyValuePair<string, string>("q", Q),
                new KeyValuePair<string, string>("regionCode", RegionCode),
                new KeyValuePair<string, string>("relevanceLanguage", RelevanceLangugage),
                new KeyValuePair<string, string>("safeSearch", SafeSearch.ToString()),
                new KeyValuePair<string, string>("topicId", TopicId),
                new KeyValuePair<string, string>("type", Type.ToString()),
                new KeyValuePair<string, string>("videoCaption", VideoCaption.ToString()),
                new KeyValuePair<string, string>("videoCategoryId", VideoCategoryId),
                new KeyValuePair<string, string>("videoDefinition", VideoDefinition.ToString()),
                new KeyValuePair<string, string>("videoDimension", VideoDimension.ToString()),
                new KeyValuePair<string, string>("videoDuration", VideoDuration.ToString()),
                new KeyValuePair<string, string>("videoEmbeddable", VideoEmbeddable.ToString()),
                new KeyValuePair<string, string>("videoLicense", VideoLicense.ToString()),
                new KeyValuePair<string, string>("videoSyndicated", VideoSyndicated.ToString()),
                new KeyValuePair<string, string>("videoType", VideoType.ToString()),
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
                var httpResponse = await httpClient.GetAsync($"https://www.googleapis.com/youtube/v3/search?{parameters}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var searchListResponse = await JsonUtils.ToObjectAsync<SearchListResponse>(contentResponse);
                    return searchListResponse;
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
            var hashCode = 581738635;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_part);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ForContentOwner);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ForDeveloper);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool?>.Default.GetHashCode(ForMine);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RelatedToVideoId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ChannelId);
            hashCode = hashCode * -1521134295 + EqualityComparer<ChannelTypeEnum?>.Default.GetHashCode(ChannelType);
            hashCode = hashCode * -1521134295 + EqualityComparer<EventTypeEnum?>.Default.GetHashCode(EventType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Location);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LocationRadius);
            hashCode = hashCode * -1521134295 + EqualityComparer<uint?>.Default.GetHashCode(MaxResults);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OnBehalfOfContentOwner);
            hashCode = hashCode * -1521134295 + EqualityComparer<OrderEnum?>.Default.GetHashCode(Order);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PageToken);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(PublishedAfter);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(PublishedBefore);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Q);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RegionCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RelevanceLangugage);
            hashCode = hashCode * -1521134295 + EqualityComparer<SafeSearchEnum?>.Default.GetHashCode(SafeSearch);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TopicId);
            hashCode = hashCode * -1521134295 + EqualityComparer<TypeEnum?>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoCaptionEnum?>.Default.GetHashCode(VideoCaption);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(VideoCategoryId);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoDefinitionEnum?>.Default.GetHashCode(VideoDefinition);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoDimensionEnum?>.Default.GetHashCode(VideoDimension);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoDurationEnum?>.Default.GetHashCode(VideoDuration);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoEmbeddableEnum?>.Default.GetHashCode(VideoEmbeddable);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoLicenseEnum?>.Default.GetHashCode(VideoLicense);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoSyndicatedEnum?>.Default.GetHashCode(VideoSyndicated);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoTypeEnum?>.Default.GetHashCode(VideoType);
            return hashCode;
        }

        public enum ChannelTypeEnum
        {
            /// <summary>
            /// Return all channels.
            /// </summary>
            Any = 0,

            /// <summary>
            /// Only retrieve shows.
            /// </summary>
            Show = 1
        }

        public enum EventTypeEnum
        {
            /// <summary>
            /// Only include completed broadcasts.
            /// </summary>
            Completed = 0,

            /// <summary>
            /// Only include active broadcasts.
            /// </summary>
            Live = 1,

            /// <summary>
            /// Only include upcoming broadcasts.
            /// </summary>
            Upcoming = 2
        }

        public enum OrderEnum
        {
            /// <summary>
            /// Resources are sorted in reverse chronological order based on the date they were created.
            /// </summary>
            Date = 0,

            /// <summary>
            /// Resources are sorted from highest to lowest rating.
            /// </summary>
            Rating = 1,

            /// <summary>
            /// Resources are sorted based on their relevance to the search query.
            /// </summary>
            Relevance = 2,

            /// <summary>
            /// Resources are sorted alphabetically by title.
            /// </summary>
            Title = 3,

            /// <summary>
            /// Channels are sorted in descending order of their number of uploaded videos.
            /// </summary>
            VideoCount = 4,

            /// <summary>
            /// Resources are sorted from highest to lowest number of views.
            /// </summary>
            ViewCount = 5
        }

        public enum SafeSearchEnum
        {
            /// <summary>
            /// YouTube will filter some content from search results and, at the least, will filter content that is
            /// restricted in your locale.
            /// </summary>
            Moderate = 0,

            /// <summary>
            /// YouTube will not filter the search result set.
            /// </summary>
            None = 1,

            /// <summary>
            /// YouTube will try to exclude all restricted content from the search result set.
            /// </summary>
            Strict = 2
        }

        public enum TypeEnum
        {
            Channel = 0,
            Playlist = 1,
            Video = 2
        }

        public enum VideoCaptionEnum
        {
            /// <summary>
            /// Do not filter results based on caption availability.
            /// </summary>
            Any = 0,

            /// <summary>
            /// Only include videos that have captions.
            /// </summary>
            ClosedCaption = 1,

            /// <summary>
            /// Only include videos that do not have captions.
            /// </summary>
            None = 2
        }

        public enum VideoDefinitionEnum
        {
            /// <summary>
            /// Return all videos, regardless of their resolution.
            /// </summary>
            Any = 0,

            /// <summary>
            /// Only retrieve HD videos.
            /// </summary>
            High = 1,

            /// <summary>
            /// Only retrieve videos in standard definition.
            /// </summary>
            Standard = 2
        }

        public enum VideoDimensionEnum
        {
            /// <summary>
            /// Restrict search results to exclude 3D videos.
            /// </summary>
            Value2d = 0,

            /// <summary>
            /// Restrict search to only include 3D videos.
            /// </summary>
            Value3d = 1,

            /// <summary>
            /// Include both 3D and non-3D videos in returned results.
            /// </summary>
            Any = 2
        }

        public enum VideoDurationEnum
        {
            /// <summary>
            /// Do not filter video search results based on their duration.
            /// </summary>
            Any = 0,

            /// <summary>
            /// Only include videos longer than 20 minutes.
            /// </summary>
            Long = 1,

            /// <summary>
            /// Only include videos that are between four and 20 minutes long (inclusive).
            /// </summary>
            Medium = 2,

            /// <summary>
            /// Only include videos that are less than four minutes long.
            /// </summary>
            Short = 3
        }

        public enum VideoEmbeddableEnum
        {
            /// <summary>
            /// Return all videos, embeddable or not.
            /// </summary>
            Any = 0,

            /// <summary>
            /// Only retrieve embeddable videos.
            /// </summary>
            True = 1
        }

        public enum VideoLicenseEnum
        {
            /// <summary>
            /// Return all videos, regardless of which license they have, that match the query parameters.
            /// </summary>
            Any = 0,

            /// <summary>
            /// Only return videos that have a Creative Commons license.
            /// </summary>
            CreativeCommon = 1,

            /// <summary>
            /// Only return videos that have the standard YouTube license.
            /// </summary>
            YouTube = 2
        }

        public enum VideoSyndicatedEnum
        {
            /// <summary>
            /// Return all videos, syndicated or not.
            /// </summary>
            Any = 0,

            /// <summary>
            /// Only retrieve syndicated videos.
            /// </summary>
            True = 1
        }

        public enum VideoTypeEnum
        {
            /// <summary>
            /// Return all videos.
            /// </summary>
            Any = 0,

            /// <summary>
            /// Only retrieve episodes of shows.
            /// </summary>
            Episode = 1,

            /// <summary>
            /// Only retrieve movies.
            /// </summary>
            Movie = 2
        }
    }
}