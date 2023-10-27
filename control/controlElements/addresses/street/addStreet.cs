using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class AddStreet
    {
        public static async Task<Street> Start(City city)
        {
            var user = Settings.User;
            string name = GetString(StreetText.streetName);
            Street street = new()
            {
                Name = name,
                CityId = city.Id
            };
            Streets streets = new();
            street = await streets.SaveGetId(user, street);
            await ShowString(StreetText.streetAdded);
            return street;
        }
    }
}