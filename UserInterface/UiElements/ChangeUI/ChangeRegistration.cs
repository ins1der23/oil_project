using MenusAndChoices;
using Models;
using Service;

namespace UserInterface;

public class ChangeRegistrationUI : ChangeLogic<BaseAddress, Registrations>
{
    public static async Task<Registration> Start(Registration item)
    {
        items = new Registrations();
        ItemChanged = RegistrationText.changed;
        ChangeCancel = RegistrationText.changeCancel;
        item = (Registration)await ChangeLogic<BaseAddress, Registrations>.Start(item);
        return item;
    }
}