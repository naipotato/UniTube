using System;
using System.Collections.Generic;

namespace UniTube.Core.Resources
{
    public class Playlist : ISearchResult
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string Id { get; set; }
        public PlaylistSnippet Snippet { get; set; }

        public class PlaylistSnippet
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
