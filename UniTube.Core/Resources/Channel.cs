using System;
using UniTube.Core.Requests;

namespace UniTube.Core.Resources
{
    public class Channel : ISearchResult
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string Id { get; set; }
        public ChannelSnippet Snippet { get; set; }
        public ChannelStatistics Statistics { get; set; }
        public ChannelBrandingSettings BrandingSettings { get; set; }

        public class ChannelSnippet
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime PublishedAt { get; set; }
            public Thumbnails Thumbnails { get; set; }
        }

        public class ChannelStatistics
        {
            public uint ViewCount { get; set; }
            public uint CommentCount { get; set; }
            public uint SubscriberCount { get; set; }
            public bool HiddenSubscriberCount { get; set; }
            public uint VideoCount { get; set; }
        }

        public class ChannelBrandingSettings
        {
            public ChannelSettings Channel { get; set; }
            public ImageSettings Image { get; set; }

            public class ChannelSettings
            {
                public string Title { get; set; }
                public string Description { get; set; }
            }

            public class ImageSettings
            {
                public string BannerImageUrl { get; set; }
            }
        }

        /// <summary>
        /// Returns a collection of zero or more <see cref="Channel"/> resources that match the request criteria.
        /// </summary>
        /// <param name="part">Specifies a comma-separated list of one or more <see cref="Channel"/> resource
        /// properties that the API response will include.</param>
        public static ChannelListRequest List(string part) => new ChannelListRequest(part);
    }
}
