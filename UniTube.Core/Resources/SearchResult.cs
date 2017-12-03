using System;

namespace UniTube.Core.Resources
{
    public class SearchResult
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public SearchResultId Id { get; set; }
        public SearchResultSnippet Snippet { get; set; }

        public class SearchResultId
        {
            public string Kind { get; set; }
            public string VideoId { get; set; }
            public string ChannelId { get; set; }
            public string PlaylistId { get; set; }
        }

        public class SearchResultSnippet
        {
            public DateTime PublishedAt { get; set; }
            public string ChannelId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Thumbnails Thumbnails { get; set; }
            public string ChannelTitle { get; set; }
        }
    }
}
