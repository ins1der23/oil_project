using static InOut;
using static MenuText;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace ClientBook
{
    public class AddClient
    {
        public static async Task Start()
        {
            var user = MainControl.user;
            var clientToAdd = new Client();
            clientToAdd.Name = InOut.GetString(MenuText.clientName);
            
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user, searchString);
            
            


            
        }
    }

}