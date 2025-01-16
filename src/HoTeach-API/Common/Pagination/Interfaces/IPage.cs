namespace HoTeach.API.Common.Pagination.Interfaces
{
    /// <summary>
    /// Represents a paginated collection of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public interface IPage<T> : IPage, IEnumerable<T>
    {
        // No additional members, inherits from IPage and IEnumerable<T>
    }

    /// <summary>
    /// Provides pagination information such as the current page, page size, and total count of items.
    /// </summary>
    public interface IPage
    {
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
    }
}