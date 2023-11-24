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
                        case 1: // Вернуться к поиску микрорайона еще раз
                            break;
                        case 2: // Отменить добавление адреса и вернуться в предыдущее меню
                            await ShowString(AddrText.addressNotChoosen);
                            return new Address();
                    }
                }
                else flag = false;
            }
            flag = true;
            Street street = new();
            while (flag)
            {
                street = await StreetControl.Start(city);
                if (street.Id == 0)
                {
                    choice = await MenuToChoice(StreetText.tryAgain, StreetText.streetNotChoosen, Text.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Вернуться к поиску улицы еще раз
                            break;
                        case 2: // Отменить добавление адреса и вернуться в предыдущее меню
                            await ShowString(AddrText.addressNotChoosen);
                            return new Address();
                    }
                }
                else flag = false;
            }
            Address address = new()
            {
                CityId = city.Id,
                City = city,
                LocationId = location.Id,
                Location = location,
                DistrictId = location.DistrictId,
                District = location.District,
                StreetId = street.Id,
                Street = street,
                HouseNum = GetString(AddrText.houseNum)
            };
            flag = true;
            while (flag)
            {
                await ShowString(address.Summary(), delay: 0);
                choice = await MenuToChoice(AddrText.saveOptions, string.Empty, Text.choice, clear: false, noNull: true);
                switch (choice)
                {
                    case 1: // Сохранить адрес
                        Addresses addresses = new();
                        bool exist = await addresses.CheckExist(user, address);
                        if (exist) await ShowString(CityText.cityExist);
                        else
                        {
                            address = await addresses.SaveGetId(user, address);
                            await ShowString(AddrText.addressAdded);
                            flag = false;
                        }
                        break;
                    case 2: // Изменить адрес
                        address = await ChangeAddress.Start(address, toSql: false);
                        break;
                    case 3: // Не сохранять адрес
                        await ShowString(AddrText.addressNotAdded);
                        address = new();
                        flag = false;
                        break;
                }
            }
            return address;
        }
    }
}







