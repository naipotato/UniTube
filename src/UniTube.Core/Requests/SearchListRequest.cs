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
    public class SearchListRequest : Request<SearchListResponse>
    {
        public SearchListRequest(string part) => this.Part = part;

		public string Part
		{
			get => this.GetPropertyString("part");
			private set => this.SetPropertyString("part", value);
		}

		public bool? ForContentOwner
		{
			get => this.GetPropertyBoolean("forContentOwner");
			set => this.SetPropertyBoolean("forContentOwner", value);
		}

		public bool? ForMine
		{
			get => this.GetPropertyBoolean("forMine");
			set => this.SetPropertyBoolean("forMine", value);
		}

		public string RelevantToVideoId
		{
			get => this.GetPropertyString("relevantToVideoId");
			set => this.SetPropertyString("relevantToVideoId", value);
		}

		public string ChannelId
		{
			get => this.GetPropertyString("channelId");
			set => this.SetPropertyString("channelId", value);
		}

		public string ChannelType
		{
			get => this.GetPropertyString("channelType");
			set => this.SetPropertyString("channelType", value);
		}

		public string EventType
		{
			get => this.GetPropertyString("eventType");
			set => this.SetPropertyString("eventType", value);
		}

		public long MaxResults
		{
			get => this.GetPropertyLong("maxResults");
			set => this.SetPropertyLong("maxResults", value);
		}

		public string OnBehalfOfContentOwner
		{
			get => this.GetPropertyString("onBehalfOfContentOwner");
			set => this.SetPropertyString("onBehalfOfContentOwner", value);
		}

		public string Order
		{
			get => this.GetPropertyString("order");
			set => this.SetPropertyString("order", value);
		}

		public string PageToken
		{
			get => this.GetPropertyString("pageToken");
			set => this.SetPropertyString("pageToken", value);
		}

		public DateTime? PublishedAfter
		{
			get => this.GetPropertyDateTime("publishedAfter");
			set => this.SetPropertyDateTime("publishedAfter", value);
		}

		public DateTime? PublishedBefore
		{
			get => this.GetPropertyDateTime("publishedBefore");
			set => this.SetPropertyDateTime("publishedBefore", value);
		}

		public string Q
		{
			get => this.GetPropertyString("q");
			set => this.SetPropertyString("q", value);
		}

		public string RegionCode
		{
			get => this.GetPropertyString("regionCode");
			set => this.SetPropertyString("regionCode", value);
		}

		public string SafeSearch
		{
			get => this.GetPropertyString("safeSearch");
			set => this.SetPropertyString("safeSearch", value);
		}

		public string TopicId
		{
			get => this.GetPropertyString("topicId");
			set => this.SetPropertyString("topicId", value);
		}

		public string Type
		{
			get => this.GetPropertyString("type");
			set => this.SetPropertyString("type", value);
		}

		public string VideoCaption
		{
			get => this.GetPropertyString("videoCaption");
			set => this.SetPropertyString("videoCaption", value);
		}

		public string VideoCategoryId
		{
			get => this.GetPropertyString("videoCategoryId");
			set => this.SetPropertyString("videoCategoryId", value);
		}

		public string VideoDefinition
		{
			get => this.GetPropertyString("videoDefinition");
			set => this.SetPropertyString("videoDefinition", value);
		}

		public string VideoDimension
		{
			get => this.GetPropertyString("videoDimension");
			set => this.SetPropertyString("videoDimension", value);
		}

		public string VideoDuration
		{
			get => this.GetPropertyString("videoDuration");
			set => this.SetPropertyString("videoDuration", value);
		}

		public string VideoEmbeddable
		{
			get => this.GetPropertyString("videoEmbeddable");
			set => this.SetPropertyString("videoEmbeddable", value);
		}

		public string VideoLicense
		{
			get => this.GetPropertyString("videoLicense");
			set => this.SetPropertyString("videoLicense", value);
		}

		public string VideoSyndicated
		{
			get => this.GetPropertyString("videoSyndicated");
			set => this.SetPropertyString("videoSyndicated", value);
		}

		public string VideoType
		{
			get => this.GetPropertyString("videoType");
			set => this.SetPropertyString("videoType", value);
		}

        public async override Task<SearchListResponse> ExecuteRequestAsync()
        {
            var parameters = this.GetParameters();
            var httpClient = new HttpClient();

            try
            {
                var responseMessage = await httpClient.GetAsync(
                $"https://www.googleapis.com/youtube.com/v3/search?{parameters}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var contentResponse = await responseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SearchListResponse>(contentResponse);
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
