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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace UniTube.Core.Data
{
    public class Thumbnail
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("width")]
        public uint Width { get; set; }

        [JsonProperty("height")]
        public uint Height { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Thumbnail thumbnail &&
                   Url == thumbnail.Url &&
                   Width == thumbnail.Width &&
                   Height == thumbnail.Height;
        }

        public override int GetHashCode()
        {
            var hashCode = -1388710761;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            return hashCode;
        }
    }
}
