using MenusAndChoices;
using Models;
using Service;

namespace UserInterface;

public class ChangePassportUI : ChangeLogic<Passport, Passports>
{
    public static async Task<Passport> Start(Passport item)
    {
        items = new Passports();
        ItemChanged = PassportText.changed;
        ChangeCancel = PassportText.changeCancel;
        item = await ChangeLogic<Passport, Passports>.Start(item);
        return item;
    }
}