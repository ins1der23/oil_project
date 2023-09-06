using static InOut;
using static MenuText;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace AddressBook
{
    public class AddAddress
    {
        public static async Task Start()
        {
            var user = MainControl.user;
            var addressToAdd = new Address();
            var cityList = new Cities(); // Города
            string searchString;
            bool flag = true;
            int choice;
            while (flag)
            {
                searchString = InOut.GetString(MenuText.cityName); // Найти город
                await cityList.GetFromSqlAsync(user, searchString);
                if (cityList.IsEmpty)
                {
                    choice = MenuToChoice(MenuText.searchAgainOrAdd, MenuText.notFound); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Добавить
                            var streetToAdd = new City();
                            streetToAdd.Name = GetString(MenuText.inputName);
                            cityList.Clear();
                            cityList.Append(streetToAdd);
                            await cityList.AddSqlAsync(user);
                            ShowString(MenuText.added);
                            break;
                        case 3: // Выход
                            return;
                    }
                }
                else flag = false;
            }
            choice = MenuToChoice(cityList.ToStringList(), MenuText.choice);
            addressToAdd.City = cityList.GetFromList(choice);
            addressToAdd.CityId = addressToAdd.City.Id;
            var districtList = new Districts(); // Районы
            await districtList.GetFromSqlAsync(user);
            if (!districtList.IsEmpty)
            {
                flag = true;
                while (flag)
                {
                    searchString = InOut.GetString(MenuText.districtName); // Найти район
                    await districtList.GetFromSqlAsync(MainControl.user, searchString);
                    if (districtList.IsEmpty)
                    {
                        choice = MenuToChoice(MenuText.searchAgain, MenuText.notFound); // Не найдено
                        switch (choice)
                        {
                            case 1: // Повторить поиск
                                break;
                            case 2: // Выход
                                return;
                        }
                    }
                    else flag = false;
                }
                choice = MenuToChoice(districtList.ToStringList(), MenuText.choice);
                addressToAdd.District = districtList.GetFromList(choice);
                addressToAdd.DistrictId = addressToAdd.District.Id;
            }
            var locationList = new Locations(); // Микрорайоны
            await locationList.GetFromSqlAsync(user);
            if (!locationList.IsEmpty)
            {
                flag = true;
                while (flag)
                {
                    searchString = InOut.GetString(MenuText.locationName); // Найти микрорайон
                    await locationList.GetFromSqlAsync(user, searchString);
                    if (locationList.IsEmpty)
                    {
                        choice = MenuToChoice(MenuText.searchAgain, MenuText.notFound); // Не найдено
                        switch (choice)
                        {
                            case 1: // Повторить поиск
                                break;
                            case 2: // Выход
                                return;
                        }
                    }
                    else flag = false;
                }
                choice = MenuToChoice(locationList.ToStringList(), MenuText.choice);
                addressToAdd.Location = locationList.GetFromList(choice);
                addressToAdd.LocationId = addressToAdd.Location.Id;
            }
            var streetList = new Streets(); // Улицы
            flag = true;
            while (flag)
            {
                searchString = InOut.GetString(MenuText.streetName); // Найти улицу
                await streetList.GetFromSqlAsync(user, searchString);
                if (streetList.IsEmpty)
                {
                    choice = MenuToChoice(MenuText.searchAgainOrAdd, MenuText.notFound); // Не найдено
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Добавить
                            var streetToAdd = new Street();
                            streetToAdd.Name = GetString(MenuText.inputName);
                            streetList.Clear();
                            streetList.Append(streetToAdd);
                            await streetList.AddSqlAsync(user);
                            ShowString(MenuText.added);
                            break;
                        case 3: // Выход
                            return;
                    }
                }
                else flag = false;
            }
            choice = MenuToChoice(streetList.ToStringList(), MenuText.choice);
            addressToAdd.Street = streetList.GetFromList(choice);
            addressToAdd.StreetId = addressToAdd.Street.Id;
            addressToAdd.HouseNum = GetString(MenuText.houseNum);
            var addressList = new Addresses();
            addressList.Append(addressToAdd);
            await addressList.AddSqlAsync(user);
            ShowString(MenuText.added);
        }
    }
}







