using MenusAndChoices;
using Models;
using Org.BouncyCastle.Asn1.Cmp;
using Service;

namespace Handbooks
{
    internal class StreetsUI : StartLogic<Street, City, Streets<City>>
    {
        public static async Task<Street> Start(bool cutOff = false)
        {
            items = new Streets<City>();
            item = new Street();

            if (cutOff) parameter = await CityUI.Start();
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

            return await StartLogic<Street, City, Streets<City>>.Start(cutOff: cutOff);
        }
    }
}