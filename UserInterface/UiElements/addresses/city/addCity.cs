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
            bool flag = true;
            while (flag)
            {
                await ShowString(city.Name, delay: 0);
                int choice = await MenuToChoice(CityText.saveOptions, string.Empty, CommonText.choice, clear: false, noNull: true);
                switch (choice)
                {
                    case 1: // Сохранить город
                        Cities cities = new();
                        bool exist = await cities.CheckExist(city);
                        if (exist) await ShowString(CityText.cityExist);
                        else
                        {
                            city = await cities.SaveGetId(city);
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
                        flag = false;
                        break;
                    case 2: // Изменить город
                        city = await ChangeCity.Start(city, toSql: false);
                        break;
                    case 3: // Не сохранять город
                        flag = false;
                        break;
                }
            }
            await ShowString(CityText.cityNotAdded);
            return new City();
        }
    }
}