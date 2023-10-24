using Connection;
using Models;

namespace Controller
{
    /// <summary>
    /// Класс отвечающий за установку базовых настроек, запускается при запуске
    /// </summary>
    static class Settings
    {
        static readonly string configPath = "X:/oilproject/config/settings.txt";
        public static string scanPath = "X:/oilproject/scans";

        static readonly FileWork settings = new(configPath);
        public static Setter numResetter = new("numResetter");

        public static DBConnection User
        {
            // get => DBConnection.Instance("profit.dns-cloud.net", "oilproject");
            get => DBConnection.Instance("127.0.0.1", "oilproject");
        }

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