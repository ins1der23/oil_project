using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    internal class LocationsUI : StartLogic<Location, Locations>
    {
        public static async Task<Location> Start(object cutOffBy = null!)
        {
            items = new Locations();
            item = new Location();

            Deleted = LocationText.deleted;
            DelCancel = LocationText.delCancel;
            SaveOptions = LocationText.saveOptions;
            ItemExist = LocationText.exist;
            ItemAdded = LocationText.added;
            ItemNotAdded = LocationText.notAdded;
            ItemChanged = LocationText.changed;
            ChangeCancel = LocationText.changeCancel;
            SearchString = LocationText.searchString;
            ItemsMenuName = LocationText.menuName;
            ItemChoosen = LocationText.choosen;
            ItemNotChoosen = LocationText.notChoosen;

            return await StartLogic<Location, Locations>.Start(cutOffBy: cutOffBy);
        }
    }
}