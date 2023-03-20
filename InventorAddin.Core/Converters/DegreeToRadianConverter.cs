using Inventor;
using System;
using System.Globalization;
using System.Windows.Data;

namespace InventorAddin.Core.Converters
{
    public class DegreeToRadianConverter
    {
        public static double DegreeToRadianConvert(double degree)
        {
            return degree * Math.PI / 180;
        }

        public static double RadianToDegreeConvert(double radian)
        {
            return radian * 180 / Math.PI;
        }
    }
}