using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;


namespace Handbooks
{
    public class FindAddress

    {
        public static async Task<Address> Start()
        {
            var user = Settings.user;
            var forSearch = InOut.GetString(AddrText.addressSearch).PrepareToSearch();
            var addressList = new Addresses();
            await addressList.GetFromSqlAsync(user, forSearch);
            bool flag = true;
            var address = new Address();
            int choice;
            while (flag)
            {
                choice = MenuToChoice(addressList.ToStringList(), AddrText.addressesFound, Text.choiceOrEmpty);
                if (choice != 0) address = addressList.GetFromList(choice);
                flag = false;
            }
            return address;
        }
    }
}
