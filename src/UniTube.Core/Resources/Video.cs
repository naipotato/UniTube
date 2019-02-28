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
using UniTube.Core.Requests;

namespace UniTube.Core.Resources
{
    /// <summary>
    /// A video resource represents a YouTube video.
    /// </summary>
    public class Video
    {
        /// <summary>
        /// Identifies the API resource's type.
        /// </summary>
        public string Kind { get; set; }

        /// <summary>
        /// The Etag of this resource.
        /// </summary>
        public string Etag { get; set; }

        /// <summary>
        /// The ID that YouTube uses to uniquely identify the video.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The snippet object contains basic details about the video, such as
        /// its title, description, and category.
        /// </summary>
        public VideoSnippet Snippet { get; set; }

        /// <summary>
        /// Contains basic details about the video, such as its title,
        /// description, and category.
        /// </summary>
        public class VideoSnippet
        {
            /// <summary>
            /// The date and time that the video was published.
            /// </summary>
            public DateTime PublishedAt { get; set; }

            /// <summary>
            /// The ID that YouTube uses to uniquely identify the channel that
            /// the video was uploaded to.
            /// </summary>
            public string ChannelId { get; set; }

            /// <summary>
            /// The video's title.
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// The video's description.
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// A map of thumbnail images associated with the video.
            /// </summary>
            public Thumbnails Thumbnails { get; set; }

            /// <summary>
            /// Channel title for the channel that the video belongs to.
            /// </summary>
            public string ChannelTitle { get; set; }

            /// <summary>
            /// A list of keyword tags associated with the video.
            /// </summary>
            public List<string> Tags { get; set; }
        }

        /// <summary>
        /// Returns a list of videos that match the API request parameters.
        /// </summary>
        /// <param name="part">Specifies a comma-separated list of one or more
        /// video resource properties that the API response will include.</param>
        /// <return>An instance of VideoListRequest to configure the
        /// request.</return>
        public static VideoListRequest List(string part)
            => new VideoListRequest(part);
    }
}
