using Wiinf_Studie.API.Models;

namespace Wiinf_Studie.API.Utils;

public static class HttpContextUtils
{
    public const string PageHeaderKey = "page";
    public const string PageSizeHeaderKey = "pageSize";

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

        if (httpContext.Request.Headers.TryGetValue(PageHeaderKey, out var pageHeader))
        {
            if (int.TryParse(pageHeader, out var parsedPage) && parsedPage >= 1)
            {
                pageNumber = parsedPage;
            }
        }

        // Get and validate page size (1 - 1000 items).
        var pageSize = 1;

        if (httpContext.Request.Headers.TryGetValue(PageSizeHeaderKey, out var pageSizeHeader))
        {
            if (int.TryParse(pageSizeHeader, out var parsedPageSize) && parsedPageSize >= 1 && parsedPageSize <= 1000)
            {
                pageSize = parsedPageSize;
            }
        }

        // Get query string contents (default pairId ascending)
        // Todo: Implement FilterBy and OrderBy
        var orderBy = "PairId asc";

        var requestContext = new RequestContext()
        {
            Page = pageNumber,
            PageSize = pageSize,
            OrderBy = orderBy,
            FilterBy = null,
        };

        return requestContext;
    }
}
