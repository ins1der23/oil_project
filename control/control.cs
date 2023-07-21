using System;
using static InOut;
using static Menu;
public class Control
{
    public static void Start()
    {
        DataBase db = new DataBase();
        db.AppendPosition(new Position(String.Empty));
        while (true)
        {
            Menu mainMenu = new Menu(MenuText.mainMenu);
            ShowMenu(mainMenu);
            bool mainFlag = true;
            int choice = MenuChoice(mainMenu, MenuText.menuChoice);
            switch (choice)
            {
                case 1: // clients
                    while (mainFlag)
                    {
                        Menu clientMenu = new Menu(MenuText.clientMenu);
                        ShowMenu(clientMenu);
                        choice = MenuChoice(clientMenu, MenuText.menuChoice);
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
                        Menu claimMenu = new Menu(MenuText.claimMenu);
                        ShowMenu(claimMenu);
                        choice = MenuChoice(claimMenu, MenuText.menuChoice);
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
                        Menu workerMenu = new Menu(MenuText.workerMenu);
                        ShowMenu(workerMenu);
                        choice = MenuChoice(workerMenu, MenuText.menuChoice);
                        switch (choice)
                        {
                            case 1:
                                db.AppendWorker(CreateWorker());
                                break;
                            case 2:
                                Console.WriteLine(db.SelectAllWorkers());
                                break;
                            case 4: mainFlag = false;
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



