using static InOut;
using MenusAndChoices;
using Handbooks;
using Controller;
using Models;
using Connection;
using Org.BouncyCastle.Tls;


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
                int choice = await MenuToChoice(TestText.options, TestText.test, CommonText.choice, noNull: true);
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
                        await streets.GetFromSqlAsync();
                        await ShowString("Streets OK", false, delay);
                        Passports passports = new();
                        await passports.GetFromSqlAsync();
                        await ShowString("Passports OK", false, delay);
                        Issueds issueds = new();
                        await issueds.GetFromSqlAsync();
                        await ShowString("Issueds OK", false, delay);
                        Registrations registrations = new();
                        await registrations.GetFromSqlAsync();
                        await ShowString("Registrations OK", false, delay);
                        ClientsRepo clients = new();
                        await clients.GetFromSqlAsync();
                        if (!clients.IsEmpty) await ShowString("Clients OK", false, delay);
                        Agreements agreements = new();
                        await agreements.GetFromSqlAsync(user, search: "1", id: 56);
                        if (!agreements.IsEmpty()) await ShowString("Agreements OK", false, delay);
                        break;
                    case 2: // Тест методов
                        
                        
                        var issuedBy = await StartIssuedByUI.Start();
                        Console.WriteLine(issuedBy);
                        Console.ReadLine();
                        break;
                    case 3: //Выход
                        flag = false;
                        break;
                }
            }













        }

    }
}
