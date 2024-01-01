using MenusAndChoices;
using Models;

namespace Handbooks

{
    public class StartLocationUI
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
                    choice = await MenuToChoice(LocationText.searchAgain, invite: CommonText.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск микрорайона
                            await ShowString(CommonText.returnToSearch);
                            break;
                        case 2: // Не выбирать микрорайон
                            await ShowString(LocationText.notChoosen);
                            mainFlag = false;
                            break;
                    }
                }
                else
                {
                    bool levelOneFlag = true;
                    while (levelOneFlag)
                    {
                        choice = city.Id == 1 ? await MenuToChoice(CommonText.options, location.Name, invite: CommonText.choice, noNull: true) : 1;
                        switch (choice)
                        {
                            case 1: // Выбрать
                                await ShowString(LocationText.choosen);
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: // Изменить
                                break;
                            case 3: // Удалить
                                break;
                            case 4: // Вернуться к поиску микрорайона
                                levelOneFlag = false;
                                await ShowString(CommonText.returnToSearch);
                                break;
                            case 5: // Не выбирать микрорайон
                                levelOneFlag = false;
                                mainFlag = false;
                                location = new();
                                await ShowString(LocationText.notChoosen);
                                break;
                        }
                    }
                }
            }
            return location;
        }
    }
}
