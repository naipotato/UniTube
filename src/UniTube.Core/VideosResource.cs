/* UniTube - An open source client for YouTube
 * Copyright (C) 2019 Nucleux Software
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY of FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 *
 * Author: Nahuel Gomez Castro <nahual_gomca@outlook.com.ar>
 */

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UniTube.Core
{
    public class VideosResource
    {
        private YouTubeService service;

        public VideosResource(YouTubeService service)
        {
            this.service = service;
        }

        public ListRequest List(string part)
        {
            return new ListRequest(part);
        }

        public class ListRequest : Request<VideoListResponse>
        {
            public ListRequest(string part)
            {
                this.Part = part;
            }

            public string Part
            {
                get => base.GetPropertyString("part");
                private set => base.SetProperty("part", value);
            }

            public string Chart
            {
                get => base.GetPropertyString("chart");
                set => base.SetProperty("chart", value);
            }

            public string Id
            {
                get => base.GetPropertyString("id");
                set => base.SetProperty("id", value);
            }

            public string MyRating
            {
                get => base.GetPropertyString("myRating");
                set => base.SetProperty("myRating", value);
            }

            public string Hl
            {
                get => base.GetPropertyString("hl");
                set => base.SetProperty("hl", value);
            }

            public long MaxHeight
            {
                get => base.GetPropertyLong("maxHeight");
                set => base.SetProperty("maxHeight", value);
            }

            public long MaxResults
            {
                get => base.GetPropertyLong("maxResults");
                set => base.SetProperty("maxResults", value);
            }

            public long MaxWidth
            {
                get => base.GetPropertyLong("maxWidth");
                set => base.SetProperty("maxWidth", value);
            }

            public string OnBehalfOfContentOwner
            {
                get => base.GetPropertyString("onBehalfOfContentOwner");
                set => base.SetProperty("onBehalfOfContentOwner", value);
            }

            public string PageToken
            {
                get => base.GetPropertyString("pageToken");
                set => base.SetProperty("pageToken", value);
            }

            public string RegionCode
            {
                get => base.GetPropertyString("regionCode");
                set => base.SetProperty("regionCode", value);
            }

            public string VideoCategoryId
            {
                get => base.GetPropertyString("videoCategoryId");
                set => base.SetProperty("videoCategoryId", value);
            }

            public override VideoListResponse Execute()
            {
                return this.ExecuteAsync().GetAwaiter().GetResult();
            }

            public override async Task<VideoListResponse> ExecuteAsync()
            {
                var client = new HttpClient();
                var message = new HttpRequestMessage(HttpMethod.Get,
                    $"https://www.googleapis.com/youtube/v3/videos?{base.Parameters}");

                var response = await client.SendAsync(message);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    var videoListResponse = await Task.Factory.StartNew(() =>
                        JsonConvert.DeserializeObject<VideoListResponse>(contentResponse));
                    return videoListResponse;
                }
                else
                {
                    throw new Exception(
                        $"Error {response.StatusCode}\n{response.ReasonPhrase}\n" +
                        $"{await response.Content.ReadAsStringAsync()}");
                }
            }
        }
    }
}
