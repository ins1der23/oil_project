using static InOut;
using static MenuText;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace ClientBook
{
    public class FindClients
    {
        public static async Task<Clients> Start()
        {
            var user = MainControl.user;
            var searchString = InOut.GetString(MenuText.searchString).PrepareToSearch();
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user, searchString);
            return clientList;
        }
    }

}