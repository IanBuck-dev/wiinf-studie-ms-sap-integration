using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wiinf_Studie.API.Data
{
    public static class DatabaseConfiguration
    {
        public const string DatabaseNameKey = "DatabaseName";
        public static string DatabaseName { get; set; }
    }
}