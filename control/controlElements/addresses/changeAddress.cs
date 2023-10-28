using MenusAndChoices;
using Controller;
using Models;

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
            while (flag)
            {
                await ShowString(address.Summary());
                int choice = await MenuToChoice(address.ChangeOptions(), string.Empty, Text.choice, clear: false);

                if (city.Id == 1)
                {
                    switch (choice)
                    {
                        case 1:
                            city = await CityControl.Start();
                            Locations locations = new();
                            await locations.GetFromSqlAsync(user, cityId: city.Id, city.Name);
                            location = locations.GetFromList();
                            district = location.District;
                            break;
                        case 2:
                            location = await LocationControl.Start(city);
                            district = location.District;
                            break;
                        case 3:
                            street = await StreetControl.Start(city);
                            break;
                        case 4:
                            houseNum = GetString(AddrText.changeHouseNum);
                            break;
                        case 5:
                            flag = false;
                            break;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case 1:
                            city = await CityControl.Start();
                            Locations locations = new();
                            await locations.GetFromSqlAsync(user, cityId: city.Id, city.Name);
                            location = locations.GetFromList();
                            district = location.District;
                            break;
                        case 2:
                            street = await StreetControl.Start(city);
                            break;
                        case 3:
                            houseNum = GetString(AddrText.changeHouseNum);
                            break;
                        case 4:
                            flag = false;
                            break;
                    }
                }
                address.Change(city, district, location, street, houseNum);
            }
            await ShowString(address.Summary(), clear: false);
            await ShowString(address.Summary(), clear: false);
            if (address.ShortString == addressOld.ShortString)
            {
                await ShowString("NOT CHANGED", clear: false);
            }

            else await ShowString("CHANGED", clear: false);
            Console.ReadLine();
            return address;
        }
    }
}