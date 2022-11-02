using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Wiinf_Studie.API.Utils
{
    public static class DateTimeUtils
    {
        public static string ToISO8601Format(this DateTime date)
        {
            return date.ToString("o", CultureInfo.InvariantCulture);
        }
    }
}