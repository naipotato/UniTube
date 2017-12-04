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
    public class SearchSource : IIncrementalSource<ISearchResult>
    {
        private string _nextPageToken;
        private string _query;
        private int _totalResults = 1;
        private readonly List<ISearchResult> _search;
        private Action _startFirstLoadAction;
        private Action _endFirstLoadAction;

        public SearchSource(string query, Action startFirstLoadAction = null, Action endFirstLoadAction = null)
        {
            _query = query;
            _search = new List<ISearchResult>();
            _startFirstLoadAction = startFirstLoadAction;
            _endFirstLoadAction = endFirstLoadAction;
        }

        public async Task<IEnumerable<ISearchResult>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_search.Count < _totalResults && _search.Count < ((pageIndex + 1) * pageSize))
            {
                if (_search.Count == 0)
                {
                    _startFirstLoadAction?.Invoke();
                }

                await PopulateSearchList(_nextPageToken);

                if (_search.Count == 50)
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
                searchListRequest.Fields = "nextPageToken,pageInfo/totalResults,items(id(kind,videoId,channelId,playlistId),snippet(title,channelTitle,channelId,publishedAt,description,thumbnails/high/url))";
                searchListRequest.Type = SearchListRequest.TypeEnum.Video;

                return await searchListRequest.ExecuteAsync();
            });

            foreach (var searchResult in response.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        var video = new Video
                        {
                            Id = searchResult.Id.VideoId,
                            Kind = searchResult.Id.Kind,
                            Snippet = new Video.VideoSnippet
                            {
                                ChannelId = searchResult.Snippet.ChannelId,
                                PublishedAt = searchResult.Snippet.PublishedAt,
                                ChannelTitle = searchResult.Snippet.ChannelTitle,
                                Title = searchResult.Snippet.Title,
                                Thumbnails = searchResult.Snippet.Thumbnails,
                                Description = searchResult.Snippet.Description
                            }
                        };
                        _search.Add(video);
                        break;
                    case "youtube#channel":
                        var channel = new Channel
                        {
                            Id = searchResult.Id.ChannelId,
                            Kind = searchResult.Id.Kind,
                            Snippet = new Channel.ChannelSnippet
                            {
                                PublishedAt = searchResult.Snippet.PublishedAt,
                                Thumbnails = searchResult.Snippet.Thumbnails,
                                Title = searchResult.Snippet.Title,
                                Description = searchResult.Snippet.Description
                            }
                        };
                        _search.Add(channel);
                        break;
                    case "youtube#playlist":
                        var playlist = new Playlist
                        {
                            Id = searchResult.Id.PlaylistId,
                            Kind = searchResult.Id.Kind,
                            Snippet = new Playlist.PlaylistSnippet
                            {
                                PublishedAt = searchResult.Snippet.PublishedAt,
                                ChannelTitle = searchResult.Snippet.ChannelTitle,
                                Thumbnails = searchResult.Snippet.Thumbnails,
                                Title = searchResult.Snippet.Title,
                                Description = searchResult.Snippet.Description,
                                ChannelId = searchResult.Snippet.ChannelId
                            }
                        };
                        _search.Add(playlist);
                        break;
                }

            }

            _nextPageToken = response.NextPageToken;
            _totalResults = response.PageInfo.TotalResults;
        }
    }
}
