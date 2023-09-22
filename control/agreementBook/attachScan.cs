using static InOut;
using MenusAndChoices;
using Models;
using Controller;


namespace Handbooks

{
    public class AttachScan
    {
        public static async Task<Agreement> Start(Agreement agreement)
        {
            var user = Settings.user;
            string sourcePath = InOut.GetString(AgrText.scanPath);
            string folder = "/agreements/";
            Console.WriteLine(sourcePath);
            if (sourcePath == String.Empty)
            {
                Console.WriteLine("ОШИБКА");
                return agreement;
            }
            sourcePath = sourcePath.Replace("\"", String.Empty);
            Console.WriteLine(sourcePath);
            FileInfo sourceFile = new FileInfo(sourcePath);
            string receivePath = $"{Settings.scanPath}{folder}{agreement.FileName}{sourceFile.Extension}";
            if (sourceFile.Exists) sourceFile.CopyTo(receivePath, true);
            else Console.WriteLine("ОШИБКА");
            agreement.ScanPath = receivePath;
            var agrList = new Agreements();
            agreement = await agrList.SaveChanges(user, agreement);
            return agreement;
        }
    }
}