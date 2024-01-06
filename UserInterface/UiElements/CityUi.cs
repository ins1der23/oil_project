using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    internal class CityUI : StartLogic<City, City, Cities>
    {
        public static async Task<City> Start()
        {
            items = new Cities();
            item = new City();
            cutOffBy = new City();
            Deleted = CityText.deleted;
            DelCancel = CityText.delCancel;
            SaveOptions = CityText.saveOptions;
            ItemExist = CityText.exist;
            ItemAdded = CityText.added;
            ItemNotAdded = CityText.notAdded;
            ItemChanged = CityText.changed;
            ChangeCancel = CityText.changeCancel;
            SearchString = CityText.searchString;
            ItemsMenuName = CityText.menuName;
            ItemChoosen = CityText.choosen;
            ItemNotChoosen = CityText.notChoosen;

            item = await StartLogic<City, City, Cities>.Start(changing: false, deleting: false);
            return item;
        }
    }
}