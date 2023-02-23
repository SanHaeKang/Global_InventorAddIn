using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAddin.Core.Converters
{
    public class ObjectToStringConverters
    {
        public static string ConvertToString(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
