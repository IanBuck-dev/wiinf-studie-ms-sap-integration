using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wiinf_Studie.API.Models;

public class RequestContext
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 1000;
    public string? FilterBy { get; set; }
    public string? OrderBy { get; set; }
}
