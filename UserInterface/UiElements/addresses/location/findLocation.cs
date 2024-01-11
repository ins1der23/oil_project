using MenusAndChoices;
using Controller;
using Models;
using UserInterface;

namespace Handbooks

{
    public class FindLocation
    {
        public static async Task<Location> Start(City city)
        {
            var user = Settings.User;
            Locations locations = new();
            string search = city.Id == 1 ? GetString(LocationText.searchString) : city.Name; // Найти микрорайон или оставить поле пустым для показа всех
            await locations.GetFromSqlAsync(search:search);
            bool flag = true;
            Location location = new();
            int choice;
            while (flag)
            {
                choice = city.Id == 1 ? await MenuToChoice(locations.ToStringList(), LocationText.menuName, CommonText.choiceOrEmpty) : 1;
                if (choice != 0) location = locations.GetFromList(choice);
                flag = false;
            }
            return location;
        }
    }
}