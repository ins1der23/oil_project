using MySql.Data.MySqlClient;
public class DbConnect
{
    public string? Server { get; set; }
    public string? DatabaseName { get; set; }
    public string? UserName { private get; set; }
    public string? Password { private get; set; }
    public MySqlConnection? Connection { get; set; }

    private DbConnect()
    {
    }

    private static DbConnect? _instance = null;
    public static DbConnect Instance()
    {
        if (_instance == null)
            _instance = new DbConnect();
        return _instance;
    }
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
    }
}



