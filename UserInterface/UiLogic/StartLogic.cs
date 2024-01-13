using MenusAndChoices;
using Models;
using Interfaces;


namespace Service
{
    public abstract class StartLogic<I, L> : BaseLogic<I, L>
    where I : BaseElement<I> where L : BaseRepo<I>, IServiceUI<I>
    {
        public static async Task<I> Start(bool adding = true, bool changing = true, bool deleting = true, object cutOffBy = null!)
        {
            mainFlag = true;
            while (mainFlag)
            {
                item = await SearchLogic<I, L>.Start(cutOffBy);
                if (item.Id == 0)
                {
                    choice = await MenuToChoice(CommonText.searchAgainMenu, ItemsMenuName, invite: CommonText.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            await ShowString(CommonText.returnToSearch, clear: false);
                            break;
                        case 2: // Добавить элемент
                            if (adding)
                            {
                                var itemNew = await AddLogic<I, L>.Start();
                                if (itemNew.Id != 0)
                                {
                                    item = itemNew;
                                    await ShowString(ItemChoosen, clear: false);
                                }
                                else await ShowString(ItemNotChoosen, clear: false);
                                mainFlag = false;
                            }
                            else await ShowString(CommonText.notAvailable, clear: false);
                            break;
                        case 3: // Назначить пустой элемент
                            await ShowString(CommonText.emptyElement, clear: false);
                            item.ChooseEmpty = true;
                            mainFlag = false;
                            break;
                        case 4: // Отменить добавление элемента и вернуться в предыдущее меню
                            await ShowString(ItemNotChoosen, clear: false);
                            mainFlag = false;
                            break;
                    }
                }
                else
                {
                    bool levelOneFlag = true;
                    while (levelOneFlag)
                    {
                        choice = await MenuToChoice(CommonText.options, item.Summary(), invite: CommonText.choice, noNull: true);
                        switch (choice)
                        {
                            case 1: // Выбрать
                                await ShowString(ItemChoosen, clear: false);
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: //Изменить
                                if (changing) item = await ChangeLogic<I, L>.Start(item);
                                else await ShowString(CommonText.notAvailable, clear: false);
                                break;
                            case 3: // Удалить
                                if (deleting)
                                {
                                    item = await DeleteLogic<I, L>.Start(item);
                                    levelOneFlag = false;
                                    mainFlag = false;
                                    await ShowString(ItemNotChoosen, clear: false);
                                }
                                else await ShowString(CommonText.notAvailable, clear: false);
                                break;
                            case 4: // Вернуться к поиску
                                levelOneFlag = false;
                                await ShowString(CommonText.returnToSearch, clear: false);
                                break;
                            case 5: // Вернуться в предыдущее меню
                                levelOneFlag = false;
                                mainFlag = false;
                                item.SetEmpty();
                                await ShowString(ItemNotChoosen, clear: false);
                                break;
                        }
                    }
                }
            }
            return item!;
        }
    }
}











