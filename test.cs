using static InOut;
using static MenuText;
using AddressBook;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Testing
{
    public class Test
    {
        public static async Task Start()
        {
            var user = MainControl.user;
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user);
            Console.WriteLine(clientList);
        }
    }
}