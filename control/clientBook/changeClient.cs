using static InOut;
using static MenuText;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace ClientBook
{
    public class ChangeClient
    {
        public static async Task Start()
        {
            var user = MainControl.user;
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user, searchString);
        }
    }

}