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
            var searchString = GetString(Text.searchString);
            Clients clients = new();
            await clients.GetFromSqlAsync(user, search: searchString);
            return clients;
        }
    }
}