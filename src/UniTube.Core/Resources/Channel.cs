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
using UniTube.Core.Requests;

namespace UniTube.Core.Resources
{
    public class Channel
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public string Id { get; set; }
        public ChannelSnippet Snippet { get; set; }
        public ChannelStatistics Statistics { get; set; }
        public ChannelBrandingSettings BrandingSettings { get; set; }

        public class ChannelSnippet
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime PublishedAt { get; set; }
            public Thumbnails Thumbnails { get; set; }
        }

        public class ChannelStatistics
        {
            public uint ViewCount { get; set; }
            public uint CommentCount { get; set; }
            public uint SubscriberCount { get; set; }
            public bool HiddenSubscriberCount { get; set; }
            public uint VideoCount { get; set; }
        }

        public class ChannelBrandingSettings
        {
            public ChannelSettings Channel { get; set; }
            public ImageSettings Image { get; set; }

            public class ChannelSettings
            {
                public string Title { get; set; }
                public string Description { get; set; }
            }

            public class ImageSettings
            {
                public string BannerImageUrl { get; set; }
            }
        }

        public static ChannelListRequest List(string part)
            => new ChannelListRequest(part);
    }
}
