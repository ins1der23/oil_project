using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;

namespace Handbooks

{
    public class AddAddress
    {
        public static async Task<Address> Start(bool forPassport = false)
        {
            var user = Settings.User;
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
                    Console.WriteLine(addressToAdd.City.Name);
                    Console.WriteLine(addressToAdd.CityId);
                    Console.ReadLine();
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
                            string name = GetString(Text.inputName);
                            cityToAdd.Name = name;
                            cityToAdd = await cityList.SaveGetId(user, cityToAdd);
                            ShowString(AddrText.cityAdded);
                            await Task.Delay(1000);
                            addressToAdd.City = cityToAdd;
                            addressToAdd.CityId = addressToAdd.City.Id;
                            Console.WriteLine(addressToAdd.CityId);
                            Console.ReadLine();

                            ShowString(AddrText.cityChoosen);
                            await Task.Delay(1000);
                            District district = new()
                            {
                                Name = name,
                                CityId = cityToAdd.Id
                            };
                            var districtList = new Districts();
                            district = await districtList.SaveGetId(user, district);
                            Location location = new()
                            {
                                Name = name,
                                CityId = cityToAdd.Id,
                                DistrictId = district.Id
                            };
                            Locations locations = new();
                            locations.Append(location);
                            await locations.AddSqlAsync(user);
                            flag = false;
                            break;
                        case 3: // Выход
                            ShowString(AddrText.addressNotAdded);
                            await Task.Delay(1000);
                            return new Address();
                    }
                }
            }
            var locationList = new Locations(); // Микрорайоны
            if (!forPassport)
            {

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
            else
            {
                await locationList.GetFromSqlAsync(user, addressToAdd.City.Name, addressToAdd.CityId);
                addressToAdd.Location = locationList.GetFromList();
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
                            var streetToAdd = new Street
                            {
                                Name = GetString(Text.inputName),
                                CityId = addressToAdd.CityId
                            };
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

            addressToAdd.HouseNum = GetString(AddrText.houseNum);
            if (forPassport)
                addressToAdd.FlatNum += GetString(AddrText.flatNum);
            var addressList = new Addresses();
            addressToAdd = await addressList.SaveGetId(user, addressToAdd);
            ShowString(AddrText.addressAdded);
            await Task.Delay(1000);
            return addressToAdd;
        }
    }
}







