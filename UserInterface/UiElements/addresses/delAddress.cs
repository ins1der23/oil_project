using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    class DelAddress
    {
        public static async Task<Address> Start(Address address)
        {
            bool flag = true;
            int choice;
            while (flag)
            {
                await ShowString(AddrText.Summary(address), true, delay: 100);
                choice = await MenuToChoice(CommonText.yesOrNo, AddrText.delAddress, CommonText.choice, clear: false, noNull: true); // Точно удалить?
                switch (choice)
                {
                    case 1: // Да
                        Addresses addresses = new();
                        addresses.Append(address);
                        var user = Settings.User;
                        await addresses.DeleteSqlAsync(user);
                        await ShowString(AddrText.addressDeleted);
                        return new Address();
                    case 2: // Нет
                        flag = false;
                        await ShowString(AddrText.addressNotDeleted);
                        break;
                }
            }
            return address;
        }
    }
}