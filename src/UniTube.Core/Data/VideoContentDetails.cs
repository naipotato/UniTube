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

namespace UniTube.Core.Data
{
    public class VideoContentDetails
    {
        public string Duration { get; set; }

        public string Dimension { get; set; }

        public string Definition { get; set; }

        public string Caption { get; set; }

        public bool LicensedContent { get; set; }

        public RegionRestriction RegionRestriction { get; set; }

        public ContentRating ContentRating { get; set; }

        public string Projection { get; set; }

        public bool HasCustomThumbnail { get; set; }
    }
}
