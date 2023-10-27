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
            int delay = 100;
            var user = Settings.User;
            Addresses addressSql = new();
            await addressSql.GetFromSqlAsync(user);
            await ShowString("Addresses OK", false, delay);
            Clients clients = new();
            await clients.GetFromSqlAsync(user);
            await ShowString("Clients OK", false, delay);
            Passports passports = new();
            await passports.GetFromSqlAsync(user);
            await ShowString("Passports OK", false, delay);
            Registrations registrations = new();
            await registrations.GetFromSqlAsync(user);
            await ShowString("Registrations OK", false, delay);
            Agreements agreements = new();
            await agreements.GetFromSqlAsync(user);
            await ShowString("Agreements OK", false, delay);
            Districts districts = new();
            await districts.GetFromSqlAsync(user);
            await ShowString("Districts OK", false, delay);
            City city = await CityControl.Start();
            await ShowString(city.Name);
            Location location = await LocationControl.Start(city);
            await ShowString(location.Name);
            Street street = await StreetControl.Start(city);
            await ShowString(street.Name);
            Console.ReadLine();









        }

    }
}
