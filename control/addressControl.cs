using static InOut;
using static MenuText;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Controller
{
    public class AddressControl
    {
        public static async Task Start(int number)
        {

            int choice = number;
            var user = MainControl.user;
            bool flag = true;
            switch (choice)
            {
                case 1:
                    var addressToAdd = new Address();
                    var addressList = new Addresses();
                    await addressList.GetFromSqlAsync(user);
                    Console.WriteLine(addressList);
                    var cityList = new Cities();
                    string toFind;
                    while (flag)
                    {
                        toFind = InOut.GetString(MenuText.cityName);
                        await cityList.GetFromSqlAsync(user, toFind);
                        if (cityList.IsEmpty)
                        {
                            ShowString(MenuText.notFound);
                            choice = MenuToChoice(MenuText.yesOrNo, MenuText.addSome); // Добавить город
                            switch (choice)
                            {
                                case 1: // Да
                                    var cityToAdd = new City();
                                    cityToAdd.Name = GetString(MenuText.inputName);
                                    cityList.Clear();
                                    cityList.Append(cityToAdd);
                                    await cityList.AddSqlAsync(user);
                                    choice = MenuToChoice(MenuText.yesOrNo, MenuText.chooseSome); // Выбрать
                                    switch (choice)
                                    {
                                        case 1:
                                            await cityList.GetFromSqlAsync(user, cityToAdd.Name);
                                            cityToAdd = cityList.GetByName(cityToAdd.Name);
                                            addressToAdd.City = cityToAdd;
                                            addressToAdd.CityId = cityToAdd.Id;
                                            break;
                                        case 2: // Нет
                                            break;
                                        case 3: // Выход
                                            return;
                                    }
                                    break;
                                case 2: // Нет
                                    break;
                                case 3: // Выход
                                    return;
                            }
                        }
                        else
                        {
                            cityList.ToStringList().ShowStringList();
                            Menu tempMenu = new Menu(MenuText.addOrchoose);
                            tempMenu.ShowMenu();
                            choice = tempMenu.MenuChoice(MenuText.choice);
                            switch (choice)
                            {
                                case 1: // Выбрать город
                                    choice = MenuToChoice(cityList.ToStringList());
                                    addressToAdd.City = cityList.GetFromList(choice);
                                    addressToAdd.CityId = addressToAdd.City.Id;
                                    break;
                                case 2: // Добавить город
                                    var cityToAdd = new City();
                                    cityToAdd.Name = GetString(MenuText.inputName);
                                    cityList.Clear();
                                    cityList.Append(cityToAdd);
                                    await cityList.AddSqlAsync(user);
                                    choice = MenuToChoice(MenuText.yesOrNo, MenuText.chooseSome); // Выбрать
                                    switch (choice)
                                    {
                                        case 1:
                                            await cityList.GetFromSqlAsync(user, cityToAdd.Name);
                                            cityToAdd = cityList.GetByName(cityToAdd.Name);
                                            addressToAdd.City = cityToAdd;
                                            addressToAdd.CityId = cityToAdd.Id;
                                            break;
                                        case 2: // Возврат
                                            break;
                                        case 3: // Выход
                                            return;
                                    }
                                    break;
                                case 3: // Возврат
                                    break;
                                case 4: // Выход
                                    return;
                            }
                        }
                        flag = addressToAdd.CityId == 0 ? true : false;
                    }

                    if (addressToAdd.CityId == 1) // Если Екатеринбург
                    {
                        toFind = InOut.GetString(MenuText.districtName); // Районы
                        var districtList = new Districts();
                        await districtList.GetFromSqlAsync(MainControl.user, toFind);
                        if (districtList.IsEmpty) ShowString(MenuText.notFound);
                        else
                        {
                            choice = MenuToChoice(districtList.ToStringList(), MenuText.choice);
                            addressToAdd.District = districtList.GetFromList(choice);
                            addressToAdd.DistrictId = addressToAdd.District.Id;
                        }
                        toFind = InOut.GetString(MenuText.locationName); // Микрорайоны
                        var locationList = new Locations();
                        await locationList.GetFromSqlAsync(MainControl.user, toFind);
                        if (locationList.IsEmpty)
                        {
                            ShowString(MenuText.notFound);
                        }
                        else
                        {
                            choice = MenuToChoice(locationList.ToStringList(), MenuText.choice);
                            addressToAdd.Location = locationList.GetFromList(choice);
                            addressToAdd.LocationId = addressToAdd.Location.Id;
                        }
                    }

                    flag = true;
                    while (flag)
                    {
                        toFind = InOut.GetString(MenuText.streetName); // Улицы
                        var streetList = new Streets();
                        await streetList.GetFromSqlAsync(MainControl.user, toFind, addressToAdd.CityId);
                        streetList.ToStringList().ShowStringList();
                        if (streetList.IsEmpty)
                        {
                            ShowString(MenuText.notFound);
                            choice = MenuToChoice(MenuText.yesOrNo, MenuText.addSome);
                            switch (choice)
                            {
                                case 1: // Добавить улицу
                                    var streetToAdd = new Street();
                                    streetToAdd.Name = GetString(MenuText.inputName);
                                    streetToAdd.CityId = addressToAdd.City.Id;
                                    streetList.Clear();
                                    streetList.Append(streetToAdd);
                                    await streetList.AddSqlAsync(user);
                                    choice = MenuToChoice(MenuText.yesOrNo, MenuText.chooseSome); // Выбрать
                                    switch (choice)
                                    {
                                        case 1:
                                            await streetList.GetFromSqlAsync(user, streetToAdd.Name, streetToAdd.CityId);
                                            streetToAdd = streetList.GetByName(streetToAdd.Name);
                                            addressToAdd.Street = streetToAdd;
                                            addressToAdd.StreetId = streetToAdd.Id;
                                            break;
                                        case 2: // Возврат
                                            break;
                                        case 3: // Выход
                                            return;
                                    }
                                    break;
                                case 2: // Возврат
                                    break;
                                case 3: // Выход
                                    return;
                            }
                        }
                        else
                        {
                            choice = MenuToChoice(MenuText.addOrchoose);
                            switch (choice)
                            {
                                case 1: // Выбрать улицу
                                    choice = MenuToChoice(streetList.ToStringList());
                                    addressToAdd.Street = streetList.GetFromList(choice);
                                    addressToAdd.StreetId = addressToAdd.Street.Id;
                                    break;
                                case 2: // Добавить улицу
                                    var streetToAdd = new Street();
                                    streetToAdd.Name = GetString(MenuText.inputName);
                                    streetToAdd.CityId = addressToAdd.City.Id;
                                    streetList.Clear();
                                    streetList.Append(streetToAdd);
                                    await streetList.AddSqlAsync(MainControl.user);
                                    choice = MenuToChoice(MenuText.yesOrNo, MenuText.chooseSome); // Выбрать
                                    switch (choice)
                                    {
                                        case 1: // Да
                                            await streetList.GetFromSqlAsync(user, streetToAdd.Name, streetToAdd.CityId);
                                            streetToAdd = streetList.GetByName(streetToAdd.Name);
                                            addressToAdd.Street = streetToAdd;
                                            addressToAdd.StreetId = streetToAdd.Id;
                                            break;
                                        case 2: // Возврат
                                            break;
                                        case 3: // Выход
                                            return;
                                    }
                                    break;
                                case 4:
                                    return;
                            }
                        }
                        flag = addressToAdd.StreetId == 0 ? true : false;
                    }
                    addressToAdd.HouseNum = GetString(MenuText.houseNum);
                    addressList.Append(addressToAdd);
                    await addressList.AddSqlAsync(user);
                    Console.WriteLine(addressList);
                    break;
            }
        }
    }
}






