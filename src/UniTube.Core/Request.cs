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
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UniTube.Core
{
    public abstract class Request<T>
    {
        private Dictionary<string, string> parameters;

        protected Request()
        {
            this.parameters = new Dictionary<string, string>();
        }

        public string AccessToken
        {
            get => this.GetPropertyString("access_token");
            set => this.SetProperty("access_token", value);
        }

        public string Callback
        {
            get => this.GetPropertyString("callback");
            set => this.SetProperty("callback", value);
        }

        public string Fields
        {
            get => this.GetPropertyString("fields");
            set => this.SetProperty("fields", value);
        }

        public string Key
        {
            get => this.GetPropertyString("key");
            set => this.SetProperty("key", value);
        }

        public string PrettyPrint
        {
            get => this.GetPropertyString("prettyPrint");
            set => this.SetProperty("prettyPrint", value);
        }

        protected string Parameters
        {
            get
            {
                var joined_pairs = new List<string>();

                foreach (var item in this.parameters)
                {
                    if (string.IsNullOrWhiteSpace(item.Value))
                    {
                        joined_pairs.Add(WebUtility.UrlEncode(item.Key) + "=" +
                            WebUtility.UrlEncode(item.Value));
                    }
                }

                return string.Join("&", joined_pairs);
            }
        }

        public string QuotaUser
        {
            get => this.GetPropertyString("quotaUser");
            set => this.SetProperty("quotaUser", value);
        }

        public string UserIp
        {
            get => this.GetPropertyString("userIp");
            set => this.SetProperty("userIp", value);
        }

        protected string GetPropertyString(string property)
        {
            return this.parameters.ContainsKey(property) ?
                this.parameters[property] : null;
        }

        protected long GetPropertyLong(string property)
        {
            return this.parameters.ContainsKey(property) ?
                long.Parse(this.parameters[property]) : -1;
        }

        protected void SetProperty(string property, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                this.parameters[property] = value;
            }
            else
            {
                this.parameters.Remove(property);
            }
        }

        protected void SetProperty(string property, long value)
        {
            if (value > -1)
            {
                this.parameters[property] = value.ToString();
            }
            else
            {
                this.parameters.Remove(property);
            }
        }

        public abstract T Execute();

        public abstract Task<T> ExecuteAsync();
    }
}
