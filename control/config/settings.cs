using MySql.Data.MySqlClient;
using Connection;
using Models;

namespace Controller
{
    /// <summary>
    /// Класс отвечающий за установку базовых настроек, запускается при запуске
    /// </summary>
    static class Settings
    {
        static string configPath = "X:/oilproject/config/settings.txt";
        public static string scanPath = "X:/oilproject/scans";
        
        static FileWork settings = new FileWork(configPath);
        public static Setter numResetter = new Setter("numResetter");

        public static DBConnection user
        {
            get => DBConnection.Instance("192.168.1.3", "oilproject");
        }

        public static async Task<bool> Connect()
        {
            bool status = false;
            int connectCount = 0;
            while (!status && connectCount < 3)
            {
                await user.ConnectAsync();
                connectCount++;
                status = user.IsConnect;
            }
            user.Close();
            return status;
        }
        public static async Task Set()
        {
            await settings.Read();
            if (settings.IsNotEmpty)
                numResetter.Status = settings.Lines.Where(l => l.ToLower().Contains("numresetter"))
                                                    .First().ToLower().Contains("true");
            numResetter.ResetByDate();
        }
        public static async Task Save()
        {
            settings.Clear();
            settings.Append($"{numResetter}");
            await settings.Write();
        }
    }
}