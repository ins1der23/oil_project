using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class FindClients
    {
        public static async Task<ClientsRepo> Start()
        {
            var user = Settings.User;
            var searchString = GetString(CommonText.searchString);
            ClientsRepo clients = new();
            await clients.GetFromSqlAsync(search: searchString);
            return clients;
        }
    }
}