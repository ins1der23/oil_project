using static InOut;
using static Menu;
public class Controller
{
    public static void Start()
    {
        while (true)
        {
            Menu mainMenu = new Menu(MenuText.mainMenu);
            ShowMenu(mainMenu);
            int choice = MenuChoice(mainMenu, MenuText.menuChoice);
            if (choice == 1)
            {
                while (true)
                {
                    Menu clientMenu = new Menu(MenuText.clientMenu);
                    ShowMenu(clientMenu);
                    choice = MenuChoice(clientMenu, MenuText.menuChoice);
                    if (choice == 6) break;
                }
            }
            else if (choice == 4) break;
        }
    }
}



