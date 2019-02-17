using System;
using System.Collections.Generic;

namespace UniTube.Core.Resources
{
    public class Video
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string Id { get; set; }
        public VideoSnippet Snippet { get; set; }

        public class VideoSnippet
        {
            public DateTime PublishedAt { get; set; }
            public string ChannelId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Thumbnails Thumbnails { get; set; }
            public string ChannelTitle { get; set; }
            public List<string> Tags { get; set; }
        }
    }
}
