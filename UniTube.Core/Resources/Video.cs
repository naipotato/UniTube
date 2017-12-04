using System;
using System.Collections.Generic;
using UniTube.Core.Requests;

namespace UniTube.Core.Resources
{
    public class Video : ISearchResult
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
            public string CategoryId { get; set; }
        }

        /// <summary>
        /// Returns a list of videos that match the API request parameters.
        /// </summary>
        /// <param name="part">Specifies a comma-separated list of one or more <see cref="Video"/> resource
        /// properties that the API response will include.</param>
        public static VideoListRequest List(string part) => new VideoListRequest(part);
    }
}
