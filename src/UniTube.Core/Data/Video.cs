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

namespace UniTube.Core.Data
{
    public class Video
    {
        public string Kind { get; set; }

        public string Etag { get; set; }

        public string Id { get; set; }

        public VideoSnippet Snippet { get; set; }

        public VideoContentDetails ContentDetails { get; set; }

        public VideoStatus Status { get; set; }

        public VideoStatistics Statistics { get; set; }

        public VideoPlayer Player { get; set; }

        public VideoTopicDetails TopicDetails { get; set; }

        public VideoRecordingDetails RecordingDetails { get; set; }

        public VideoFileDetails FileDetails { get; set; }

        public VideoProcessingDetails ProcessingDetails { get; set; }

        public VideoSuggestions Suggestions { get; set; }

        public VideoLiveStreamingDetails LiveStreamingDetails { get; set; }

        public Dictionary<string, VideoLocalization> Localizations { get; set; }
    }
}
