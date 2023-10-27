using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class FindClients
    {
        public static async Task<Clients> Start()
        {
            var user = Settings.User;
            var searchString = InOut.GetString(Text.searchString);
            var clientList = new Clients();
            await clientList.GetFromSqlAsync(user, searchString);
            return clientList;
        }
    }

}