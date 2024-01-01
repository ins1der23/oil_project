using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class ChangeCity
    {
        public static async Task<City> Start(City city, bool toSql = true)
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
            if (city.Name != cityOld.Name)
            {
                int choice = await MenuToChoice(CommonText.yesOrNo, CityText.changeConfirm, CommonText.choice, clear: false, noNull: true);
                if (choice == 1)
                {

                    var user = Settings.User;
                    Cities cities = new();
                    bool exist = await cities.CheckExist(city);
                    if (exist) await ShowString(CityText.cityExist);
                    else
                    {
                        await ShowString(CityText.changed);
                        if (toSql)
                        {
                            city = await cities.SaveChanges(city);
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
                        }
                        return city;
                    }
                }
            }
            await ShowString(CityText.changeCancel);
            return cityOld;
        }
    }
}