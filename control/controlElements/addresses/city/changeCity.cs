using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class ChangeCity
    {
        public static async Task<City> Start(City city)
        {
            var cityNew = (City)city.Clone();
            await ShowString(city.Name, clear: true);
            string name = GetString(CityText.changeName, clear: false);
            if (name == string.Empty)
            {
                await ShowString(CityText.changeCancel);
                return city;
            }
            cityNew.Change(name);
            await ShowString(cityNew.Name, clear: true);
            int choice = await MenuToChoice(Text.yesOrNo, CityText.changeConfirm, Text.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                Cities cities = new();
                cities.Append(city);
                var user = Settings.User;
                await cities.ChangeSqlAsync(user);
                await ShowString(CityText.changed);
                Districts districts = new();
                await districts.GetFromSqlAsync(user, city.Name);

                
                return cityNew;
            }
            await ShowString(CityText.changeCancel);
            return city;
        }
    }
}