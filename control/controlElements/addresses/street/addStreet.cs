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
            bool flag = true;
            while (flag)
            {
                await ShowString(street.Name, delay: 0);
                int choice = await MenuToChoice(StreetText.saveOptions, string.Empty, Text.choice, clear: false, noNull: true);
                switch (choice)
                {
                    case 1: // Сохранить улицу
                        Streets streets = new();
                        bool exist = await streets.CheckExist(user, street);
                        if (exist) await ShowString(StreetText.streetExist);
                        else
                        {
                            street = await streets.SaveGetId(user, street);
                            await ShowString(StreetText.streetAdded);
                            return street;
                        }
                        flag = false;
                        break;
                    case 2: // Изменить улицу
                        await ChangeStreet.Start(street, toSql: false);
                        break;
                    case 3: // Не сохранять улицу
                        flag = false;
                        break;
                }
            }
            await ShowString(StreetText.streetNotAdded);
            return new Street();
        }
    }
}