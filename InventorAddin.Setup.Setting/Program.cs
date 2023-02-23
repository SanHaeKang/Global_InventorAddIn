using System.Xml.Linq;

namespace InventorAddin.Setup.Setting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Autodesk\Inventor 2023\Addins\";

            XDocument doc = XDocument.Load(appDataPath + "Autodesk.InventorAddin.Inventor.addin");

            XElement assemblyElement = doc.Descendants("Assembly").FirstOrDefault();

            if (assemblyElement != null)
            {
                assemblyElement.Value = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Autodesk\ApplicationPlugins\Globalsoft_InventorAddin\InventorAddin.dll";
            }

            doc.Save(appDataPath + "Autodesk.InventorAddin.Inventor.addin");
        }
    }
}