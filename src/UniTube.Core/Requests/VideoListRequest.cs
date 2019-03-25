// Copyright (C) 2019 Nucleux Software
//
// This file is part of UniTube.
//
// UniTube is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// UniTube is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with UniTube.  If not, see <https://www.gnu.org/licenses/>.
//
// Author: Nahuel Gomez Castro <nahual_gomca@outlook.com.ar>

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniTube.Core.Responses;

namespace UniTube.Core.Requests
{
    public class VideoListRequest : Request<VideoListResponse>
    {
        public VideoListRequest(string part) => this.Part = part;

        public string Part
        {
            get => this.GetPropertyString("part");
            private set => this.SetPropertyString("part", value);
        }

        public string Chart
        {
            get => this.GetPropertyString("chart");
            set => this.SetPropertyString("chart", value);
        }

        public string Id
        {
            get => this.GetPropertyString("id");
            set => this.SetPropertyString("id", value);
        }

        public string MyRating
        {
            get => this.GetPropertyString("myRating");
            set => this.SetPropertyString("myRating", value);
        }

        public string Hl
        {
            get => this.GetPropertyString("hl");
            set => this.SetPropertyString("hl", value);
        }

        public long MaxHeight
        {
            get => this.GetPropertyLong("maxHeight");
            set => this.SetPropertyLong("maxHeight", value);
        }

        public long MaxResults
        {
            get => this.GetPropertyLong("maxResults");
            set => this.SetPropertyLong("maxResults", value);
        }

        public long MaxWidth
        {
            get => this.GetPropertyLong("maxWidth");
            set => this.SetPropertyLong("maxWidth", value);
        }

        public string OnBehalfOfContentOwner
        {
            get => this.GetPropertyString("onBehalfOfContentOwner");
            set => this.SetPropertyString("onBehalfOfContentOwner", value);
        }

        public string PageToken
        {
            get => this.GetPropertyString("pageToken");
            set => this.SetPropertyString("pageToken", value);
        }

        public string RegionCode
        {
            get => this.GetPropertyString("regionCode");
            set => this.SetPropertyString("regionCode", value);
        }

        public string VideoCategoryId
        {
            get => this.GetPropertyString("videoCategoryId");
            set => this.SetPropertyString("videoCategoryId", value);
        }

        public override async Task<VideoListResponse> ExecuteRequestAsync()
        {
            var parameters = this.GetParameters();
            var httpClient = new HttpClient();

            try
            {
                var responseMessage = await httpClient.GetAsync(
                $"https://www.googleapis.com/youtube.com/v3/videos?{parameters}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var contentResponse = await responseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<VideoListResponse>(contentResponse);
                }
                else
                    throw new Exception(
                        $"Error {responseMessage.StatusCode}\n{responseMessage.ReasonPhrase}\n{await responseMessage.Content.ReadAsStringAsync()}");
            }
			catch (Exception ex)
			{
				throw new Exception("Exception while request", ex);
			}
        }
    }
}
