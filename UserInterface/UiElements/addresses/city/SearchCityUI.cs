using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    internal class SearchCityUI : SearchLogic<City, City, Cities<City>>
    {
        public static async Task<City> Start()
        {
            items = new Cities<City>();
            item = new City();
            parameter = new City();
            SearchString = CityText.searchString;
            item = await SearchLogic<City, City, Cities<City>>.Start();
            return item;
        }
    }
}