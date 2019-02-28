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
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniTube.Core.Responses;

namespace UniTube.Core.Requests
{
    public class ChannelListRequest : RequestBase<ChannelListResponse>
    {
        private string part;

        internal ChannelListRequest(string part) => this.part = part;

        public string CategoryId
        {
            get => this.pairs.ContainsKey("categoryId") ?
                this.pairs["categoryId"] : null;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.pairs.Remove("categoryId");
                else
                    this.pairs["categoryId"] = value;
            }
        }

        public override async Task<ChannelListResponse> ExecuteAsync()
        {
            var parameters = JoinPairs(pairs);

            var httpClient = new HttpClient();

            try
            {
                var httpResponse = await httpClient.GetAsync($"https://www.googleapis.com/youtube/v3/channels?{parameters}");
                if (httpResponse.IsSuccessStatusCode)
                {
                    var contentResponse = await httpResponse.Content.ReadAsStringAsync();
                    var channelListResponse = JsonConvert.DeserializeObject<ChannelListResponse>(contentResponse);
                    return channelListResponse;
                }
                else
                    throw new Exception($"Error {httpResponse.StatusCode}\n{httpResponse.ReasonPhrase}\n{await httpResponse.Content.ReadAsStringAsync()}");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Exception while request", ex);
            }
        }
    }
}
