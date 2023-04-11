using System.Xml;

namespace InventorAddin.Setup
{
    internal class Program
    {
        private static readonly string programFilesPath = Environment.GetEnvironmentVariable("PROGRAMFILES");
        private static readonly string allUsersProfilePath = Environment.GetEnvironmentVariable("ALLUSERSPROFILE");

        static void Main(string[] args)
        {
            Console.WriteLine("Inventor 버전을 입력해주세요(기본:2024)");
            string version = ReadVersion();
            
            string sourceDirectory = Directory.GetCurrentDirectory(); // 복사할 파일들이 있는 디렉토리 경로
            //string sourceDirectory = @"C:\Users\tksgo\Desktop\GIT\InventorAddIn\InventorAddin\bin\Debug"; // 복사할 파일들이 있는 디렉토리 경로
            string targetDirectory = string.Empty; // 복사한 파일들을 붙여넣을 디렉토리 경로 
            
            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourceDirectory); // 디렉토리 정보 가져오기
            FileInfo[] files = sourceDirectoryInfo.GetFiles(); // 디렉토리 내의 파일들에 대한 정보 가져오기
            
            switch (version)
            {
                case "2024":
                    targetDirectory = programFilesPath + @"\Autodesk\Inventor " + version + @"\Bin\Addins\Globalsoft_InventorAddin";
                    break;
                default:
                    targetDirectory = allUsersProfilePath + @"\Autodesk\Inventor " + version + @"\Addins\Globalsoft_InventorAddin";
                    break;
            }

            Directory.CreateDirectory(targetDirectory);

            foreach (FileInfo file in files) // 파일들을 대상 디렉토리로 복사하기
            {
                string tempPath = Path.Combine(targetDirectory, file.Name);
                file.CopyTo(tempPath, true);
            }

            // XmlDocument 생성
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclaration);

            // Addin 루트 엘리먼트 생성
            XmlElement addin = doc.CreateElement("Addin");
            addin.SetAttribute("Type", "Standard");

            // ClassId, ClientId, DisplayName, Description, Assembly, OSType, LoadAutomatically, UserUnloadable, Hidden, SupportedSoftwareVersionGreaterThan, DataVersion, LoadBehavior, UserInterfaceVersion 엘리먼트 생성
            XmlElement classId = doc.CreateElement("ClassId");
            classId.InnerText = "{b2e5a201-9d46-4e2d-8d8a-b16748936219}";

            XmlElement clientId = doc.CreateElement("ClientId");
            clientId.InnerText = "{b2e5a201-9d46-4e2d-8d8a-b16748936219}";

            XmlElement displayName = doc.CreateElement("DisplayName");
            displayName.InnerText = "Globalsoft_InventorAddin";

            XmlElement description = doc.CreateElement("Description");
            description.InnerText = "InventorAddin";

            XmlElement assembly = doc.CreateElement("Assembly");
            assembly.InnerText = "InventorAddin.dll";

            XmlElement osType = doc.CreateElement("OSType");
            osType.InnerText = "Win64";

            XmlElement loadAutomatically = doc.CreateElement("LoadAutomatically");
            loadAutomatically.InnerText = "1";

            XmlElement userUnloadable = doc.CreateElement("UserUnloadable");
            userUnloadable.InnerText = "1";

            XmlElement hidden = doc.CreateElement("Hidden");
            hidden.InnerText = "0";

            XmlElement supportedSoftwareVersionGreaterThan = doc.CreateElement("SupportedSoftwareVersionGreaterThan");
            supportedSoftwareVersionGreaterThan.InnerText = "16..";

            XmlElement dataVersion = doc.CreateElement("DataVersion");
            dataVersion.InnerText = "1";

            XmlElement loadBehavior = doc.CreateElement("LoadBehavior");
            loadBehavior.InnerText = "2";

            XmlElement userInterfaceVersion = doc.CreateElement("UserInterfaceVersion");
            userInterfaceVersion.InnerText = "1";

            // 엘리먼트를 Addin에 추가
            addin.AppendChild(classId);
            addin.AppendChild(clientId);
            addin.AppendChild(displayName);
            addin.AppendChild(description);
            addin.AppendChild(assembly);
            addin.AppendChild(osType);
            addin.AppendChild(loadAutomatically);
            addin.AppendChild(userUnloadable);
            addin.AppendChild(hidden);
            addin.AppendChild(supportedSoftwareVersionGreaterThan);
            addin.AppendChild(dataVersion);
            addin.AppendChild(loadBehavior);
            addin.AppendChild(userInterfaceVersion);

            // Addin을 XmlDocument에 추가
            doc.AppendChild(addin);

            // XmlDocument를 파일로 저장
            string xmlFileName = Path.Combine(targetDirectory, "Autodesk.InventorAddin.Inventor.addin");
            using (FileStream fileStream = new FileStream(xmlFileName, FileMode.Create))
            {
                doc.Save(fileStream);
            }

            Console.WriteLine("완료되었습니다. 종료를 원하시면 Enter를 입력해주세요.");
            Console.ReadLine();
        }

        private static string ReadVersion()
        {
            Console.Write(">> ");
            return Console.ReadLine();
        }
    }
}