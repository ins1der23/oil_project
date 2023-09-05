using static InOut;
using static MenuText;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Controller
{
    public class MainControl
    {
        public static DBConnection user
        {
            get => DBConnection.Instance("localhost", "oilproject");
        }

        public static async Task Start()
        {
            int connectCount = 0;
            while (!user.IsConnect && connectCount < 3)
            {
                // user = DBConnection.Instance("localhost", "oilproject");
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
                                choice = MenuToChoice(MenuText.yesOrNo, MenuText.addAddress); //Добавить адрес
                                switch (choice)
                                {
                                    case 1:
                                        await AddressControl.Start(1);
                                        break;
                                    case 3:
                                        mainFlag = false;
                                        break;
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
}

