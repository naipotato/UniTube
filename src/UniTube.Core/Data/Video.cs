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
    public class Video
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("etag")]
        public string Etag { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("snippet")]
        public VideoSnippet Snippet { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Video video &&
                   Kind == video.Kind &&
                   Etag == video.Etag &&
                   Id == video.Id &&
                   EqualityComparer<VideoSnippet>.Default.Equals(Snippet, video.Snippet);
        }

        public override int GetHashCode()
        {
            var hashCode = 1034369001;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Kind);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Etag);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<VideoSnippet>.Default.GetHashCode(Snippet);
            return hashCode;
        }
    }
}
