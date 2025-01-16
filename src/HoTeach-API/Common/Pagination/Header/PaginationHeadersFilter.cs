using HoTeach.API.Common.Pagination.Extensions;
using HoTeach.API.Common.Pagination.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HoTeach.API.Common.Pagination.Header
{
    public class PaginationHeadersFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(
            ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult
                && objectResult.Value is IPage page)
            {
                context.HttpContext.Response.Headers.AddPaginationHeader(
                    page.CurrentPage,
                    page.PageSize,
                    page.TotalCount);
            }

            await next();
        }
    }
}