using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class IssuedControl
    {
        public static async Task<IssuedBy> Start()
        {
            var user = Settings.User;
            bool mainFlag = true;
            int choice;
            IssuedBy issuedBy = new();
            while (mainFlag)
            {
                issuedBy = await FindIssuedBy.Start();
                if (issuedBy.Id == 0)
                {
                    choice = MenuToChoice(IssuedByText.searchAgainOrAdd, invite: Text.choice);
                    switch (choice)
                    {
                        case 0: // Повторить поиск
                            break;
                        case 2: // Добавить орган, выдавший паспорт
                            var issuedByNew = await AddIssued.Start();
                            if (issuedByNew.Name != string.Empty) issuedBy = issuedByNew;
                            ShowString(IssuedByText.choosen);
                            await Task.Delay(1000);
                            mainFlag = false;
                            break;
                        case 3: // возврат в предыдущее меню
                            ShowString(IssuedByText.notChoosen);
                            await Task.Delay(1000);
                            mainFlag = false;
                            break;
                    }
                }
                else
                {
                    bool levelOneFlag = true;
                    while (levelOneFlag)
                    {
                        choice = MenuToChoice(IssuedByText.issuedChoices, issuedBy.Name, invite: Text.choice, noNull: true);
                        switch (choice)
                        {
                            case 1: // Выбрать
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: // Изменить
                                ShowString(IssuedByText.changed);
                                await Task.Delay(1000);
                                break;
                            case 3: //Удалить
                                levelOneFlag = false;
                                ShowString(IssuedByText.deleted);
                                await Task.Delay(1000);
                                ShowString(IssuedByText.returnToSearch);
                                await Task.Delay(1000);
                                break;
                            case 4: //Вернуться к поиску
                                levelOneFlag = false;
                                ShowString(IssuedByText.returnToSearch);
                                await Task.Delay(1000);
                                break;
                                case 5: //Вернуться в предыдущее меню
                                levelOneFlag = false;
                                mainFlag = false;
                                issuedBy = new();
                                ShowString(IssuedByText.notChoosen);
                                await Task.Delay(1000);
                                break;
                        }
                    }
                }
            }
            return issuedBy;


        }
    }
}