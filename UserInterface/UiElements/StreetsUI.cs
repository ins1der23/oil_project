using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    internal class StreetsUI : StartLogic<Street, City, Streets>
    {
        public static async Task<Street> Start(bool cutOff = false, bool city = false)
        {
            items = new Streets();
            item = new Street();

            if (city) cutOffBy = await CityUI.Start();
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

            return await StartLogic<Street, City, Streets>.Start(cutOff: cutOff);
        }
    }
}