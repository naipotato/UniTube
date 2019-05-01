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
using Newtonsoft.Json;

namespace UniTube.Core.Data
{
    public class VideoSnippet
    {
        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("thumbnails")]
        public Dictionary<string, Thumbnail> Thumbnails { get; set; }

        [JsonProperty("channelTitle")]
        public string ChannelTitle { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is VideoSnippet snippet &&
                   PublishedAt == snippet.PublishedAt &&
                   ChannelId == snippet.ChannelId &&
                   Title == snippet.Title &&
                   Description == snippet.Description &&
                   EqualityComparer<Dictionary<string, Thumbnail>>.Default.Equals(Thumbnails, snippet.Thumbnails) &&
                   ChannelTitle == snippet.ChannelTitle &&
                   EqualityComparer<List<string>>.Default.Equals(Tags, snippet.Tags) &&
                   CategoryId == snippet.CategoryId;
        }

        public override int GetHashCode()
        {
            var hashCode = 1952003337;
            hashCode = hashCode * -1521134295 + PublishedAt.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ChannelId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<string, Thumbnail>>.Default.GetHashCode(Thumbnails);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ChannelTitle);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(Tags);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CategoryId);
            return hashCode;
        }
    }
}
