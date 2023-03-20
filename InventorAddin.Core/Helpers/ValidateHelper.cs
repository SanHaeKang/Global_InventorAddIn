using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorAddin.Core.Helpers
{
    public class ValidateHelper
    {
        public static bool IsDecimalNumber(string senderText, string text)
        {
            bool isNumeric = double.TryParse(text, out double result);
            bool isDecimalSeparator = text == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            bool isValidDecimal = !(isDecimalSeparator && senderText.Contains(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));

            return isNumeric || (isDecimalSeparator && isValidDecimal);
        }
    }
}
