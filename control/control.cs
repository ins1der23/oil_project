using System;
using static InOut;
using static Menu;
using static Worker;
public class Control
{
    public static void Start()
    {
        DataBase db = new DataBase();
        // db.AppendPosition(new Position("Не назначено"));
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
                case 4: // workers
                    while (mainFlag)
                    {
                        tempMenu = new Menu(MenuText.menuNames[3], MenuText.workerMenu);
                        ShowMenu(tempMenu);
                        choice = MenuChoice(tempMenu, MenuText.menuChoice);
                        switch (choice)
                        {
                            case 1:
                                Worker worker = CreateWorker();
                                db.AppendWorker(worker);
                                ShowString(MenuText.setPosition);
                                tempMenu = new Menu(String.Empty, MenuText.yesMenu);
                                ShowMenu(tempMenu);
                                choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                switch (choice)
                                {
                                    case 1:
                                        tempMenu = new Menu(MenuText.menuNames[4], db.SelectAllPositions());
                                        ShowMenu(tempMenu);
                                        tempIndex = MenuChoice(tempMenu, MenuText.menuChoice) - 1;
                                        worker.SetPosition(worker, choice - 1);
                                        break;
                                }
                                break;

                            case 2:
                                ShowStringList(db.SelectAllWorkers());
                                tempMenu = new Menu(String.Empty, MenuText.choiceMenu);
                                ShowMenu(tempMenu);
                                choice = MenuChoice(tempMenu, MenuText.menuChoice);
                                switch (choice)
                                {
                                    case 1:
                                        tempMenu = new Menu(String.Empty, db.SelectAllWorkers());
                                        ShowMenu(tempMenu);
                                        tempIndex = MenuChoice(tempMenu, MenuText.menuChoice) - 1;
                                        ShowString(db.SelectWorker(tempIndex));
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
}



