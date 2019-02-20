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

using System.Threading.Tasks;
using UniTube.Core.Responses;

namespace UniTube.Core.Requests
{
    public class SearchListRequest : RequestBase<SearchListResponse>
    {
        private string part;

        internal SearchListRequest(string part) => this.part = part;

        public override Task<SearchListResponse> ExecuteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
