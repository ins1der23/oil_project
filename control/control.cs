using static InOut;
using static Menu;
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
                Menu tempMenu;
                Menu mainMenu = new Menu(MenuText.menuNames[0], MenuText.mainMenu);
                ShowMenu(mainMenu);
                bool mainFlag = true;
                int choice = MenuChoice(mainMenu, MenuText.menuChoice);
                int tempIndex = 0;
                switch (choice)
                {
                    case 1: // Клиенты
                        while (mainFlag)
                        {
                            tempMenu = new Menu(MenuText.menuNames[1], MenuText.clientMenu);
                            ShowMenu(tempMenu);
                            choice = MenuChoice(tempMenu, MenuText.menuChoice);
                            switch (choice)
                            {
                                case 6:
                                    mainFlag = false;
                                    break;
                            }
                        }
                        break;
                    case 2: //Заявки
                        while (mainFlag)
                        {
                            tempMenu = new Menu(MenuText.menuNames[2], MenuText.claimMenu);
                            ShowMenu(tempMenu);
                            choice = MenuChoice(tempMenu, MenuText.menuChoice);
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
                            tempMenu = new Menu(MenuText.menuNames[3], MenuText.workerMenu);
                            ShowMenu(tempMenu);
                            choice = MenuChoice(tempMenu, MenuText.menuChoice);
                            switch (choice)
                            {
                                case 1: // Добавить сотрудника
                                    Worker workerToAdd = Worker.Create();
                                    ShowString(MenuText.setPosition);
                                    tempMenu = new Menu(String.Empty, MenuText.yesMenu);
                                    ShowMenu(tempMenu);
                                    choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                    switch (choice)
                                    {
                                        case 1: // Назначить должность
                                            await positionsList.GetFromSqlAsync(user);
                                            tempMenu = new Menu(MenuText.menuNames[4], positionsList.ToStringList());
                                            ShowMenu(tempMenu);
                                            choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                            workerToAdd.SetPosition(choice);
                                            break;
                                    }
                                    workersList.AppendWorker(workerToAdd);
                                    await workersList.AddSqlAsync(user);
                                    break;
                                case 2: // Показать всех сотрудников
                                    await workersList.GetFromSqlAsync(user);
                                    ShowStringList(workersList.ToStringList());
                                    tempMenu = new Menu(String.Empty, MenuText.choiceMenu);
                                    ShowMenu(tempMenu);
                                    choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать запись
                                            tempMenu = new Menu(String.Empty, workersList.ToStringList());
                                            ShowMenu(tempMenu);
                                            choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                            Worker? workerToChange = workersList.GetFromList(choice);
                                            ShowString(workerToChange.ToString());
                                            tempMenu = new Menu(String.Empty, MenuText.changeMenu);
                                            ShowMenu(tempMenu);
                                            choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                            switch (choice)
                                            {
                                                case 1: // Изменить запись
                                                    workerToChange.Change();
                                                    await positionsList.GetFromSqlAsync(user);
                                                    tempMenu = new Menu(MenuText.menuNames[4], positionsList.ToStringList());
                                                    ShowMenu(tempMenu);
                                                    choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                                    workerToChange.SetPosition(choice);
                                                    await workersList.ChangeSqlAsync(user);
                                                    break;
                                                case 2: // Удалить запись
                                                    workersList.DeleteWorker(workerToChange.Id);
                                                    workersList.Clear();
                                                    workersList.AppendWorker(workerToChange);
                                                    await workersList.DeleteSqlAsync(user);
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 3: // Найти сотрудника
                                    string searchName = InOut.GetString(MenuText.workerName);
                                    // List<Worker> searchList = db.WorkersSearch(searchName);
                                    // List<string> searchListString = db.ListWorkerSearch(searchList);
                                    // ShowStringList(searchListString);
                                    tempMenu = new Menu(String.Empty, MenuText.choiceMenu);
                                    ShowMenu(tempMenu);
                                    choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                    switch (choice)
                                    {
                                        case 1: // Выбрать запись
                                            // tempMenu = new Menu(String.Empty, searchListString);
                                            ShowMenu(tempMenu);
                                            tempIndex = MenuChoice(tempMenu, MenuText.menuChoice);
                                            // Worker toChange = db.WorkerSearchReturn(tempIndex, searchList);
                                            tempMenu = new Menu(String.Empty, MenuText.changeMenu);
                                            ShowMenu(tempMenu);
                                            choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                            switch (choice)
                                            {
                                                case 1: // Изменить запись
                                                    // ControlInterface.ChangeWorker(toChange);
                                                    tempMenu = new Menu(MenuText.menuNames[4], db.SelectAllPositions());
                                                    ShowMenu(tempMenu);
                                                    choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                                    // toChange.SetPosition(toChange, choice);
                                                    // ShowString(db.StringWorker(toChange.workerId));
                                                    break;
                                                case 2: // Удалить запись
                                                    // db.DeleteWorker(toChange.workerId);
                                                    // ShowStringList(db.ListWorkers());
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                                case 4:
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



