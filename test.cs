using static InOut;
using MenusAndChoices;
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
            await clientList.GetFromSqlAsync(user);
            Console.WriteLine(clientList);
            var clients = clientList.ToWorkingList();
            clients.ForEach(c => c.OwnerId = 3);
            foreach (var item in clients)
            {
                Console.WriteLine($"{item.OwnerId}     {item.AddressId}");
            }
            clientList.ToWriteList(clients);
            await clientList.ChangeSqlAsync(user);


        }
    }
}
