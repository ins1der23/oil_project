using MenusAndChoices;
using Models;
using Controller;


namespace Handbooks

{
    public class AttachScan
    {
        public static async Task<Agreement> Start(Agreement agreement)
        {
            var user = Settings.User;
            Client client = agreement.Client;
            string sourcePath = GetString(AgrText.scanPath);
            string folder = "/agreements/";
            await ShowString(sourcePath, delay: 200);
            if (sourcePath == string.Empty)
            {
                await ShowString(Text.saveError, delay: 200);
                return agreement;
            }
            sourcePath = sourcePath.Replace("\"", string.Empty);
            FileInfo sourceFile = new(sourcePath);
            string receivePath = $"{Settings.ScanPath}{folder}{agreement.FileName}{sourceFile.Extension}";
            try
            {
                sourceFile.CopyTo(receivePath, true);
            }
            catch (Exception)
            {
                await ShowString(Text.saveError, delay: 200);
                throw;
            }
            agreement.ScanPath = receivePath;
            var agrList = new Agreements();
            agreement = await agrList.SaveChanges(user, agreement);
            agreement.Client = client;
            return agreement;
        }
    }
}