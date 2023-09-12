using static InOut;
using static MenuText;
using AddressBook;
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
            var user = MainControl.user;
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user);
            var clientsToChange = clientList.ToWorkingList();
            clientsToChange.ForEach(c => c.OwnerId = 1);
            clientList.ToWriteList(clientsToChange);
            
            
            
        }
    }
}
