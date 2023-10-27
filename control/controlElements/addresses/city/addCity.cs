using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class AddCity
    {
        public static async Task<City> Start()
        {
            var user = Settings.User;
            string name = GetString(CityText.cityName);
            City city = new()
            {
                Name = name
            };
            Cities cities = new();
            city = await cities.SaveGetId(user, city);
            await ShowString(CityText.cityAdded);
            District district = new()
            {
                Name = name,
                CityId = city.Id
            };
            Districts districts = new();
            district = await districts.SaveGetId(user, district);
            Location location = new()
            {
                Name = name,
                CityId = city.Id,
                DistrictId = district.Id
            };
            Locations locations = new();
            _ = await locations.SaveGetId(user, location);
            return city;
        }
    }
}