using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class AddressControl
    {
        public static async Task<Address> Start()
        {
            return await AddAddress.Start();
        }
    }
}