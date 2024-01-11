using MenusAndChoices;
using Models;
using Service;


namespace UserInterface;

public class SearchDistrictUI : SearchLogic<District, Districts>
{
    public static async Task<District> Start()
    {
        items = new Districts();
        item = new District();
        SearchString = DistrictText.searchString;
        item = await SearchLogic<District, Districts>.Start();
        return item;
    }
}
