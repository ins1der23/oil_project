using static InOut;
using static MenuText;
using Models;
using Connection;
using MySql.Data.MySqlClient;
using Dapper;
public class Control
{
    public static async Task Start()
    {
        DBConnection user = DBConnection.Instance("localhost", "oilproject");
        int connectCount = 0;
        while (!user.IsConnect && connectCount < 3)
        {
            user = DBConnection.Instance("localhost", "oilproject");
            await user.ConnectAsync();
            connectCount++;
        }

        if (user.IsConnect)
        {
            Workers workersList = new Workers();
            Positions positionsList = new Positions();
            var toFind = string.Empty;
            user.Close();
            while (true)
            {
                Menu tempMenu = new Menu(MenuText.mainMenu, MenuText.menuNames[0]);
                tempMenu.ShowMenu();
                bool mainFlag = true;
                int choice = tempMenu.MenuChoice(MenuText.choice);
                switch (choice)
                {
                    case 1: // Клиенты
                        while (mainFlag)
                        {
                            tempMenu = new Menu(MenuText.showOrFind, MenuText.menuNames[1]);
                            tempMenu.ShowMenu();
                            choice = tempMenu.MenuChoice(MenuText.choice);
                            switch (choice)
                            {
                                case 1:  // Показать все
                                    tempMenu = new Menu(MenuText.addOrchoose, MenuText.menuNames[1]);
                                    tempMenu.ShowMenu();
                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать

                                            break;
                                        case 2: // Добавить
                                            break;
                                    }
                                    break;
                                case 2: // Найти
                                    tempMenu = new Menu(MenuText.choose, MenuText.menuNames[1]);
                                    tempMenu.ShowMenu();
                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать
                                            break;
                                    }
                                    break;
                                case 3: // Возврат
                                    mainFlag = false;
                                    break;
                            }
                        }
                        break;
                    case 2: //Заявки
                        while (mainFlag)
                        {
                            tempMenu = new Menu(MenuText.claimMenu, MenuText.menuNames[2]);
                            tempMenu.ShowMenu();
                            choice = tempMenu.MenuChoice(MenuText.choice);
                            switch (choice)
                            {
                                case 3:
                                    mainFlag = false;
                                    break;
                            }
                        }
                        break;
                    case 4: // Сотрудники
                        while (mainFlag)
                        {
                            tempMenu = new Menu(MenuText.showOrFind, MenuText.menuNames[3]);
                            tempMenu.ShowMenu();
                            choice = tempMenu.MenuChoice(MenuText.choice);
                            switch (choice)
                            {
                                case 1: // Показать все
                                    await workersList.GetFromSqlAsync(user);
                                    workersList.ToStringList().ShowStringList();
                                    tempMenu = new Menu(MenuText.addOrchoose);
                                    tempMenu.ShowMenu();
                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать 
                                            tempMenu = new Menu(workersList.ToStringList(), MenuText.menuNames[3]);
                                            tempMenu.ShowMenu();
                                            choice = tempMenu.MenuChoice(MenuText.choice);
                                            Worker? workerToChange = workersList.GetFromList(choice);
                                            ShowString(workerToChange.ToString());
                                            tempMenu = new Menu(MenuText.changeOrDelete);
                                            tempMenu.ShowMenu();
                                            choice = tempMenu.MenuChoice(MenuText.choice);
                                            switch (choice)
                                            {
                                                case 1: // Изменить работника
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    tempMenu = new Menu(positionsList.ToStringList(), MenuText.menuNames[4]);
                                                    tempMenu.ShowMenu();
                                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                                    workerToChange.SetPosition(choice);
                                                    ShowString(workerToChange.ToString());
                                                    await workersList.ChangeSqlAsync(user);
                                                    break;
                                                case 2: // Удалить работника
                                                    workersList.Clear();
                                                    workersList.Append(workerToChange);
                                                    await workersList.DeleteSqlAsync(user);
                                                    break;
                                            }
                                            break;
                                        case 2: // Добавить работника
                                            var workerToAdd = Worker.Create();
                                            ShowString(MenuText.setPosition);
                                            tempMenu = new Menu(MenuText.yesOrNo);
                                            tempMenu.ShowMenu();
                                            choice = tempMenu.MenuChoice(MenuText.choice);
                                            switch (choice)
                                            {
                                                case 1: // Назначить должность
                                                    await positionsList.GetFromSqlAsync(user);
                                                    tempMenu = new Menu(positionsList.ToStringList(), MenuText.menuNames[4]);
                                                    tempMenu.ShowMenu();
                                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                                    workerToAdd.SetPosition(choice);
                                                    break;
                                            }
                                            workersList.Clear();
                                            workersList.Append(workerToAdd);
                                            await workersList.AddSqlAsync(user);
                                            break;
                                    }
                                    break;
                                case 2: // Найти работника
                                    toFind = InOut.GetString(MenuText.workerName);
                                    await workersList.GetFromSqlAsync(user, toFind);
                                    workersList.ToStringList().ShowStringList();
                                    tempMenu = new Menu(MenuText.choose);
                                    tempMenu.ShowMenu();
                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать работника
                                            tempMenu = new Menu(workersList.ToStringList(), MenuText.menuNames[3]);
                                            tempMenu.ShowMenu();
                                            choice = tempMenu.MenuChoice(MenuText.choice);
                                            Worker? workerToChange = workersList.GetFromList(choice);
                                            ShowString(workerToChange.ToString());
                                            tempMenu = new Menu(MenuText.changeOrDelete);
                                            tempMenu.ShowMenu();
                                            choice = tempMenu.MenuChoice(MenuText.choice);
                                            switch (choice)
                                            {
                                                case 1: // Изменить работника
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    tempMenu = new Menu(positionsList.ToStringList(), MenuText.menuNames[4]);
                                                    tempMenu.ShowMenu();
                                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                                    workerToChange.SetPosition(choice);
                                                    ShowString(workerToChange.ToString());
                                                    await workersList.ChangeSqlAsync(user);
                                                    break;
                                                case 2: // Удалить работника
                                                    workersList.Clear();
                                                    workersList.Append(workerToChange);
                                                    await workersList.DeleteSqlAsync(user);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 3: // Возврат
                                    mainFlag = false;
                                    break;
                            }
                        }
                        break;
                    case 5: // Тест
                        mainFlag = true;
                        while (mainFlag)
                        {
                            choice = MenuToChoice(MenuText.yesOrNo, MenuText.addAddress);
                            if (choice == 2)
                            {
                                mainFlag = false;
                                break;
                            }
                            var addressToAdd = new Address();
                            toFind = InOut.GetString(MenuText.cityName);
                            var cityList = new Cities();
                            await cityList.GetFromSqlAsync(user, toFind);
                            if (cityList.IsEmpty)
                            {
                                ShowString(MenuText.notFound);
                                choice = MenuToChoice(MenuText.yesOrNo, MenuText.addSome);
                                switch (choice)
                                {
                                    case 1: // Добавить город
                                        var cityToAdd = new City();
                                        break;
                                    case 2: // выход
                                        break;
                                }
                            }
                            else
                            {
                                cityList.ToStringList().ShowStringList();
                                tempMenu = new Menu(MenuText.addOrchoose);
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
                                        break;
                                }
                            }
                            if (!cityList.IsEmpty)
                            {
                                if (addressToAdd.CityId == 1) // Если Екатеринбург
                                {
                                    toFind = InOut.GetString(MenuText.districtName); // Районы
                                    var districtList = new Districts();
                                    await districtList.GetFromSqlAsync(user, toFind);
                                    if (districtList.IsEmpty)
                                    {
                                        ShowString(MenuText.notFound);
                                    }
                                    else
                                    {
                                        choice = MenuToChoice(districtList.ToStringList(), MenuText.choice);
                                        addressToAdd.District = districtList.GetFromList(choice);
                                        addressToAdd.DistrictId = addressToAdd.District.Id;
                                    }
                                    toFind = InOut.GetString(MenuText.locationName); // Микрорайоны
                                    var locationList = new Locations();
                                    await locationList.GetFromSqlAsync(user, toFind);
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
                                toFind = InOut.GetString(MenuText.streetName); // Улицы
                                var streetList = new Streets();
                                await streetList.GetFromSqlAsync(user, toFind);
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
                                            addressToAdd.Street = streetToAdd;
                                            addressToAdd.StreetId = streetToAdd.Id;
                                            Console.WriteLine(addressToAdd);
                                            break;
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
                                            await streetList.AddSqlAsync(user);
                                            addressToAdd.Street = streetToAdd;
                                            addressToAdd.StreetId = streetToAdd.Id;
                                            break;
                                    }
                                }
                                addressToAdd.HouseNum = GetString(MenuText.houseNum);
                                Console.WriteLine(addressToAdd);
                            }
                        }
                        break;
                    case 6:
                        user.Close();
                        return;
                }
            }
        }
    }
}



