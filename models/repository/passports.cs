namespace Models;

public class Passports
{
    List<Passport> PassportList { get; set; }

    public Passports()
    {
        PassportList = new();
    }
}
