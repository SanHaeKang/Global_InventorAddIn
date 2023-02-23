using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventor;

namespace InventorAddin.Core.Helpers
{
    public class UnitHelper
    {
        public static string UnitToString(UnitsTypeEnum unit)
        {
            string result = string.Empty;

            switch (unit)
            {
                case UnitsTypeEnum.kMillimeterLengthUnits:
                    result = "mm";
                    break;
                case UnitsTypeEnum.kCentimeterLengthUnits:
                    result = "cm";
                    break;
                case UnitsTypeEnum.kInchLengthUnits:
                    result = "inch";
                    break;
                default:
                    throw new ArgumentException("Invalid Unit Type.");
            }

            return result;
        }
    }
}
