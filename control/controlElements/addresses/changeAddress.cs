using MenusAndChoices;
using Controller;
using Models;
using System.Xml.Serialization;

namespace Handbooks

{
    public class ChangeAddress
    {
        public static async Task<Address> Start(Address address, bool toSql = true)
        {
            bool flag = true;
            var addressOld = (Address)address.Clone();
            City city = address.City;
            District district = address.District;
            Location location = address.Location;
            Street street = address.Street;
            string houseNum = string.Empty;
            var user = Settings.User;
            int choice;
            while (flag)
            {
                await ShowString(address.Summary(), delay: 0);
                choice = await MenuToChoice(address.ChangeOptions(), string.Empty, Text.choice, clear: false);
                if (city.Id == 1)
                {
                    switch (choice)
                    {
                        case 1: // Выбрать другой город
                            city = await CityControl.Start();
                            if (city.Id != 0)
                            {
                                Locations locations = new();
                                await locations.GetFromSqlAsync(user, cityId: city.Id, city.Name);
                                location = locations.GetFromList();
                                district = location.District;
                            }
                            break;
                        case 2: // Выбрать другой микрорайон
                            location = await LocationControl.Start(city);
                            district = location.District;
                            break;
                        case 3: // Выбрать другую улицу
                            street = await StreetControl.Start(city);
                            break;
                        case 4: // Изменить номер дома
                            houseNum = GetString(AddrText.changeHouseNum);
                            break;
                        case 5: // Вернуться в предыдущее меню
                            flag = false;
                            break;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case 1: // Выбрать другой город
                            city = await CityControl.Start();
                            if (city.Id != 0)
                            {
                                Locations locations = new();
                                await locations.GetFromSqlAsync(user, cityId: city.Id, city.Name);
                                location = locations.GetFromList();
                                district = location.District;
                            }
                            break;
                        case 2: // Выбрать другую улицу
                            street = await StreetControl.Start(city);
                            break;
                        case 3: // Изменить номер дома
                            houseNum = GetString(AddrText.changeHouseNum);
                            break;
                        case 4: // Вернуться в предыдущее меню
                            flag = false;
                            break;
                    }
                }
                address.Change(city, district, location, street, houseNum);
            }
            await ShowString(address.Summary(), delay: 100);
            if (address.SearchString != addressOld.SearchString)
            {
                choice = await MenuToChoice(Text.yesOrNo, AddrText.confirmChanges, Text.choice, false);
                if (choice == 1)
                {
                    if (toSql)
                    {
                        Addresses addresses = new();
                        address = await addresses.SaveChanges(user, address);
                    }
                    await ShowString(AddrText.addressChanged);
                    return address;
                }
            }
            await ShowString(AddrText.addressNotChanged);
            return addressOld;
        }
    }
}
