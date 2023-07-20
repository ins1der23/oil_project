using static InOut;
using static Menu;
public class Control
{
    public static void Start()
    {
        while (true)
        {
            Menu mainMenu = new Menu(MenuText.mainMenu);
            ShowMenu(mainMenu);
            bool mainFlag = true;
            int choice = MenuChoice(mainMenu, MenuText.menuChoice);
            switch (choice)
            {
                case 1:
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
                case 2:
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
                case 5:
                    return;
            }

        }
    }
}



