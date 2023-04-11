namespace InventorAddin.CleanUp
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

            try
            {
                if (Directory.Exists(targetDirectory))
                {
                    Directory.Delete(targetDirectory, true);
                    Console.WriteLine("Addin이 삭제되었습니다.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("오류가 발생했습니다 : " + ex.Message);
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