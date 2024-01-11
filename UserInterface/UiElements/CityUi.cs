using MenusAndChoices;
using Models;
using Service;
using UserInterface;

namespace Handbooks
{
    public class CityUI : StartLogic<City, Cities>
    {
        public static async Task<City> Start()
        {
            items = new Cities();
            item = new City();
            
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

            item = await StartLogic<City, Cities>.Start(changing: false, deleting: false);
            return item;
        }
    }
}