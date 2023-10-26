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
            bool mainFlag = true;
            int choice;
            IssuedBy issuedBy = new();
            while (mainFlag)
            {
                issuedBy = await FindIssuedBy.Start();
                if (issuedBy.Id == 0)
                {
                    choice = await MenuToChoice(IssuedByText.searchAgainOrAdd, invite: Text.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Добавить орган, выдавший паспорт
                            var issuedByNew = await AddIssuedBy.Start();
                            if (issuedByNew.Name != string.Empty)
                            {
                                issuedBy = issuedByNew;
                                await ShowString(IssuedByText.choosen);
                            }
                            else await ShowString(IssuedByText.notChoosen);
                            mainFlag = false;
                            break;
                        case 3: // возврат в предыдущее меню
                            await ShowString(IssuedByText.notChoosen);
                            mainFlag = false;
                            break;
                    }
                }
                else
                {
                    bool levelOneFlag = true;
                    while (levelOneFlag)
                    {
                        choice = await MenuToChoice(IssuedByText.options, issuedBy.Name, invite: Text.choice, noNull: true);
                        switch (choice)
                        {
                            case 1: // Выбрать
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: // Изменить
                                issuedBy = await ChangeIssuedBy.Start(issuedBy);
                                break;
                            case 3: //Удалить
                                issuedBy = await DelIssuedBy.Start(issuedBy);
                                if (issuedBy.Id == 0)
                                {
                                    levelOneFlag = false;
                                    await ShowString(Text.returnToSearch);
                                }
                                break;
                            case 4: //Вернуться к поиску
                                levelOneFlag = false;
                                await ShowString(Text.returnToSearch);
                                break;
                            case 5: //Вернуться в предыдущее меню
                                levelOneFlag = false;
                                mainFlag = false;
                                issuedBy = new();
                                await ShowString(IssuedByText.notChoosen);
                                break;
                        }
                    }
                }
            }
            return issuedBy;


        }
    }
}