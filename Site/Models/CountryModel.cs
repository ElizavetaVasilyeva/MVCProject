using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public static class CountryModel
    {
        public static IEnumerable<string> GetCountries()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                              .Select(x => new RegionInfo(x.LCID).EnglishName)
                              .Distinct()
                              .OrderBy(x => x);
        }
    }
}