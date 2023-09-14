using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Handbooks

{
    public class ChangeClient
    {
        public static async Task Start()
        {
            var user = MainControl.user;
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user);
        }
    }

}