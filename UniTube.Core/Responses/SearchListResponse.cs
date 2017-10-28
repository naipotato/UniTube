using System.Collections.Generic;

using UniTube.Core.Resources;

namespace UniTube.Core.Responses
{
    public class SearchListResponse
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string NextPageToken { get; set; }
        public string PrevPageToken { get; set; }
        public SearchListPageInfo PageInfo { get; set; }
        public List<SearchResult> Items { get; set; }

        public class SearchListPageInfo
        {
            public int TotalResults { get; set; }
            public int ResultPerPage { get; set; }
        }
    }
}