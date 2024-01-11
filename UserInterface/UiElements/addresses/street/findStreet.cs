using MenusAndChoices;
using Controller;
using Models;
using UserInterface;

namespace Handbooks

{
    public class FindStreet
    {
        public static async Task<Street> Start(City city)
        {
            var user = Settings.User;
            Streets streets = new();
            Street street = new()
            {
                CityId = city.Id
            };
            string search = GetString(StreetText.streetNameOrEmpty); // Найти улицу или оставить поле пустым для показа всех
            await streets.GetFromSqlAsync(street, search, false);
            bool flag = true;
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(streets.ToStringList(), StreetText.streets, CommonText.choiceOrEmpty);
                if (choice != 0) street = streets.GetFromList(choice);
                flag = false;
            }
            return street;
        }
    }
}