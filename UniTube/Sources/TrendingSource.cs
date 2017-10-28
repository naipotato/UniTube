using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using UniTube.Collections;
using UniTube.Core;
using UniTube.Core.Requests;
using UniTube.Core.Resources;

using Windows.System.UserProfile;

namespace UniTube.Sources
{
    public class TrendingSource : IIncrementalSource<Video>
    {
        private string _nextPageToken;
        private int _totalResults = 1;
        private readonly List<Video> _trending;
        private Action _startFirstLoadAction;
        private Action _endFirstLoadAction;

        public TrendingSource(Action startFirstLoadAction = null, Action endFirstLoadAction = null)
        {
            _trending = new List<Video>();
            _startFirstLoadAction = startFirstLoadAction;
            _endFirstLoadAction = endFirstLoadAction;
        }

        public async Task<IEnumerable<Video>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_trending.Count < _totalResults && _trending.Count < ((pageIndex + 1) * pageSize))
            {
                if (_trending.Count == 0)
                {
                    _startFirstLoadAction?.Invoke();
                }

                await PopulateTrendingList(_nextPageToken);

                if (_trending.Count == 50)
                {
                    _endFirstLoadAction?.Invoke();
                }
            }

            var result = (from v in _trending
                          select v).Skip(pageIndex * pageSize).Take(pageSize);

            return result;
        }

        private async Task PopulateTrendingList(string nextPageToken = null)
        {
            var response = await Task.Run(async () =>
            {
                var videoListRequest = Video.List("id,snippet");
                videoListRequest.Chart = VideoListRequest.ChartEnum.MostPopular;
                videoListRequest.MaxResults = 50;
                videoListRequest.RegionCode = GlobalizationPreferences.HomeGeographicRegion;
                videoListRequest.PageToken = nextPageToken;
                videoListRequest.Hl = GlobalizationPreferences.Languages[0];
                videoListRequest.Key = Client.ApiKey;
                videoListRequest.Fields = "nextPageToken,pageInfo/totalResults,items(id,snippet(title,channelTitle,thumbnails/high/url))";

                return await videoListRequest.ExecuteAsync();
            });

            _trending.AddRange(response.Items);
            _nextPageToken = response.NextPageToken;
            _totalResults = response.PageInfo.TotalResults;
        }
    }
}
