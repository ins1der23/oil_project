using MenusAndChoices;
using Models;
using Interfaces;


namespace Service
{
    internal abstract class AddLogic<I, E, L> : BaseLogic<I, E, L>
    where I : BaseElement<I> where E : BaseElement<E> where L : BaseRepo<I>, IServiceUI<I>
    {

        /// <summary>
        /// Логика для создания элемента
        /// </summary>
        /// <returns> элемент обобщенного типа I</returns>
        public static async Task<I> Start()
        {
            item = await items!.CreateAndAdd();
            bool flag = true;
            while (flag)
            {
                await ShowString(item.Summary(), clear: true, delay: 0);
                int choice = await MenuToChoice(SaveOptions, string.Empty, CommonText.choice, clear: false, noNull: true);
                switch (choice)
                {
                    case 1: // Сохранить элемент
                        bool exist = await items.CheckExist(item);
                        if (exist) await ShowString(ItemExist);
                        else
                        {
                            item = await items.SaveGetId(item);
                            await ShowString(ItemAdded);
                            return item;
                        }
                        flag = false;
                        break;
                    case 2: // Изменить элемент
                        item = await ChangeLogic<I, E, L>.Start(item, toSql: false);
                        break;
                    case 3: // Не сохранять элемент
                        flag = false;
                        break;
                }
            }
            await ShowString(ItemNotAdded);
            return item.SetEmpty();
        }


        /// <summary>
        /// Перегрузка логики для районов и улиц
        /// </summary>
        /// <param name="city">Передается City, к которому относятся районы или улицы</param> 
        /// <returns></returns>
        public static async Task<I> Start(City city)
        {
            item = await items!.CreateAndAdd();
            bool flag = true;
            while (flag)
            {
                await ShowString(item.Summary(), clear: true, delay: 0);
                int choice = await MenuToChoice(SaveOptions, string.Empty, CommonText.choice, clear: false, noNull: true);
                switch (choice)
                {
                    case 1: // Сохранить элемент
                        bool exist = await items.CheckExist(item);
                        if (exist) await ShowString(ItemExist);
                        else
                        {
                            item = await items.SaveGetId(item);
                            await ShowString(ItemAdded);
                            return item;
                        }
                        flag = false;
                        break;
                    case 2: // Изменить элемент
                        item = await ChangeLogic<I, E, L>.Start(item, toSql: false);
                        break;
                    case 3: // Не сохранять элемент
                        flag = false;
                        break;
                }
            }
            await ShowString(ItemNotAdded);
            return item.SetEmpty();
        }
    }
}
