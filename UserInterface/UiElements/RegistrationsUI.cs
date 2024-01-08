using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    internal class RegistrationsUI : StartLogic<Registration, Registrations>
    {
        public static async Task<Registration> Start(object cutOffBy = null!)
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

            return await StartLogic<Registration, Registrations>.Start(cutOffBy:cutOffBy);
        }
    }
}