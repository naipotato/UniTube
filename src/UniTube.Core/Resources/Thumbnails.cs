/* 
 * Copyright (C) 2019 Nucleux Software
 * 
 * This file is part of UniTube.
 * 
 * UniTube is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * UniTube is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with UniTube.  If not, see <https://www.gnu.org/licenses/>.
 * 
 * Author: Nahuel Gomez Castro <nahual_gomca@outlook.com.ar>
 */

namespace UniTube.Core.Resources
{
    public class Thumbnails
    {
        public Thumbnail Default { get; set; }
        public Thumbnail Medium { get; set; }
        public Thumbnail High { get; set; }
        public Thumbnail Standard { get; set; }
        public Thumbnail MaxRes { get; set; }

        public class Thumbnail
        {
            public string Url { get; set; }
            public uint Width { get; set; }
            public uint Height { get; set; }
        }
    }
}
