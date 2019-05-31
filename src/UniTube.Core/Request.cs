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
        private Dictionary<string, string> parameters;

        public Request() => this.parameters = new Dictionary<string, string>();

        public string AccessToken
        {
            get => this.GetParameterString("access_token");
            set => this.SetParameter("access_token", value);
        }

        public string Callback
        {
            get => this.GetParameterString("callback");
            set => this.SetParameter("callback", value);
        }

        public string Fields
        {
            get => this.GetParameterString("fields");
            set => this.SetParameter("fields", value);
        }

        public string Key
        {
            get => this.GetParameterString("key");
            set => this.SetParameter("key", value);
        }

        public string PrettyPrint
        {
            get => this.GetParameterString("prettyPrint");
            set => this.SetParameter("prettyPrint", value);
        }

        public string QuotaUser
        {
            get => this.GetParameterString("quotaUser");
            set => this.SetParameter("quotaUser", value);
        }

        public string UserIp
        {
            get => this.GetParameterString("userIp");
            set => this.SetParameter("userIp", value);
        }

        public virtual T Execute()
            => this.ExecuteAsync().GetAwaiter().GetResult();

        public abstract Task<T> ExecuteAsync();

        protected string GetParameterString(string key)
            => this.parameters.ContainsKey(key) ?
                this.parameters[key] : null;

        protected uint? GetParameterUint(string key)
            => this.parameters.ContainsKey(key) ?
                uint.Parse(this.parameters[key]) : (uint?) null;

        protected void SetParameter(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                this.parameters.Remove(key);
            else
                this.parameters[key] = value;
        }

        protected string GetParametersAsString()
        {
            var joined_pairs = new List<string>();

            foreach (var item in this.parameters)
                if (string.IsNullOrWhiteSpace(item.Value))
                    joined_pairs.Add(WebUtility.UrlEncode(item.Key) + "=" +
                        WebUtility.UrlEncode(item.Value));

            return string.Join("&", joined_pairs);
        }
    }
}
