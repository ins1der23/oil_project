using MenusAndChoices;
using Models;

namespace Handbooks

{
    public class StreetControl
    {
        public static async Task<Street> Start(City city)
        {
            bool mainFlag = true;
            int choice;
            Street street = new();
            while (mainFlag)
            {
                street = await FindStreet.Start(city);
                if (street.Id == 0)
                {
                    choice = await MenuToChoice(StreetText.searchAgainOrAddStreet, invite: Text.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск улицы
                            await ShowString(Text.returnToSearch);
                            break;
                        case 2: // Добавить улицу
                            var streetNew = await AddStreet.Start(city);
                            if (streetNew.Name != string.Empty)
                            {
                                street = streetNew;
                                await ShowString(StreetText.streetChoosen);
                            }
                            else await ShowString(StreetText.streetNotChoosen);
                            mainFlag = false;
                            break;
                        case 3: // Не выбирать улицу
                            await ShowString(StreetText.streetNotChoosen);
                            mainFlag = false;
                            break;
                    }
                }
                else
                {
                    bool levelOneFlag = true;
                    while (levelOneFlag)
                    {
                        choice = await MenuToChoice(StreetText.options, street.Name, invite: Text.choice, noNull: true);
                        switch (choice)
                        {
                            case 1: // Выбрать
                                await ShowString(StreetText.streetChoosen);
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: //Изменить
                                street = await ChangeStreet.Start(street);
                                break;
                            case 3: // Вернуться к поиску улицы
                                levelOneFlag = false;
                                await ShowString(Text.returnToSearch);
                                break;
                            case 4: // Не выбирать улицу
                                levelOneFlag = false;
                                mainFlag = false;
                                street = new();
                                await ShowString(StreetText.streetNotChoosen);
                                break;
                        }
                    }
                }
            }
            return street;
        }
    }
}
