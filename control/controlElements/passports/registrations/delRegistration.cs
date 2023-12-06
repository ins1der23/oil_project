using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    class DelRegistration
    {
        public static async Task<Registration> Start(Registration registration)
        {
            bool flag = true;
            int choice;
            while (flag)
            {
                await ShowString(registration.Summary(), true, delay: 100);
                choice = await MenuToChoice(Text.yesOrNo, AddrText.delAddress, Text.choice, clear: false, noNull: true); // Точно удалить?
                switch (choice)
                {
                    case 1: // Да
                        Registrations registrations = new();
                        registrations.Append(registration);
                        var user = Settings.User;
                        await registrations.DeleteSqlAsync(user);
                        await ShowString(AddrText.addressDeleted);
                        return new Registration();
                    case 2: // Нет
                        flag = false;
                        await ShowString(AddrText.addressNotDeleted);
                        break;
                }
            }
            return registration;
        }
    }
}