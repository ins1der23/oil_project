using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    internal class FindClients
    {
        public static async Task<Clients> Start()
        {
            var user = Settings.User;
            var searchString = GetString(CommonText.searchString);
            Clients clients = new();
            await clients.GetFromSqlAsync(search: searchString);
            return clients;
        }
    }
}