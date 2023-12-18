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
            bool flag = true;
            while (flag)
            {
                int choice = await MenuToChoice(TestText.options, TestText.test, Text.choice, noNull: true);
                switch (choice)
                {
                    case 1: // Тест GetFromSql
                        var user = Settings.User;
                        Addresses addressSql = new();
                        await addressSql.GetFromSqlAsync(user);
                        await ShowString("Addresses OK", false, delay);
                        Districts districts = new();
                        await districts.GetFromSqlAsync(user, 1);
                        await ShowString("Districts OK", false, delay);
                        Locations locations = new();
                        await locations.GetFromSqlAsync(user, 1);
                        await ShowString("Locations OK", false, delay);
                        Streets streets = new();
                        await streets.GetFromSqlAsync(user, 1);
                        await ShowString("Streets OK", false, delay);
                        Passports passports = new();
                        await passports.GetFromSqlAsync(user);
                        await ShowString("Passports OK", false, delay);
                        Issueds issueds = new();
                        await issueds.GetFromSqlAsync(user);
                        await ShowString("Issueds OK", false, delay);
                        Registrations registrations = new();
                        await registrations.GetFromSqlAsync(user);
                        await ShowString("Registrations OK", false, delay);
                        Clients clients = new();
                        await clients.GetFromSqlAsync(user);
                        await ShowString("Clients OK", false, delay);
                        Agreements agreements = new();
                        await agreements.GetFromSqlAsync(user);
                        await ShowString("Agreements OK", false, delay);
                        break;
                    case 2: // Тест методов
                        // user = Settings.User;
                        // PassportList passportList = new();
                        // await passportList.GetFromSqlAsync(user, "12");
                        // Passport? passport = passportList.GetFromList() as Passport;
                        // Console.WriteLine(passportList.IsEmpty);
                        // Console.WriteLine(passport);
                        // Console.ReadLine();
                        await AddressControl.Start();
                        
                        
                        



                        break;
                    case 3: //Выход
                        flag = false;
                        break;
                }
            }













        }

    }
}
