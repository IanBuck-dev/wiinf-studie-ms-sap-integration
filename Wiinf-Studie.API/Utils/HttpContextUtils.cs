using Wiinf_Studie.API.Models;

namespace Wiinf_Studie.API.Utils;

public static class HttpContextUtils
{
    public const string PageQueryKey = "page";
    public const string PageSizeQueryKey = "pageSize";
    public const string OrderByQueryKey = "orderBy";
    public const string FilterByQueryKey = "filterBy";

    /// <summary>
    /// Gets the page, pageSize, orderby and filterby from the current http context.
    /// Todo: Check if PowerApps supports such custom headers and query string contents.
    /// https://learn.microsoft.com/en-us/connectors/custom-connectors/policy-templates/setheader/setheader
    /// </summary>
    /// <param name="httpContext">The current http context.</param>
    public static RequestContext GetRequestContext(this HttpContext httpContext)
    {
        ArgumentNullException.ThrowIfNull(httpContext);

        // Get and validate page number (1 or more).
        var pageNumber = 1;

        if (httpContext.Request.Query.TryGetValue(PageQueryKey, out var pageHeader))
        {
            if (int.TryParse(pageHeader, out var parsedPage) && parsedPage >= 1)
            {
                pageNumber = parsedPage;
            }
        }

        // Get and validate page size (1 - 1000 items).
        var pageSize = 1000;

        if (httpContext.Request.Query.TryGetValue(PageSizeQueryKey, out var pageSizeHeader))
        {
            if (int.TryParse(pageSizeHeader, out var parsedPageSize) && parsedPageSize >= 1 && parsedPageSize <= 1000)
            {
                pageSize = parsedPageSize;
            }
        }

        // Get query string contents (default pairId ascending)
        // Todo: Implement FilterBy and OrderBy
        var orderBy = "PairId asc";

        if (httpContext.Request.Query.TryGetValue(OrderByQueryKey, out var orderByHeader))
        {
            if (!string.IsNullOrEmpty(orderByHeader))
            {
                orderBy = orderByHeader;
            }
        }

        var filterBy = "";

        if (httpContext.Request.Query.TryGetValue(FilterByQueryKey, out var filterByHeader))
        {
            if (!string.IsNullOrEmpty(filterByHeader))
            {
                filterBy = filterByHeader;
            }
        }

        var requestContext = new RequestContext()
        {
            Page = pageNumber,
            PageSize = pageSize,
            OrderBy = orderBy,
            FilterBy = filterBy,
        };

        return requestContext;
    }

    /// <summary>
    /// Adds the pagination info onto the 
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="context"></param>
    public static void AddPagedContextInfo(this HttpContext httpContext, RequestContext context)
    {
        httpContext.Response.Headers.TryAdd("page", context.Page.ToString());
        httpContext.Response.Headers.TryAdd("pageSize", context.PageSize.ToString());
        // Todo: add total items count if needed.
    }
}
