using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class ChangeCity
    {
        public static async Task<City> Start(City city)
        {
            var cityOld = (City)city.Clone();
            await ShowString(city.Name, clear: true, delay: 300);
            string name = GetString(CityText.changeName, clear: false);
            if (name == string.Empty)
            {
                await ShowString(CityText.changeCancel);
                return city;
            }
            city.Change(name);
            await ShowString(city.Name, clear: true);
            int choice = await MenuToChoice(Text.yesOrNo, CityText.changeConfirm, Text.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                Cities cities = new();
                cities.Append(city);
                var user = Settings.User;
                await cities.ChangeSqlAsync(user);
                await ShowString(CityText.changed);
                Districts districts = new();
                await districts.GetFromSqlAsync(user, cityId: city.Id, cityOld.Name);
                District district = districts.GetFromList();
                district.Name = name;
                _ = await districts.SaveChanges(user, district);
                Locations locations = new();
                await locations.GetFromSqlAsync(user, cityId: city.Id, cityOld.Name);
                Location location = locations.GetFromList();
                location.Name = name;
                _ = await locations.SaveChanges(user, location);
                return city;
            }
            await ShowString(CityText.changeCancel);
            return cityOld;
        }
    }
}