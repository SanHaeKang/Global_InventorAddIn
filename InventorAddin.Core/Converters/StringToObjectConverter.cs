using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAddin.Core.Converters
{
    public static class StringToObjectConverter
    {
        public static DateTime ConvertToDateTime(string _dateString)
        {
            return DateTime.ParseExact(_dateString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
