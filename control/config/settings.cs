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
            if (isTest)
            {
                User.SetUsername("root");
                User.SetPassword("Hacker$arefuck1ngevil");
            }
            else
            {
                // string UserName = InOut.GetString(Text.userName);
                string userName = "diana_s";
                User.SetUsername(userName);
                switch (userName)
                {
                    case "root":
                        User.SetPassword("Hacker$arefuck1ngevil");
                        User.UserId = 2;
                        break;
                    case "misha":
                        User.SetPassword("M1shaisa$martguy");
                        User.UserId = 1;
                        break;
                    case "diana_s":
                        User.SetPassword("Diana1sthebe$tmanager");
                        User.UserId = 3;
                        break;
                }
            }

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