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
    public class VideoProcessingDetails
    {
        public string ProcessingStatus { get; set; }

        public ProcessingProgress ProcessingProgress { get; set; }

        public string ProcessingFailureReason { get; set; }

        public string FileDetailsAvailability { get; set; }

        public string ProcessingIssuesAvailability { get; set; }

        public string TagSuggestionsAvailability { get; set; }

        public string EditorSuggestionsAvailability { get; set; }

        public string ThumbnailsAvailability { get; set; }
    }
}
