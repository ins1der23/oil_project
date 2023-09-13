using static InOut;
using static Text;
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
            var forSearch = InOut.GetString(searchString).PrepareToSearch();
            var addressList = new Addresses();
            await addressList.GetFromSqlAsync(user, forSearch);
            return addressList;
        }
    }
}
