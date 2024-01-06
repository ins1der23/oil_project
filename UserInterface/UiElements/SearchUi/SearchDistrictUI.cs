using MenusAndChoices;
using Models;
using Service;

namespace Handbooks
{
    internal class SearchDistrictUI : SearchLogic<District, City, Districts>
    {
        public static async Task<District> Start()
        {
            items = new Districts();
            item = new District();
            SearchString = DistrictText.searchString;
            item = await SearchLogic<District, City, Districts>.Start(cutOff: true);
            return item;
        }
    }
}