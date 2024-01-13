using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class FindAddress

    {
        public static async Task<Address> Start()
        {
            var user = Settings.User;
            string search = GetString(AddressText.addressSearch).PrepareToSearch();
            var addressList = new Addresses();
            await addressList.GetFromSqlAsync(user, search);
            bool flag = true;
            var address = new Address();
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(addressList.ToStringList(), AddressText.addressesFound, CommonText.choiceOrEmpty);
                if (choice != 0) address = addressList.GetFromList(choice);
                flag = false;
            }
            return address;
        }
    }
}
