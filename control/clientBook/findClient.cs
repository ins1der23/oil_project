using static InOut;
using static MenuText;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace ClientBook
{
    public class FindClient
    {
        public static async Task Start()
        {
            var user = MainControl.user;
            var clientToFind = new Client();
            var searchString = InOut.GetString(MenuText.clientSearch);
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user);
            Console.WriteLine(clientList);
        }
    }

}