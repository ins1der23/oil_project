using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;

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
                choice = MenuToChoice(cityList.ToStringList(), AddrText.cities, Text.choiceOrEmpty);
                if (choice != 0)
                {
                    addressToAdd.City = cityList.GetFromList(choice);
                    addressToAdd.CityId = addressToAdd.City.Id;
                    ShowString(AddrText.cityChoosen);
                    await Task.Delay(1000);
                    flag = false;
                }
                else
                {
                    choice = MenuToChoice(AddrText.searchAgainOrAddCity, AddrText.cities, Text.choice, noNull: true); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск города
                            break;
                        case 2: // Добавить город
                            var cityToAdd = new City();
                            cityToAdd.Name = GetString(Text.inputName);
                            cityToAdd = await cityList.SaveGetId(user, cityToAdd);
                            ShowString(AddrText.cityAdded);
                            await Task.Delay(1000);
                            addressToAdd.City = cityToAdd;
                            addressToAdd.CityId = addressToAdd.City.Id;
                            ShowString(AddrText.cityChoosen);
                            await Task.Delay(1000);
                            break;
                        case 3: // Выход
                            ShowString(AddrText.addressNotAdded);
                            await Task.Delay(1000);
                            return new Address();
                    }
                }
            }
            /*if (addressToAdd.CityId == 1) // Если Екатеринбург*/
            {
                var locationList = new Locations(); // Микрорайоны
                flag = true;
                while (flag)
                {
                    searchString = InOut.GetString(Text.locationName); // Найти микрорайон
                    await locationList.GetFromSqlAsync(user, searchString, addressToAdd.CityId);
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
            }

            var streetList = new Streets(); // Улицы
            flag = true;
            while (flag)
            {
                searchString = InOut.GetString(Text.streetName); // Найти улицу
                await streetList.GetFromSqlAsync(user, searchString, addressToAdd.CityId);
                choice = MenuToChoice(streetList.ToStringList(), AddrText.streets, Text.choiceOrEmpty);
                if (choice != 0)
                {
                    addressToAdd.Street = streetList.GetFromList(choice);
                    addressToAdd.StreetId = addressToAdd.Street.Id;
                    ShowString(AddrText.streetChoosen);
                    await Task.Delay(1000);
                    flag = false;
                }
                else
                {
                    choice = MenuToChoice(AddrText.searchAgainOrAddStreet, AddrText.streets, Text.choice, noNull: true); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Добавить
                            var streetToAdd = new Street();
                            streetToAdd.Name = GetString(Text.inputName);
                            streetToAdd.CityId = addressToAdd.CityId;
                            streetToAdd = await streetList.SaveGetId(user, streetToAdd);
                            ShowString(AddrText.streetAdded);
                            await Task.Delay(1000);
                            addressToAdd.Street = streetToAdd;
                            addressToAdd.StreetId = streetToAdd.Id;
                            ShowString(AddrText.streetChoosen);
                            await Task.Delay(1000);
                            flag = false;
                            break;
                        case 3: // Выход
                            ShowString(AddrText.addressNotAdded);
                            await Task.Delay(1000);
                            return new Address();
                    }
                }
            }

            addressToAdd.HouseNum = GetString(Text.houseNum);
            var addressList = new Addresses();
            addressToAdd = await addressList.SaveGetId(user, addressToAdd);
            ShowString(AddrText.addressAdded);
            await Task.Delay(1000);
            return addressToAdd;
        }
    }
}







