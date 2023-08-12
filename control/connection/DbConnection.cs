using MySql.Data.MySqlClient;

namespace Connection
{
    class DBConnection
    {
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; private set; }
        private string Password { get; set; }
        public MySqlConnection? Connection { get; set; }

        private DBConnection(string server, string databaseName)
        {
            this.Server = server;
            this.DatabaseName = databaseName;
            this.UserName = SetUsername();
            this.Password = SetPassword();
        }

        private static DBConnection? _instance = null;
        public static DBConnection Instance(string server, string databaseName)
        {
            if (_instance == null)
                _instance = new DBConnection(server, databaseName);
            return _instance;
        }

        private string SetUsername() => InOut.GetString(MenuText.userName);
        private string SetPassword() => InOut.GetString(MenuText.password);
        
        public bool IsConnect()
        {
            if (Connection == null)
            {
                string connstring = $"Server={Server}; database={DatabaseName}; UID={UserName}; password={Password}";
                try
                {
                    Connection = new MySqlConnection(connstring);
                    Connection.Open();
                }
                catch (MySqlException)
                {
                    Console.WriteLine(MenuText.inputError);
                    _instance = null;
                    Connection = null;
                    return false;
                }
            }
            return true;
        }
        public void Close()
        {
            if (Connection != null)
                Connection.Close();
            Connection = null;
        }
    }
}




