using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class FindCity
    {
        public static async Task<City> Start()
        {
            var user = Settings.User;
            Cities cities = new();
            string search = GetString(CityText.cityNameOrEmpty); // Найти город или оставить поле пустым для показа всех
            await cities.GetFromSqlAsync(user, search);
            bool flag = true;
            City city = new();
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(cities.ToStringList(), CityText.cities, Text.choiceOrEmpty);
                if (choice != 0) city = cities.GetFromList(choice);
                flag = false;
            }
            return city;
        }
    }
}