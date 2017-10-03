using System.Collections.Generic;
using UniTube.Core.Resources;

namespace UniTube.Core.Responses
{
    public class ChannelListResponse
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string NextPageToken { get; set; }
        public string PrevPageToken { get; set; }
        public List<Channel> Items { get; set; }
    }
}
