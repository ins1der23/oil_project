using static InOut;
using Models;
using Connection;
using MySql.Data.MySqlClient;
using Dapper;
public class Control
{
    public static async Task Start()
    {
        DataBase db = new DataBase();
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
                                    ShowStringList(workersList.ToStringList());
                                    tempMenu = new Menu(MenuText.addOrchoose);
                                    tempMenu.ShowMenu();
                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                    switch (choice)
                                    {
                                        case 1: // Добавить 
                                            Worker workerToAdd = Worker.Create();
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
                                            workersList.AppendWorker(workerToAdd);
                                            await workersList.AddSqlAsync(user);
                                            break;
                                        case 2: // Выбрать 
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
                                                case 1: // Изменить 
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    tempMenu = new Menu(positionsList.ToStringList(), MenuText.menuNames[4]);
                                                    tempMenu.ShowMenu();
                                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                                    workerToChange.SetPosition(choice);
                                                    ShowString(workerToChange.ToString());
                                                    await workersList.ChangeSqlAsync(user);
                                                    break;
                                                case 2: // Удалить 
                                                    workersList.Clear();
                                                    workersList.AppendWorker(workerToChange);
                                                    await workersList.DeleteSqlAsync(user);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 2: // Найти 
                                    string searchName = InOut.GetString(MenuText.workerName);
                                    await workersList.GetFromSqlAsync(user, searchName);
                                    ShowStringList(workersList.ToStringList());
                                    tempMenu = new Menu(MenuText.choose);
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
                                                case 1: // Изменить
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    tempMenu = new Menu(positionsList.ToStringList(), MenuText.menuNames[4]);
                                                    tempMenu.ShowMenu();
                                                    choice = tempMenu.MenuChoice(MenuText.choice);
                                                    workerToChange.SetPosition(choice);
                                                    ShowString(workerToChange.ToString());
                                                    await workersList.ChangeSqlAsync(user);
                                                    break;
                                                case 2: // Удалить 
                                                    workersList.Clear();
                                                    workersList.AppendWorker(workerToChange);
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
                        var toAdd = new Address();
                        toAdd.Create();
                        Console.WriteLine(toAdd);
                        break;
                    case 6:
                        user.Close();
                        return;
                }
            }
        }
    }
}



