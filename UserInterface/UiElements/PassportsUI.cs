using MenusAndChoices;
using Models;
using Service;


namespace UserInterface;

public class PassportsUI : StartLogic<Passport, Passports>
{
    public static async Task<Passport> Start(object cutOffBy = null!)
    {
        items = new Passports();
        item = new Passport();

        Deleted = PassportText.deleted;
        DelCancel = PassportText.delCancel;
        SaveOptions = PassportText.saveOptions;
        ItemExist = PassportText.exist;
        ItemAdded = PassportText.added;
        ItemNotAdded = PassportText.notAdded;
        ItemChanged = PassportText.changed;
        ChangeCancel = PassportText.changeCancel;
        SearchString = PassportText.searchString;
        ItemsMenuName = PassportText.menuName;
        ItemChoosen = PassportText.choosen;
        ItemNotChoosen = PassportText.notChoosen;

        return await StartLogic<Passport, Passports>.Start(cutOffBy: cutOffBy);
    }
}
