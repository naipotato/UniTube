using System.Collections.Generic;

using UniTube.Core.Resources;

namespace UniTube.Core.Responses
{
    public class VideoListResponse
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string NextPageToken { get; set; }
        public string PrevPageToken { get; set; }
        public VideoListPageInfo PageInfo { get; set; }
        public List<Video> Items { get; set; }

        public class VideoListPageInfo
        {
            public int TotalResults { get; set; }
            public int ResultsPerPage { get; set; }
        }
    }
}
