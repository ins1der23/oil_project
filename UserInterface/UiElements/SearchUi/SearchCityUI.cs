using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    internal class SearchCityUI : SearchLogic<City, City, Cities>
    {
        public static async Task<City> Start()
        {
            items = new Cities();
            item = new City();
            cutOffBy = new City();
            SearchString = CityText.searchString;
            item = await SearchLogic<City, City, Cities>.Start();
            return item;
        }
    }
}