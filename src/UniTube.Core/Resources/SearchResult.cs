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

namespace UniTube.Core.Resources
{
    public class SearchResult
    {
        public string Kind { get; set; }
        public string Etag { get; set; }
        public SearchResultId Id { get; set; }
        public SearchResultSnippet Snippet { get; set; }

        public class SearchResultId
        {
            public string Kind { get; set; }
            public string VideoId { get; set; }
            public string ChannelId { get; set; }
            public string PlaylistId { get; set; }
        }

        public class SearchResultSnippet
        {
            public DateTime PublishedAt { get; set; }
            public string ChannelId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Thumbnails Thumbnails { get; set; }
            public string ChannelTitle { get; set; }
        }
    }
}
