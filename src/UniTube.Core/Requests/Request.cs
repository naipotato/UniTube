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
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace UniTube.Core.Requests
{
    public abstract class Request<T>
    {
        private IDictionary<string, string> parameters;

        public Request() => this.parameters = new Dictionary<string, string>();

		public string AccessToken
		{
			get => this.GetPropertyString("access_token");
			set => this.SetPropertyString("access_token", value);
		}

		public string Callback
		{
			get => this.GetPropertyString("callback");
			set => this.SetPropertyString("callback", value);
		}

		public string Fields
		{
			get => this.GetPropertyString("fields");
			set => this.SetPropertyString("fields", value);
		}

		public string Key
		{
			get => this.GetPropertyString("key");
			set => this.SetPropertyString("key", value);
		}

		public string PrettyPrint
		{
			get => this.GetPropertyString("prettyPrint");
			set => this.SetPropertyString("prettyPrint", value);
		}

		public string QuotaUser
		{
			get => this.GetPropertyString("quotaUser");
			set => this.SetPropertyString("quotaUser", value);
		}

		public string UserIp
		{
			get => this.GetPropertyString("userIp");
			set => this.SetPropertyString("userIp", value);
		}

		protected string GetPropertyString(string property)
		{
			if (this.parameters.ContainsKey(property))
				return this.parameters[property];
			else
				return null;
		}

		protected void SetPropertyString(string property, string value)
		{
			if (!string.IsNullOrWhiteSpace(property))
				this.parameters[property] = value;
			else
				this.parameters.Remove(property);
		}

		protected long GetPropertyLong(string property)
		{
			if (this.parameters.ContainsKey(property))
				return long.Parse(this.parameters[property]);
			else
				return -1;
		}

		protected void SetPropertyLong(string property, long value)
		{
			if (value > -1)
				this.parameters[property] = value.ToString();
			else
				this.parameters.Remove(property);
		}

		protected bool? GetPropertyBoolean(string property)
        {
            if (this.parameters.ContainsKey(property))
				return bool.Parse(this.parameters[property]);
			else
				return null;
        }

        protected void SetPropertyBoolean(string property, bool? value)
        {
            if (value != null)
				this.parameters[property] = value?.ToString();
			else
				this.parameters.Remove(property);
        }

        protected DateTime? GetPropertyDateTime(string property)
        {
            if (this.parameters.ContainsKey(property))
				return DateTime.Parse(this.parameters[property], null,
					DateTimeStyles.RoundtripKind);
			else
				return null;
        }

		protected void SetPropertyDateTime(string property, DateTime? value)
        {
			if (value != null)
				this.parameters[property] = value?.ToString("o");
			else
				this.parameters.Remove(property);
        }

		protected string GetParameters()
		{
			var joinedPairs = new List<string>();

			foreach (var item in this.parameters)
			{
				if (!string.IsNullOrWhiteSpace(item.Value))
					joinedPairs.Add(WebUtility.UrlEncode(item.Key) + "=" +
						WebUtility.UrlEncode(item.Value));
			}

			return string.Join("&", joinedPairs);
		}

		public abstract Task<T> ExecuteRequestAsync();
    }
}
