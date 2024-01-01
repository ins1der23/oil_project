using MenusAndChoices;
using Models;
using Interfaces;

namespace Service
{
    internal abstract class StartLogic<I, L> : BaseLogic<I, L> where I : BaseElement<I> where L : BaseRepo<I>, IService<I>
    {
        public static async Task<I> Start(bool adding = true, bool changing = true, bool deleting = true)
        {
            bool mainFlag = true;
            int choice;
            while (mainFlag)
            {
                Item = await SearchLogic<I, L>.Start();
                if (Item.Id == 0)
                {
                    choice = await MenuToChoice(CommonText.searchAgainMenu, ItemsMenuName, invite: CommonText.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            await ShowString(CommonText.returnToSearch);
                            break;
                        case 2: // Добавить элемент
                            var itemNew = await AddLogic<I, L>.Start();
                            if (itemNew.Id != 0)
                            {
                                Item = itemNew;
                                await ShowString(ItemChoosen);
                            }
                            else await ShowString(ItemNotChoosen);
                            mainFlag = false;
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
                        choice = await MenuToChoice(CommonText.options, Item.Summary(), invite: CommonText.choice, noNull: true);
                        switch (choice)
                        {
                            case 1: // Выбрать
                                await ShowString(ItemChoosen);
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: //Изменить
                                if (changing) Item = await ChangeLogic<I, L>.Start(Item);
                                else await ShowString(CommonText.notAvailable);
                                break;
                            case 3: // Удалить
                                if (deleting)
                                {
                                    Item = await DeleteLogic<I, L>.Start(Item);
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
                                Item.SetEmpty();
                                await ShowString(ItemNotChoosen);
                                break;
                        }
                    }
                }
            }
            return Item;
        }

    /// <summary>
    /// Перегрузка логики для микрорайонов и улиц
    /// </summary>
    /// <param name="city"></param>
    /// <param name="adding"></param>
    /// <param name="changing"></param>
    /// <param name="deleting"></param>
    /// <returns></returns>
        public static async Task<I> Start(City city, bool adding = true, bool changing = true, bool deleting = true)
        {
            bool mainFlag = true;
            int choice;
            while (mainFlag)
            {
                Item = await SearchLogic<I, L>.Start();
                if (Item.Id == 0)
                {
                    choice = await MenuToChoice(CommonText.searchAgainMenu, ItemsMenuName, invite: CommonText.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            await ShowString(CommonText.returnToSearch);
                            break;
                        case 2: // Добавить элемент
                            var itemNew = await AddLogic<I, L>.Start();
                            if (itemNew.Id != 0)
                            {
                                Item = itemNew;
                                await ShowString(ItemChoosen);
                            }
                            else await ShowString(ItemNotChoosen);
                            mainFlag = false;
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
                        choice = await MenuToChoice(CommonText.options, Item.Summary(), invite: CommonText.choice, noNull: true);
                        switch (choice)
                        {
                            case 1: // Выбрать
                                await ShowString(ItemChoosen);
                                levelOneFlag = false;
                                mainFlag = false;
                                break;
                            case 2: //Изменить
                                if (changing) Item = await ChangeLogic<I, L>.Start(Item);
                                else await ShowString(CommonText.notAvailable);
                                break;
                            case 3: // Удалить
                                if (deleting)
                                {
                                    Item = await DeleteLogic<I, L>.Start(Item);
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
                                Item.SetEmpty();
                                await ShowString(ItemNotChoosen);
                                break;
                        }
                    }
                }
            }
            return Item;
        }

    }
}











