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

            string searchString;
            bool flag = true;
            int choice;
            City city = await CityControl.Start();
            if (city.Id == 0)
            {
                await ShowString(AddrText.addressNotChoosen);
                return new Address();
            }
            Location location = new();
            while (flag)
            {
                location = await LocationControl.Start(city);
                if (location.Id == 0)
                {
                    choice = await MenuToChoice(LocationText.tryAgain, LocationText.locationNotChoosen, Text.choice, noNull: true);
                    switch (choice)
                    {
                        case 1:
                            break;
                        case 2:
                            await ShowString(AddrText.addressNotChoosen);
                            return new Address();
                    }
                }
                flag = false;
            }

            Address address = new()
            {
                CityId = city.Id,
                City = city,
                LocationId = location.Id,
                Location = location,
                DistrictId = location.DistrictId,

            };

            var streetList = new Streets(); // Улицы
            flag = true;
            while (flag)
            {
                searchString = InOut.GetString(Text.streetName); // Найти улицу
                await streetList.GetFromSqlAsync(user, searchString, address.CityId);
                choice = await MenuToChoice(streetList.ToStringList(), AddrText.streets, Text.choiceOrEmpty);
                if (choice != 0)
                {
                    address.Street = streetList.GetFromList(choice);
                    address.StreetId = address.Street.Id;
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
                                CityId = address.CityId
                            };
                            streetToAdd = await streetList.SaveGetId(user, streetToAdd);
                            await ShowString(AddrText.streetAdded);
                            address.Street = streetToAdd;
                            address.StreetId = streetToAdd.Id;
                            await ShowString(AddrText.streetChoosen);
                            flag = false;
                            break;
                        case 3: // Выход
                            await ShowString(AddrText.addressNotAdded);
                            return new Address();
                    }
                }
            }

            address.HouseNum = GetString(AddrText.houseNum);
            var addressList = new Addresses();
            address = await addressList.SaveGetId(user, address);
            await ShowString(AddrText.addressAdded);
            return address;
        }
    }
}







