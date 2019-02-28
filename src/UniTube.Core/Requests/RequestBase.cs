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

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UniTube.Core.Requests
{
    public abstract class RequestBase<T>
    {
        protected Dictionary<string, string> pairs;

        protected RequestBase()
            => this.pairs = new Dictionary<string, string>();

        public string AccessToken
        {
            get => this.pairs.ContainsKey("access_token") ?
                this.pairs["access_token"] : null;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.pairs.Remove("access_token");
                else
                    this.pairs["access_token"] = value;
            }
        }

        public string Callback
        {
            get => this.pairs.ContainsKey("callback") ?
                this.pairs["callback"] : null;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.pairs.Remove("callback");
                else
                    this.pairs["callback"] = value;
            }
        }

        public string Fields
        {
            get => this.pairs.ContainsKey("fields") ?
                this.pairs["fields"] : null;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.pairs.Remove("fields");
                else
                    this.pairs["fields"] = value;
            }
        }

        public string Key
        {
            get => this.pairs.ContainsKey("key") ?
                this.pairs["key"] : null;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.pairs.Remove("key");
                else
                    this.pairs["key"] = value;
            }
        }

        public string PrettyPrint
        {
            get => this.pairs.ContainsKey("prettyPrint") ?
                this.pairs["prettyPrint"] : null;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.pairs.Remove("prettyPrint");
                else
                    this.pairs["prettyPrint"] = value;
            }
        }

        public string QuotaUser
        {
            get => this.pairs.ContainsKey("quotaUser") ?
                this.pairs["quotaUser"] : null;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.pairs.Remove("quotaUser");
                else
                    this.pairs["quotaUser"] = value;
            }
        }

        public string UserIp
        {
            get => this.pairs.ContainsKey("userIp") ?
                this.pairs["userIp"] : null;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    this.pairs.Remove("userIp");
                else
                    this.pairs["userIp"] = value;
            }
        }

        public abstract Task<T> ExecuteAsync();

        protected string JoinPairs(Dictionary<string, string> pairs)
        {
            var joinedPairs = new List<string>();

            foreach (var pair in pairs)
            {
                if (!string.IsNullOrEmpty(pair.Value))
                    joinedPairs.Add(WebUtility.UrlEncode(pair.Key) + "=" +
                        WebUtility.UrlEncode(pair.Value));
            }

            return string.Join("&", joinedPairs);
        }
    }
}
