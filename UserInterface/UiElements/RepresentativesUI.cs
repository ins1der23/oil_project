using MenusAndChoices;
using Models;
using Service;


namespace UserInterface;

public class RepresentativesUI : StartLogic<Human, Representatives>
{
    public static async Task<Representative> Start(object cutOffBy = null!)
    {
        items = new Representatives();
        item = new Representative();

        Deleted = RepresentativeText.deleted;
        DelCancel = RepresentativeText.delCancel;
        SaveOptions = RepresentativeText.saveOptions;
        ItemExist = RepresentativeText.exist;
        ItemAdded = RepresentativeText.added;
        ItemNotAdded = RepresentativeText.notAdded;
        ItemChanged = RepresentativeText.changed;
        ChangeCancel = RepresentativeText.changeCancel;
        SearchString = RepresentativeText.searchString;
        ItemsMenuName = RepresentativeText.menuName;
        ItemChoosen = RepresentativeText.choosen;
        ItemNotChoosen = RepresentativeText.notChoosen;

        return (Representative)await StartLogic<Human, Representatives>.Start(cutOffBy: cutOffBy);
    }
}