using static InOut;
using static Text;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Handbooks
{
    public class FindClients
    {
        public static async Task<Clients> Start()
        {
            var user = MainControl.user;
            var searchString = InOut.GetString(Text.searchString);
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user, searchString);
            return clientList;
        }
    }

}