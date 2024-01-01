using MenusAndChoices;
using Models;
using Interfaces;


namespace Service
{
    internal abstract class AddLogic<I, L> : BaseLogic<I, L> where I : BaseElement<I> where L : BaseRepo<I>, IService<I>
    {
       
        /// <summary>
        /// Логика для создания элемента
        /// </summary>
        /// <returns> элемент обобщенного типа I</returns>
        public static async Task<I> Start()
        {
            Item = await Items!.CreateAndAdd();
            bool flag = true;
            while (flag)
            {
                await ShowString(Item.Summary(), clear: true, delay: 0);
                int choice = await MenuToChoice(SaveOptions, string.Empty, CommonText.choice, clear: false, noNull: true);
                switch (choice)
                {
                    case 1: // Сохранить элемент
                        bool exist = await Items.CheckExist(Item);
                        if (exist) await ShowString(ItemExist);
                        else
                        {
                            Item = await Items.SaveGetId(Item);
                            await ShowString(ItemAdded);
                            return Item;
                        }
                        flag = false;
                        break;
                    case 2: // Изменить элемент
                        Item = await ChangeLogic<I, L>.Start(Item, toSql: false);
                        break;
                    case 3: // Не сохранять элемент
                        flag = false;
                        break;
                }
            }
            await ShowString(ItemNotAdded);
            return Item.SetEmpty();
        }
    }
}
