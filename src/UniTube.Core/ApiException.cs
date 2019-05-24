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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace UniTube.Core
{
    public class ApiException : Exception
    {
        public ApiError Error { get; set; }

        public static ApiException FromJson(string json)
            => JsonConvert.DeserializeObject<ApiException>(json);
    }

    public class ApiError
    {
        public List<Error> Errors { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }
    }

    public class Error
    {
        public string Domain { get; set; }

        public string Reason { get; set; }

        public string Message { get; set; }

        public string LocationType { get; set; }

        public string Location { get; set; }
    }
}
