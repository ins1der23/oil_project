using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class FindStreet
    {
        public static async Task<Street> Start(City city)
        {
            var user = Settings.User;
            Streets streets = new();
            string search = GetString(StreetText.streetNameOrEmpty); // Найти улицу или оставить поле пустым для показа всех
            await streets.GetFromSqlAsync(user, cityId: city.Id, search);
            bool flag = true;
            Street street = new();
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(streets.ToStringList(), StreetText.streets, Text.choiceOrEmpty);
                if (choice != 0) street = streets.GetFromList(choice);
                flag = false;
            }
            return street;
        }
    }
}