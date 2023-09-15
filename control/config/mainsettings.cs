using FileWork;
using MySql.Data.MySqlClient;
namespace Controller
{
    /// <summary>
    /// Класс отыкчающий за установку базовых настроек, запускается при запуске
    /// </summary>
    class MainSettings
    {
        static string path = "control/config/settings.txt";
        static FileToWork settings = new FileToWork(path);
        static bool check;

        public static DBConnection user
        {
            get => DBConnection.Instance("localhost", "oilproject");
        }

        public static async Task Set()
        {
            int connectCount = 0;
            while (!user.IsConnect && connectCount < 3)
            {
                await user.ConnectAsync();
                connectCount++;
            }
            
            await settings.Read();
            Setters.numResetter = settings.Lines.Where(l => l.ToLower().Contains("numresetter"))
                                                .First()
                                                .Contains("true");






        }
    }
}