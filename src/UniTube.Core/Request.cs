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

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UniTube.Core
{
    public abstract class Request<T>
    {
        protected Dictionary<string, string> parameters;

        public string AccessToken
        {
            get => this.parameters.ContainsKey("access_token") ?
                this.parameters["access_token"] : null;
            set => this.SetParameter("access_token", value);
        }

        public string Callback
        {
            get => this.parameters.ContainsKey("callback") ?
                this.parameters["callback"] : null;
            set => this.SetParameter("callback", value);
        }

        public string Fields
        {
            get => this.parameters.ContainsKey("fields") ?
                this.parameters["fields"] : null;
            set => this.SetParameter("fields", value);
        }

        public string Key
        {
            get => this.parameters.ContainsKey("key") ?
                this.parameters["key"] : null;
            set => this.SetParameter("key", value);
        }

        public string PrettyPrint
        {
            get => this.parameters.ContainsKey("prettyPrint") ?
                this.parameters["prettyPrint"] : null;
            set => this.SetParameter("prettyPrint", value);
        }

        public string QuotaUser
        {
            get => this.parameters.ContainsKey("quotaUser") ?
                this.parameters["quotaUser"] : null;
            set => this.SetParameter("quotaUser", value);
        }

        public string UserIp
        {
            get => this.parameters.ContainsKey("userIp") ?
                this.parameters["userIp"] : null;
            set => this.SetParameter("userIp", value);
        }

        protected string Parameters
        {
            get
            {
                var joined_pairs = new List<string>();

                foreach (var item in this.parameters)
                    if (string.IsNullOrWhiteSpace(item.Value))
                        joined_pairs.Add(WebUtility.UrlEncode(item.Key) + "=" +
                            WebUtility.UrlEncode(item.Value));

                return string.Join("&", joined_pairs);
            }
        }

        public abstract VideoListResponse Execute();

        public abstract Task<VideoListResponse> ExecuteAsync();

        protected void SetParameter(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                this.parameters.Remove(key);
            else
                this.parameters[key] = value;
        }
    }
}
