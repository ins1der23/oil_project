using Models;
using MenusAndChoices;


namespace Handbooks
{
    public class OpenScan
    {
        public static async Task Start(Agreement agreement)
        {
            if (agreement.ScanPath == String.Empty)
            {
                await ShowString(AgrText.noScan);
                return;
            }
            string filePath = $"\"{agreement.ScanPath.Replace("/", "\\")}\"";
            string name = "explorer.exe";
            string argument = "/separate";
            var process = new StartProcess(name, filePath, argument);
        }
    }
}