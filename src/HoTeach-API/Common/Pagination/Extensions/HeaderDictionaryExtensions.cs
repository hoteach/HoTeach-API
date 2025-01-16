using HoTeach.API.Common.Pagination.Header;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace HoTeach.API.Common.Pagination.Extensions
{
    public static class HeaderDictionaryExtensions
    {
        /// <summary>
        /// Adds pagination information as a custom header to the response.
        /// </summary>
        /// <param name="headers">The collection of headers to which the pagination information will be added.</param>
        /// <param name="currentPage">The current page number to include in the header.</param>
        /// <param name="pageSize">The number of items per page to include in the header.</param>
        /// <param name="totalCount">The total count of items available across all pages to include in the header.</param>
        public static void AddPaginationHeader(this IHeaderDictionary headers, int currentPage, int pageSize, int totalCount)
        {
            var paginationHeaderValue = new PaginationHeaderValue
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = CalculateTotalPages(pageSize, totalCount) 
            };

            headers[PaginationHeaderNames.PaginationHeaderName] = JsonConvert.SerializeObject(paginationHeaderValue);
        }

        private static int CalculateTotalPages(int pageSize, int totalCount)
        {
            if (pageSize == 0)
            {
                return default;
            }

            var totalPages = totalCount / pageSize;

            if (totalCount % pageSize != 0)
            {
                totalPages++;
            }

            return totalPages;
        }
    }
}
