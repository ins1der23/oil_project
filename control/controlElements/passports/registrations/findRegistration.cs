using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class FindRegistration

    {
        public static async Task<Registration> Start()
        {
            var user = Settings.User;
            string search = GetString(RegistrationText.registrationSearch).PrepareToSearch();
            var registrations = new Registrations();
            await registrations.GetFromSqlAsync(user, search);
            bool flag = true;
            var registration = new Registration();
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(registrations.ToStringList(), RegistrationText.addressesFound, Text.choiceOrEmpty);
                if (choice != 0) registration = registrations.GetFromList(choice);
                flag = false;
            }
            return registration;
        }
    }
}
