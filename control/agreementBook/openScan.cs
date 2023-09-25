using Models;
using MenusAndChoices;
using System.Threading;
using static InOut;

namespace Handbooks
{
    public class OpenScan
    {
        public static async Task Start(Agreement agreement)
        {
            if (agreement.ScanPath == String.Empty)
            {
                ShowString(AgrText.noScan);
                await Task.Delay(1000);
                return;
            }
            string filePath = $"\"{agreement.ScanPath.Replace("/", "\\")}\"";
            string name = "explorer.exe";
            string argument = "/separate";
            var process = new StartProcess(name, filePath, argument);
        }
    }
}