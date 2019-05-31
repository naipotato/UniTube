// UniTube - An open source client for YouTube
// Copyright (C) 2019 Nucleux Software
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Author: Nahuel Gomez Castro <nahual_gomca@outlook.com.ar>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniTube.Core.Data;

namespace UniTube.Core
{
    public class VideosResource
    {
        private string apiKey;

        public VideosResource(string apiKey) => this.apiKey = apiKey;

        public ListRequest List(string part, string apiKey)
            => new ListRequest(part, apiKey);

        public class ListRequest : Request<ListResponse<Video>>
        {
            public ListRequest(string part, string apiKey) : base()
            {
                base.Key = apiKey;
                this.Part = part;
            }

            public string Part
            {
                get => base.GetParameterString("part");
                set => base.SetParameter("part", value);
            }

            public string Chart
            {
                get => base.GetParameterString("chart");
                set => base.SetParameter("chart", value);
            }

            public string Id
            {
                get => base.GetParameterString("id");
                set => base.SetParameter("id", value);
            }

            public string MyRating
            {
                get => base.GetParameterString("myRating");
                set => base.SetParameter("myRating", value);
            }

            public string Hl
            {
                get => base.GetParameterString("hl");
                set => base.SetParameter("hl", value);
            }

            public uint? MaxHeight
            {
                get => base.GetParameterUint("maxHeight");
                set => base.SetParameter("maxHeight", value.ToString());
            }

            public uint? MaxResults
            {
                get => base.GetParameterUint("maxResults");
                set => base.SetParameter("maxResults", value.ToString());
            }

            public uint? MaxWidth
            {
                get => base.GetParameterUint("maxWidth");
                set => base.SetParameter("maxWidth", value.ToString());
            }

            public string OnBehalfOfContentOwner
            {
                get => base.GetParameterString("onBehalfOfContentOwner");
                set => base.SetParameter("onBehalfOfContentOwner", value);
            }

            public string PageToken
            {
                get => base.GetParameterString("pageToken");
                set => base.SetParameter("pageToken", value);
            }

            public string RegionCode
            {
                get => base.GetParameterString("regionCode");
                set => base.SetParameter("regionCode", value);
            }

            public string VideoCategoryId
            {
                get => base.GetParameterString("videoCategoryId");
                set => base.SetParameter("videoCategoryId", value);
            }

            public override ListResponse<Video> Execute()
                => ExecuteAsync().GetAwaiter().GetResult();

            public override async Task<ListResponse<Video>> ExecuteAsync()
            {
                // First of all, we need the HttpClient
                var client = new HttpClient();

                // Then, we need to set up the message.
                var url = $"https://www.googleapis.com/youtube/v3/videos?{base.GetParametersAsString()}";
                var message = new HttpRequestMessage(HttpMethod.Get, url);

                // Send msg to the server and wait for the response
                var response = await client.SendAsync(message);

                // Retrieve the data from the response
                var contentData = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // If the status code is 200, then return the
                    // deserialized data
                    return ListResponse<Video>.FromJson(contentData);
                }
                else
                {
                    // If not, then throw an ApiException
                    throw ApiException.FromJson(contentData);
                }
            }
        }
    }
}
