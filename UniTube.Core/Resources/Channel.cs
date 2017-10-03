using System;

namespace UniTube.Core.Resources
{
    public class Channel
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
            public ChannelThumbnails Thumbnails { get; set; }

            public class ChannelThumbnails
            {
                public Thumbnail Default { get; set; }
                public Thumbnail Medium { get; set; }
                public Thumbnail High { get; set; }
            }
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
    }
}
