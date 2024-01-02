using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    internal class FindClients
    {
        public static async Task<Clients<Client>> Start()
        {
            var user = Settings.User;
            var searchString = GetString(CommonText.searchString);
            Clients<Client> clients = new();
            await clients.GetFromSqlAsync(search: searchString);
            return clients;
        }
    }
}