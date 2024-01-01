using Controller;
using MenusAndChoices;

namespace Connection
{
    public class DBConnection
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; private set; }
        private string Password { get; set; }
        public int UserId { get; set; }
        public MySqlConnection? Connection { get; set; }
        public bool IsConnect { get; private set; }

        private DBConnection(string server, string databaseName)
        {
            Server = server;
            DatabaseName = databaseName;
            UserName = string.Empty;
            Password = string.Empty;
            if (Settings.isTest)
            {
                UserName = "root";
                Password = "Hacker$arefuck1ngevil";
                UserId = 1;
            }
            else
            {
                UserName = "diana_s";
                Password = "Diana1sthebe$tmanager";
                UserId = 3;
                // UserName = InOut.GetString(Text.userName);
                // switch (UserName)
                // {
                //     case "root":
                //         Password ="Hacker$arefuck1ngevil";
                //         UserId = 2;
                //         break;
                //     case "misha":
                //         Password = "M1shaisa$martguy";
                //         UserId = 1;
                //         break;
                //     case "diana_s":
                //         Password = "Diana1sthebe$tmanager";
                //         UserId = 3;
                //         break;
                // }
            }
        }
        private static DBConnection? _instance = null;
        public static DBConnection Instance(string server, string databaseName)
        {
            if (_instance == null)
                _instance = new DBConnection(server, databaseName);
            return _instance;
        }

        private void SetUsername(string userName) => UserName = userName;
        private void SetPassword(string password) => Password = password;//InOut.GetString(Text.password);
        public async Task ConnectAsync()
        {
            if (Connection == null)
            {
                string connstring = $"Server={Server}; database={DatabaseName}; UID={UserName}; password={Password}";
                try
                {
                    Connection = new MySqlConnection(connstring);
                    await Connection.OpenAsync();
                    IsConnect = true;
                }
                catch (MySqlException)
                {
                    Console.WriteLine(CommonText.inputError);
                    Connection = null;
                    _instance = null;
                }
            }
        }
        public void Close()
        {
            if (Connection != null)
                Connection.Close();
            Connection = null;
            IsConnect = false;
        }
    }
}




