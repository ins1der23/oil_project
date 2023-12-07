using Connection;
using Models;

namespace Controller
{
    /// <summary>
    /// Класс отвечающий за установку базовых настроек, запускается при запуске
    /// </summary>
    static class Settings
    {
        public static bool isTest = false;
        public static string baseName = "oilproject";
        public static string ServerPath => isTest ? "127.0.0.1" : "profit.dns-cloud.net";
        public static string ScanPath => isTest ? "D:/oilproject/scans" : "X:/oilproject/scans";
        public static DBConnection User = DBConnection.Instance(ServerPath, baseName);
        public static int UserId;
        static readonly string configPath = "X:/oilproject/config/settings.txt";
        static readonly FileWork settings = new(configPath);
        public static Setter numResetter = new("numResetter");

        public static async Task<bool> Connect()
        {
            bool status = false;
            int connectCount = 0;
            while (!status && connectCount < 3)
            {
                await User.ConnectAsync();
                connectCount++;
                status = User.IsConnect;
            }
            User.Close();
            return status;
        }
        public static async Task<bool> Set()
        {
            UserId = User.UserId;
            bool checkFile = await settings.Read();
            if (!checkFile) return false;
            numResetter.Status = settings.Lines.Where(l => l.ToLower().Contains("numresetter"))
                                                .First().ToLower().Contains("true");
            numResetter.ResetByDate();
            return true;
        }

        public static async Task Save()
        {
            settings.Clear();
            settings.Append($"{numResetter}");
            await settings.Write();
        }
    }
}