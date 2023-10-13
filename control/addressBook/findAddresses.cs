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
            var user = Settings.user;
            var forSearch = InOut.GetString(AddrText.addressSearch).PrepareToSearch();
            var addressList = new Addresses();
            await addressList.GetFromSqlAsync(user, forSearch);
            return addressList;
        }
    }
}
