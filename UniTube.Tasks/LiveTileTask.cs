using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using UniTube.Core;
using UniTube.Core.Requests;
using UniTube.Core.Resources;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.System.UserProfile;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;

namespace UniTube.Tasks
{
    public sealed class LiveTileTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var entry = (await Package.Current.GetAppListEntriesAsync())[0];

            var isSupported = StartScreenManager.GetDefault().SupportsAppListEntry(entry);
            var isPinned = await StartScreenManager.GetDefault().ContainsAppListEntryAsync(entry);

            if (isSupported && isPinned)
            {
                var trending = await GetTrendingList();

                if (trending.Count == 5)
                {
                    UpdateTile(trending);
                }
            }

            deferral.Complete();
        }

        private static async Task<List<TileVideoUpdate>> GetTrendingList()
        {
            var trending = new List<TileVideoUpdate>();

            try
            {
                var videoListRequest = Video.List("snippet");
                videoListRequest.Chart = VideoListRequest.ChartEnum.MostPopular;
                videoListRequest.MaxResults = 5;
                videoListRequest.RegionCode = GlobalizationPreferences.HomeGeographicRegion;
                videoListRequest.Key = Client.ApiKey;
                videoListRequest.Fields = "items/snippet(title,channelTitle,channelId,thumbnails/high/url)";

                try
                {
                    var videoListResponse = await videoListRequest.ExecuteAsync();

                    foreach (var item in videoListResponse.Items)
                    {
                        var tileVideoUpdate = new TileVideoUpdate
                        {
                            ChannelTitle = item.Snippet.ChannelTitle,
                            ThumbnailUrl = item.Snippet.Thumbnails.High.Url,
                            Title = item.Snippet.Title
                        };

                        var channelListRequest = Channel.List("snippet");
                        channelListRequest.Id = item.Snippet.ChannelId;
                        channelListRequest.Fields = "items/snippet/thumbnails/high/url";
                        channelListRequest.Key = Client.ApiKey;

                        try
                        {
                            var channelListResponse = await channelListRequest.ExecuteAsync();

                            tileVideoUpdate.ProfilePicUrl = channelListResponse.Items[0].Snippet.Thumbnails.High.Url;

                            trending.Add(tileVideoUpdate);
                        }
                        catch { }
                    }
                }
                catch { }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return trending;
        }

        private static void UpdateTile(List<TileVideoUpdate> trending)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            foreach (var item in trending)
            {
                var xml = $@"
                  <tile>
                    <visual branding='nameAndLogo'>

                      <binding template='TileMedium' hint-textStacking='center'>
                        <image src='{item.ThumbnailUrl}' placement='peek'/>
                        <text hint-style='base' hint-align='center' hint-wrap='true' hint-maxLines='2'>{item.Title}</text>
                        <text hint-style='captionSubtle' hint-align='center'>{item.ChannelTitle}</text>
                      </binding>

                      <binding template='TileWide' hint-textStacking='center'>
                        <image src='{item.ThumbnailUrl}' placement='peek'/>
                        <group>
                          <subgroup hint-weight='33'>
                            <image src='{item.ProfilePicUrl}' hint-crop='circle'/>
                          </subgroup>
                          <subgroup>
                            <text hint-style='base' hint-wrap='true' hint-maxLines='2'>{item.Title}</text>
                            <text hint-style='captionSubtle'>{item.ChannelTitle}</text>
                          </subgroup>
                        </group>
                      </binding>

                      <binding template='TileLarge' hint-textStacking='center'>
                        <image src='{item.ThumbnailUrl}' placement='peek'/>
                        <group>
                          <subgroup hint-weight='1'/>
                          <subgroup hint-weight='2'>
                            <image src='{item.ProfilePicUrl}' hint-crop='circle'/>
                          </subgroup>
                          <subgroup hint-weight='1'/>
                        </group>
                        <text hint-style='base' hint-align='center' hint-wrap='true' hint-maxLines='2'>{item.Title}</text>
                        <text hint-style='captionSubtle' hint-align='center'>{item.ChannelTitle}</text>
                      </binding>

                    </visual>
                  </tile>";

                var document = new XmlDocument();
                document.LoadXml(xml);

                try
                {
                    updater.Update(new TileNotification(document));
                }
                catch { }
            }
        }
    }
}
