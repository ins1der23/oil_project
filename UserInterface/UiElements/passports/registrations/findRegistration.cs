using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class FindRegistration

    {
        public static async Task<Registration> Start()
        {
            string search = GetString(RegistrationText.registrationSearch).PrepareToSearch();
            var registrations = new Registrations<Client>();
            await registrations.GetFromSqlAsync(search:search);
            bool flag = true;
            var registration = new Registration();
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(registrations.ToStringList(), RegistrationText.addressesFound, CommonText.choiceOrEmpty);
                if (choice != 0) registration = registrations.GetFromList(choice);
                flag = false;
            }
            return registration;
        }
    }
}
