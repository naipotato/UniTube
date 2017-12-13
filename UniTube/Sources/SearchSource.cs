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
    public class SearchSource : IIncrementalSource<SearchResult>
    {
        private bool _hasBeenLoaded;
        private string _nextPageToken;
        private string _query;
        private int _totalResults = 1;
        private readonly List<SearchResult> _search;
        private Action _startFirstLoadAction;
        private Action _endFirstLoadAction;

        public SearchSource(string query, Action startFirstLoadAction = null, Action endFirstLoadAction = null)
        {
            _query = query;
            _search = new List<SearchResult>();
            _startFirstLoadAction = startFirstLoadAction;
            _endFirstLoadAction = endFirstLoadAction;
        }

        public async Task<IEnumerable<SearchResult>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_search.Count < _totalResults && _search.Count < ((pageIndex + 1) * pageSize))
            {
                if (!_hasBeenLoaded)
                {
                    _startFirstLoadAction?.Invoke();
                }

                await PopulateSearchList(_nextPageToken);

                if (_hasBeenLoaded)
                {
                    _endFirstLoadAction?.Invoke();
                }
            }

            var result = (from v in _search
                          select v).Skip(pageIndex * pageSize).Take(pageSize);

            return result;
        }

        private async Task PopulateSearchList(string nextPageToken = null)
        {
            var response = await Task.Run(async () =>
            {
                var searchListRequest = Search.List("snippet");
                searchListRequest.MaxResults = 50;
                searchListRequest.Order = SearchListRequest.OrderEnum.Relevance;
                searchListRequest.Q = _query;
                searchListRequest.RegionCode = GlobalizationPreferences.HomeGeographicRegion;
                searchListRequest.PageToken = nextPageToken;
                searchListRequest.RelevanceLangugage = GlobalizationPreferences.Languages[0];
                searchListRequest.Key = Client.ApiKey;
                searchListRequest.Fields = "nextPageToken,pageInfo/totalResults,items(id/videoId,snippet(title,channelTitle,thumbnails/high/url))";

                return await searchListRequest.ExecuteAsync();
            });

            _search.AddRange(response.Items);
            _nextPageToken = response.NextPageToken;
            _totalResults = response.PageInfo.TotalResults;
            _hasBeenLoaded = true;
        }
    }
}
