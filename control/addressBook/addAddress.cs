using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Handbooks

{
    public class AddAddress
    {
        public static async Task<Address> Start()
        {
            var user = Settings.user;
            var addressToAdd = new Address();
            var cityList = new Cities(); // Города
            string searchString;
            bool flag = true;
            int choice;
            while (flag)
            {
                searchString = InOut.GetString(AddrText.cityName); // Найти город
                await cityList.GetFromSqlAsync(user, searchString);
                if (cityList.IsEmpty)
                {
                    choice = MenuToChoice(Text.searchAgainOrAdd, Text.notFound, Text.choice); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Добавить
                            var cityToAdd = new City();
                            cityToAdd.Name = GetString(Text.inputName);
                            cityList.Clear();
                            cityList.Append(cityToAdd);
                            await cityList.AddSqlAsync(user);
                            ShowString(AddrText.cityAdded);
                            await Task.Delay(1000);
                            break;
                        case 3: // Выход
                            ShowString(AddrText.addressNotAdded);
                            await Task.Delay(1000);
                            return new Address();
                    }
                }
                else flag = false;
            }
            choice = MenuToChoice(cityList.ToStringList(), AddrText.cities, Text.choice, noNull: true);
            addressToAdd.City = cityList.GetFromList(choice);
            addressToAdd.CityId = addressToAdd.City.Id;

            var locationList = new Locations(); // Микрорайоны
            flag = true;
            while (flag)
            {
                searchString = InOut.GetString(Text.locationName); // Найти микрорайон
                await locationList.GetFromSqlAsync(user, searchString);
                if (locationList.IsEmpty)
                {
                    choice = MenuToChoice(Text.searchAgain, Text.notFound, Text.choice); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Выход
                            ShowString(AddrText.addressNotAdded);
                            await Task.Delay(1000);
                            return new Address();
                    }
                }
                else flag = false;
            }
            choice = MenuToChoice(locationList.ToStringList(), AddrText.locations, Text.choice, noNull: true);
            addressToAdd.Location = locationList.GetFromList(choice);
            addressToAdd.LocationId = addressToAdd.Location.Id;
            addressToAdd.DistrictId = addressToAdd.Location.DistrictId;

            var streetList = new Streets(); // Улицы
            flag = true;
            while (flag)
            {
                searchString = InOut.GetString(Text.streetName); // Найти улицу
                await streetList.GetFromSqlAsync(user, searchString);
                if (streetList.IsEmpty)
                {
                    choice = MenuToChoice(Text.searchAgainOrAdd, Text.notFound); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Добавить
                            var streetToAdd = new Street();
                            streetToAdd.Name = GetString(Text.inputName);
                            streetList.Clear();
                            streetList.Append(streetToAdd);
                            await streetList.AddSqlAsync(user);
                            ShowString(Text.added);
                            break;
                        case 3: // Выход
                            ShowString(AddrText.addressNotAdded);
                            await Task.Delay(1000);
                            return new Address();
                    }
                }
                else flag = false;
            }
            choice = MenuToChoice(streetList.ToStringList(), AddrText.streets, Text.choice, noNull: true);
            addressToAdd.Street = streetList.GetFromList(choice);
            addressToAdd.StreetId = addressToAdd.Street.Id;
            addressToAdd.HouseNum = GetString(Text.houseNum);
            var addressList = new Addresses();
            addressToAdd = await addressList.SaveGetId(user, addressToAdd);
            ShowString(AddrText.addressAdded);
            await Task.Delay(1000);
            return addressToAdd;
        }
    }
}







