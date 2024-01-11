using MenusAndChoices;
using Models;
using Service;


namespace UserInterface;

public class SearchCityUI : SearchLogic<City, Cities>
{
    public static async Task<City> Start()
    {
        items = new Cities();
        item = new City();
        SearchString = CityText.searchString;
        item = await SearchLogic<City, Cities>.Start();
        return item;
    }
}
