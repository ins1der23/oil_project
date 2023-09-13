using static InOut;
using static Text;
using Handbooks;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;
using System.Linq;

using System.Collections;

namespace Testing
{
    public class Test
    {
        public static async Task Start()
        {
            Console.Clear();
            var user = MainControl.user;
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user, "Наш склад");
            Console.WriteLine(clientList);
            var client = clientList.GetFromList();
            Console.WriteLine(client.InfoToString());
        }
    }
}
