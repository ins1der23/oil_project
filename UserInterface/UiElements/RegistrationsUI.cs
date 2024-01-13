using MenusAndChoices;
using Models;
using Service;
using UserInterface;

namespace Handbooks
{
    public class RegistrationsUI : StartLogic<BaseAddress, Registrations>
    {
        public static async Task<Registration> Start()
        {
            items = new Registrations();
            item = new Registration();

            Deleted = RegistrationText.deleted;
            DelCancel = RegistrationText.delCancel;
            SaveOptions = RegistrationText.saveOptions;
            ItemExist = RegistrationText.exist;
            ItemAdded = RegistrationText.added;
            ItemNotAdded = RegistrationText.notAdded;
            ItemChanged = RegistrationText.changed;
            ChangeCancel = RegistrationText.changeCancel;
            SearchString = RegistrationText.searchString;
            ItemsMenuName = RegistrationText.menuName;
            ItemChoosen = RegistrationText.choosen;
            ItemNotChoosen = RegistrationText.notChoosen;

            return (Registration)await StartLogic<BaseAddress, Registrations>.Start();
        }
    }
}