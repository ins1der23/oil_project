using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    public class StreetsUI : StartLogic<Street, Streets>
    {
        public static async Task<Street> Start(object cutOffBy = null!)
        {
            items = new Streets();
            item = new Street();
            Deleted = StreetText.deleted;
            DelCancel = StreetText.delCancel;
            SaveOptions = StreetText.saveOptions;
            ItemExist = StreetText.exist;
            ItemAdded = StreetText.added;
            ItemNotAdded = StreetText.notAdded;
            ItemChanged = StreetText.changed;
            ChangeCancel = StreetText.changeCancel;
            SearchString = StreetText.searchString;
            ItemsMenuName = StreetText.menuName;
            ItemChoosen = StreetText.choosen;
            ItemNotChoosen = StreetText.notChoosen;

            return await StartLogic<Street, Streets>.Start(cutOffBy: cutOffBy);
        }
    }
}