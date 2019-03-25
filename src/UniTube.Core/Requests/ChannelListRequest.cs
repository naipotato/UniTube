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
    public class ChannelListRequest : Request<ChannelListResponse>
    {
        public ChannelListRequest(string part) => this.Part = part;

        public string Part
        {
            get => this.GetPropertyString("part");
            private set => this.SetPropertyString("part", value);
        }

        public string CategoryId
        {
            get => this.GetPropertyString("categoryId");
            set => this.SetPropertyString("categoryId", value);
        }

        public string ForUsername
        {
            get => this.GetPropertyString("forUsername");
            set => this.SetPropertyString("forUsername", value);
        }

        public string Id
        {
            get => this.GetPropertyString("id");
            set => this.SetPropertyString("id", value);
        }

        public bool? ManagedByMe
        {
            get => this.GetPropertyBoolean("managedByMe");
            set => this.SetPropertyBoolean("managedByMe", value);
        }

        public bool? Mine
        {
            get => this.GetPropertyBoolean("mine");
            set => this.SetPropertyBoolean("mine", value);
        }

        public string Hl
        {
            get => this.GetPropertyString("hl");
            set => this.SetPropertyString("hl", value);
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

        public string PageToken
        {
            get => this.GetPropertyString("pageToken");
            set => this.SetPropertyString("pageToken", value);
        }

        public async override Task<ChannelListResponse> ExecuteRequestAsync()
        {
            var parameters = this.GetParameters();
            var httpClient = new HttpClient();

            try
            {
                var responseMessage = await httpClient.GetAsync(
                $"https://www.googleapis.com/youtube.com/v3/channels?{parameters}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var contentResponse = await responseMessage.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ChannelListResponse>(contentResponse);
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
