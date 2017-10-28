using UniTube.Core.Requests;

namespace UniTube.Core.Resources
{
    public class Search
    {
        /// <summary>
        /// Returns a collection of search results that match the query parameters specified in the API request.
        /// </summary>
        /// <param name="part">Specifies a comma-separated list of one or more <see cref="Search"/> resource
        /// properties that the API response will include.</param>
        public static SearchListRequest List(string part) => new SearchListRequest(part);
    }
}
