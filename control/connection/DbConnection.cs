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
        }
        private static DBConnection? _instance = null;
        public static DBConnection Instance(string server, string databaseName)
        {
            if (_instance == null)
                _instance = new DBConnection(server, databaseName);
            return _instance;
        }

        public void SetUsername(string userName) => UserName = userName;
        public void SetPassword(string password) => Password = password;//InOut.GetString(Text.password);
        public int GetUserId()
        {
            string choice = UserName;
            switch (choice)
            {
                case "misha":
                    return 1;
                case "diana_s":
                    return 3;
            }
            return 2;
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




