using MySql.Data.MySqlClient;
using MenusAndChoices;

namespace Connection
{
    public class DBConnection
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; private set; }
        private string Password { get; set; }
        public int UserId
        {
            get => UserName == "root" ? 3 : 1;

        }
        public MySqlConnection? Connection { get; set; }
        public bool IsConnect { get; private set; }

        private DBConnection(string server, string databaseName)
        {
            this.Server = server;
            this.DatabaseName = databaseName;
            this.UserName = "root";//SetUsername();
            this.Password = "Hacker$arefuck1ngevil";//SetPassword();
        }

        private static DBConnection? _instance = null;
        public static DBConnection Instance(string server, string databaseName)
        {
            if (_instance == null)
                _instance = new DBConnection(server, databaseName);
            return _instance;
        }

        private string SetUsername() => InOut.GetString(Text.userName);
        private string SetPassword() => InOut.GetString(Text.password);
        public int GetUserId()
        {
            string choice = UserName;
            switch (choice)
            {
                case "root":
                    return 1;
            }
            return 3;
        }

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
                    Console.WriteLine(Text.inputError);
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




