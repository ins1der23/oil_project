using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;

namespace Handbooks
{
    public class FindClients
    {
        public static async Task<Clients> Start()
        {
            var user = Settings.user;
            var searchString = InOut.GetString(Text.searchString);
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user, searchString);
            return clientList;
        }
    }

}