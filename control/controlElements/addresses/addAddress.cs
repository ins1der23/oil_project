using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class AddAddress
    {
        public static async Task<Address> Start()
        {
            var user = Settings.User;
            var addressToAdd = new Address();
            var cityList = new Cities(); // Города
            string searchString;
            bool flag = true;
            int choice;
            while (flag)
            {
                searchString = InOut.GetString(CityText.cityNameOrEmpty); // Найти город
                await cityList.GetFromSqlAsync(user, searchString);
                choice = await MenuToChoice(cityList.ToStringList(), CityText.cities, Text.choiceOrEmpty);
                if (choice != 0)
                {
                    addressToAdd.City = cityList.GetFromList(choice);
                    addressToAdd.CityId = addressToAdd.City.Id;
                    await ShowString(CityText.cityChoosen);
                    flag = false;
                }
                else
                {
                    choice = await MenuToChoice(CityText.searchAgainOrAddCity, CityText.cities, Text.choice, noNull: true); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск города
                            break;
                        case 2: // Добавить город
                            var cityToAdd = new City();
                            string name = GetString(Text.inputName);
                            cityToAdd.Name = name;
                            cityToAdd = await cityList.SaveGetId(user, cityToAdd);
                            await ShowString(CityText.cityAdded);
                            addressToAdd.City = cityToAdd;
                            addressToAdd.CityId = addressToAdd.City.Id;
                            await ShowString(CityText.cityChoosen);
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
                            await ShowString(AddrText.addressNotAdded);
                            return new Address();
                    }
                }
            }
            var locationList = new Locations(); // Микрорайоны
            flag = true;
            while (flag)
            {
                searchString = InOut.GetString(Text.locationName); // Найти микрорайон
                await locationList.GetFromSqlAsync(user, searchString, addressToAdd.CityId);
                if (locationList.IsEmpty)
                {
                    choice = await MenuToChoice(Text.searchAgain, Text.notFound, Text.choice); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Выход
                            await ShowString(AddrText.addressNotAdded);
                            return new Address();
                    }
                }
                else flag = false;
            }
            choice = await MenuToChoice(locationList.ToStringList(), AddrText.locations, Text.choice, noNull: true);
            addressToAdd.Location = locationList.GetFromList(choice);
            addressToAdd.LocationId = addressToAdd.Location.Id;
            addressToAdd.DistrictId = addressToAdd.Location.DistrictId;

            var streetList = new Streets(); // Улицы
            flag = true;
            while (flag)
            {
                searchString = InOut.GetString(Text.streetName); // Найти улицу
                await streetList.GetFromSqlAsync(user, searchString, addressToAdd.CityId);
                choice = await MenuToChoice(streetList.ToStringList(), AddrText.streets, Text.choiceOrEmpty);
                if (choice != 0)
                {
                    addressToAdd.Street = streetList.GetFromList(choice);
                    addressToAdd.StreetId = addressToAdd.Street.Id;
                    await ShowString(AddrText.streetChoosen);
                    flag = false;
                }
                else
                {
                    choice = await MenuToChoice(AddrText.searchAgainOrAddStreet, AddrText.streets, Text.choice, noNull: true); // Не найдено
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
                            await ShowString(AddrText.streetAdded);
                            addressToAdd.Street = streetToAdd;
                            addressToAdd.StreetId = streetToAdd.Id;
                            await ShowString(AddrText.streetChoosen);
                            flag = false;
                            break;
                        case 3: // Выход
                            await ShowString(AddrText.addressNotAdded);
                            return new Address();
                    }
                }
            }

            addressToAdd.HouseNum = GetString(AddrText.houseNum);
            var addressList = new Addresses();
            addressToAdd = await addressList.SaveGetId(user, addressToAdd);
            await ShowString(AddrText.addressAdded);
            return addressToAdd;
        }
    }
}







