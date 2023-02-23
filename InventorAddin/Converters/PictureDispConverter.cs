using System.Drawing;
using System.Windows.Forms;
using stdole;

namespace InvAddIn.Converters
{
    public class PictureDispConverter : AxHost
    {
        public static IPictureDisp Convert(Bitmap bitmap)
        {
            return (IPictureDisp)GetIPictureDispFromPicture(bitmap);
        }

        public PictureDispConverter(string clsid) : base(clsid)
        {
        }

        public PictureDispConverter(string clsid, int flags) : base(clsid, flags)
        {
        }
    }
}
