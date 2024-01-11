using MenusAndChoices;
using Models;
using Service;

namespace UserInterface;

public class AddPassportUI : AddLogic<Passport, Passports>
{
    public static new async Task<Passport> Start()
    {
        items = new Passports();
        item = new Passport();
        SaveOptions = PassportText.saveOptions;
        ItemExist = PassportText.exist;
        ItemAdded = PassportText.added;
        ItemNotAdded = PassportText.notAdded;
        item = await AddLogic<Passport, Passports>.Start();
        return item;
    }
}
