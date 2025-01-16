using System.Collections;
using HoTeach.API.Common.Pagination.Interfaces;

namespace HoTeach.API.Common.Pagination
{
    /// <summary>
    /// Represents a paginated collection of items with pagination information.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public class Page<T> : IPage<T>
    {
        private readonly IEnumerable<T> _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="Page{T}"/> class with the provided values, current page, page size, and total count.
        /// </summary>
        /// <param name="values">The collection of items on the current page.</param>
        /// <param name="currentPage">The current page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="totalCount">The total count of items across all pages.</param>
        public Page(IEnumerable<T> values, int currentPage, int pageSize, int totalCount)
        {
            _values = values;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// Gets the number of items per page.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Gets the total count of items available across all pages.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Returns an enumerator that iterates through the collection of items on the current page.
        /// </summary>
        /// <returns>An enumerator for the collection of items on the current page.</returns>
        public IEnumerator<T> GetEnumerator() => _values.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection of items on the current page.
        /// Implements the non-generic version of <see cref="GetEnumerator"/> for compatibility with <see cref="IEnumerable"/>.
        /// </summary>
        /// <returns>An enumerator for the collection of items on the current page.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _values.GetEnumerator();
    }
}