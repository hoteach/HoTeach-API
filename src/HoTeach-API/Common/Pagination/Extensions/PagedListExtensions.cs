using HoTeach.API.Common.Pagination.Interfaces;

namespace HoTeach.API.Common.Pagination.Extensions
{
    public static class PagedListExtensions
    {
        /// <summary>
        /// Converts an <see cref="IPagedList{T}"/> to a <see cref="IPage{T}"/> by creating a new <see cref="Page{T}"/> instance.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="pagedList">The paged list to convert.</param>
        /// <returns>An <see cref="IPage{T}"/> instance that represents the paged list.</returns>
        public static IPage<T> ToPage<T>(this IPagedList<T> pagedList) => new Page<T>(
            pagedList.Items,
            pagedList.PageIndex,
            pagedList.PageSize,
            (int)pagedList.TotalCount);

    }
}
