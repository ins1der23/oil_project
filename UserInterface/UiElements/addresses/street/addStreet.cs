using MenusAndChoices;
using Controller;
using Models;
using UserInterface;

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
                await ShowString(street.Name, clear: true, delay: 0);
                int choice = await MenuToChoice(StreetText.saveOptions, string.Empty, CommonText.choice, clear: false, noNull: true);
                switch (choice)
                {
                    case 1: // Сохранить улицу
                        Streets streets = new();
                        bool exist = await streets.CheckExist(street);
                        if (exist) await ShowString(StreetText.streetExist);
                        else
                        {
                            street = await streets.SaveGetId(street);
                            await ShowString(StreetText.streetAdded);
                            return street;
                        }
                        flag = false;
                        break;
                    case 2: // Изменить улицу
                        street = await ChangeStreet.Start(street, toSql: false);
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