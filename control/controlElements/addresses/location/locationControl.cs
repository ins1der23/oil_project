using MenusAndChoices;
using Models;

namespace Handbooks

{
    public class LocationControl
    {
        public static async Task<Location> Start(City city)
        {
            bool mainFlag = true;
            int choice;
            Location location = new();
            while (mainFlag)
            {
                location = await FindLocation.Start(city);
                if (location.Id == 0)
                {
                    choice = await MenuToChoice(LocationText.searchAgain, invite: Text.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            await ShowString(Text.returnToSearch);
                            break;
                        case 2: // возврат в предыдущее меню
                            await ShowString(LocationText.locationNotChoosen);
                            mainFlag = false;
                            break;
                    }
                }
                else
                {
                    bool levelOneFlag = true;
                    while (levelOneFlag)
                    {
                        choice = city.Id == 1 ? await MenuToChoice(LocationText.options, location.Name, invite: Text.choice, noNull: true) : 1;
                        switch (choice)
                        {
                            case 1: // Выбрать
                                await ShowString(LocationText.locationChoosen);
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: // Вернуться к поиску
                                levelOneFlag = false;
                                await ShowString(Text.returnToSearch);
                                break;
                            case 3: // Вернуться в предыдущее меню
                                levelOneFlag = false;
                                mainFlag = false;
                                location = new();
                                await ShowString(LocationText.locationNotChoosen);
                                break;
                        }
                    }
                }
            }
            return location;
        }
    }
}
