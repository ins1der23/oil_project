using static InOut;
using static MenuText;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace AddressBook
{
    public class FindAddresses
    {
        static async Task<Addresses> Start()
        {
            var user = MainControl.user;
            var searchString = InOut.GetString(MenuText.searchString).PrepareToSearch();
            var addressList = new Addresses();
            await addressList.GetFromSqlAsync(user, searchString);
            return addressList;
            
        }
    }
}
