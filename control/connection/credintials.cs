using static MenuText;

public class Credintials
{
    public string server { get; set; }
    public string databaseName { get; set; }
    public string userName { get; set; }
    private string password { get; set; }

    public Credintials(string server, string databaseName, string userName)
    {
        this.server = server;
        this.databaseName = databaseName;
        this.userName = userName;
        this.password = GetPassword();
    }

    private static string GetPassword()
    {
        Console.WriteLine(MenuText.password);
        string password = String.Empty;
        password += Console.ReadLine();
        return password;
    }
    public static string ReturnPassword(Credintials credintials) => credintials.password;
}



