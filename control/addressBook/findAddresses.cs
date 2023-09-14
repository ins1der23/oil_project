using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Handbooks
{
    public class FindAddresses

    {
        public static async Task<Addresses> Start()
        {
            var user = MainControl.user;
            var forSearch = InOut.GetString(Text.searchString).PrepareToSearch();
            var addressList = new Addresses();
            await addressList.GetFromSqlAsync(user, forSearch);
            return addressList;
        }
    }
}
