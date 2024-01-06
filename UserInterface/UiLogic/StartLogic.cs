using MenusAndChoices;
using Models;
using Interfaces;
using Handbooks;

namespace Service
{
    internal abstract class StartLogic<I, E, L> : BaseLogic<I, E, L>
    where I : BaseElement<I> where E : BaseElement<E> where L : BaseRepo<I>, IServiceUI<I>
    {
        public static async Task<I> Start(bool adding = true, bool changing = true, bool deleting = true, bool cutOff = false)
        {
            mainFlag = true;
            while (mainFlag)
            {
                item = await SearchLogic<I, E, L>.Start(cutOff);
                if (item.Id == 0)
                {
                    choice = await MenuToChoice(CommonText.searchAgainMenu, ItemsMenuName, invite: CommonText.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            await ShowString(CommonText.returnToSearch);
                            break;
                        case 2: // Добавить элемент
                            if (adding)
                            {
                                var itemNew = await AddLogic<I, E, L>.Start();
                                if (itemNew.Id != 0)
                                {
                                    item = itemNew;
                                    await ShowString(ItemChoosen);
                                }
                                else await ShowString(ItemNotChoosen);
                                mainFlag = false;
                            }
                            else await ShowString(CommonText.notAvailable);
                            break;
                        case 3: // Отменить добавление элемента и вернуться в предыдущее меню
                            await ShowString(ItemNotChoosen);
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
                                await ShowString(ItemChoosen);
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: //Изменить
                                if (changing) item = await ChangeLogic<I, E, L>.Start(item);
                                else await ShowString(CommonText.notAvailable);
                                break;
                            case 3: // Удалить
                                if (deleting)
                                {
                                    item = await DeleteLogic<I, E, L>.Start(item);
                                    levelOneFlag = false;
                                    mainFlag = false;
                                    await ShowString(ItemNotChoosen);
                                }
                                else await ShowString(CommonText.notAvailable);
                                break;
                            case 4: // Вернуться к поиску
                                levelOneFlag = false;
                                await ShowString(CommonText.returnToSearch);
                                break;
                            case 5: // Вернуться в предыдущее меню
                                levelOneFlag = false;
                                mainFlag = false;
                                item.SetEmpty();
                                await ShowString(ItemNotChoosen);
                                break;
                        }
                    }
                }
            }
            return item!;
        }
    }
}











