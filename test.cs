using static InOut;
using MenusAndChoices;
using Handbooks;
using Controller;
using Models;
using Connection;


namespace Testing
{
    public class Test
    {
        public static async Task Start()
        {
            var user = Settings.User;
            Addresses addressSql = new();
            await addressSql.GetFromSqlAsync(user);
            Console.WriteLine("Addresses OK");
            await Task.Delay(1000);
            Clients clients = new();
            await clients.GetFromSqlAsync(user);
            Console.WriteLine("Clients OK");
            await Task.Delay(1000);
            Passports passports = new();
            await passports.GetFromSqlAsync(user);
            Console.WriteLine("Passports OK");
            await Task.Delay(1000);
            Registrations registrations = new();
            await registrations.GetFromSqlAsync(user);
            Console.WriteLine("Registrations OK");
            await Task.Delay(1000);
            Agreements agreements = new();
            await agreements.GetFromSqlAsync(user);
            Console.WriteLine("Agreements OK");
            await IssuedControl.Start();
        }

    }
}
