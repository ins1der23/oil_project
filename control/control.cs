using System;
using static InOut;
using static Menu;
using static Worker;
public class Control
{
    public static void Start()
    {
        DataBase db = new DataBase();
        db.AppendPosition(new Position("Директор"));
        db.AppendPosition(new Position("Начальник производства"));
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
                case 1: // clients
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
                case 2: //claims
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
                                Worker worker = CreateWorker();
                                db.AppendWorker(worker);
                                ShowString(MenuText.setPosition);
                                tempMenu = new Menu(String.Empty, MenuText.yesMenu);
                                ShowMenu(tempMenu);
                                choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                switch (choice)
                                {
                                    case 1: // Назначить должность
                                        tempMenu = new Menu(MenuText.menuNames[4], db.SelectAllPositions());
                                        ShowMenu(tempMenu);
                                        tempIndex = MenuChoice(tempMenu, MenuText.menuChoice);
                                        worker.SetPosition(worker, tempIndex);
                                        break;
                                }
                                break;

                            case 2: // Показать всех сотрудников
                                ShowStringList(db.ListWorkers());
                                tempMenu = new Menu(String.Empty, MenuText.choiceMenu);
                                ShowMenu(tempMenu);
                                choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                switch (choice)
                                {
                                    case 1: // Выбрать запись
                                        tempMenu = new Menu(String.Empty, db.ListWorkers());
                                        ShowMenu(tempMenu);
                                        tempIndex = MenuChoice(tempMenu, MenuText.menuChoice);
                                        ShowString(db.StringWorker(tempIndex));
                                        tempMenu = new Menu(String.Empty, MenuText.changeMenu);
                                        ShowMenu(tempMenu);
                                        choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                        switch (choice)
                                        {
                                            case 1: // Изменить запись
                                                Worker toChange = db.ReturnWorker(tempIndex);
                                                ChangeWorker(toChange);
                                                tempMenu = new Menu(MenuText.menuNames[4], db.SelectAllPositions());
                                                ShowMenu(tempMenu);
                                                choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                                toChange.SetPosition(toChange, choice);
                                                ShowString(db.StringWorker(tempIndex));
                                                break;
                                            case 2: // Удалить запись
                                                db.DeleteWorker(tempIndex);
                                                ShowStringList(db.ListWorkers());
                                                break;
                                        }
                                        break;
                                }
                                break;
                            case 3: // Найти сотрудника
                                string searchName = InOut.GetString("Введите имя сотруника");
                                List<Worker> searchList = db.WorkersSearch(searchName);
                                List<string> searchListString = db.ListWorkerSearch(searchList);
                                ShowStringList(searchListString);
                                tempMenu = new Menu(String.Empty, MenuText.choiceMenu);
                                ShowMenu(tempMenu);
                                choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                switch (choice)
                                {
                                    case 1: // Выбрать запись
                                    tempMenu = new Menu(String.Empty, searchListString);
                                    ShowMenu(tempMenu);
                                    tempIndex = MenuChoice(tempMenu, MenuText.menuChoice);
                                    Worker toChange = db.WorkerSearchReturn(tempIndex, searchList);
                                    Console.WriteLine(toChange);
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
                    return;
            }

        }
    }

    private static Worker CreateWorker()
    {
        string name = GetString(MenuText.workerName);
        string surname = GetString(MenuText.workerSurname);
        DateTime birth = GetDate(MenuText.workerBirth);
        return new Worker(name, surname, birth);
    }

    private static void ChangeWorker(Worker worker)
    {
        string name = GetString(MenuText.workerName);
        string surname = GetString(MenuText.workerSurname);
        DateTime birth = GetDate(MenuText.workerBirth);
        worker.ChangeFields(worker, name, surname, birth);

    }
}



