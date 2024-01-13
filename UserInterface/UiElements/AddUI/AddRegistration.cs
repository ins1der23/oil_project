using MenusAndChoices;
using Models;
using Service;

namespace UserInterface;

public class AddRegistrationUI : AddLogic<BaseAddress, Registrations>
{
    public static new async Task<Registration> Start()
    {
        items = new Registrations();
        item = new Registration();
        SaveOptions = RegistrationText.saveOptions;
        ItemExist = RegistrationText.exist;
        ItemAdded = RegistrationText.added;
        ItemNotAdded = RegistrationText.notAdded;
        item = await AddLogic<BaseAddress, Registrations>.Start();
        return (Registration)item;
    }
}
