using Interfaces;
using MenusAndChoices;
using Models;

namespace Service
{

    /// <summary>
    /// Класс для создания логики изменения элемента в базе даннных
    /// </summary>
    /// <typeparam name="I"></typeparam> тип элемента
    /// <typeparam name="L"></typeparam> тип списка элементов
    internal abstract class ChangeLogic<I,E, L> : BaseLogic<I,E,L> 
    where I : BaseElement<I> where E : BaseElement<E> where L : BaseRepo<I, E>, IServiceUI<I>
    {
        /// <summary>
        /// Логика для создания элемента
        /// </summary>
        /// <returns> элемент обобщенного типа I</returns>
        public static async Task<I> Start(I item, bool toSql = true)
        {
            I itemOld = item.Clone();
            await ShowString(item.Summary(), clear: true, delay: 300);
            item = await items!.ChangeAndAdd(item);
            await ShowString(item.Summary(), clear: true, delay: 100);
            if (!item.Equals(itemOld))
            {
                int choice = await MenuToChoice(CommonText.yesOrNo, CommonText.changeConfirm, CommonText.choice, clear: false, noNull: true);
                if (choice == 1)
                {
                    bool exist = await items.CheckExist(item);
                    if (exist) await ShowString(ItemExist);
                    else
                    {
                        await ShowString(ItemChanged);
                        if (toSql) item = await items.SaveChanges(item);
                        return item;
                    }
                }
            }
            await ShowString(ChangeCancel);
            return itemOld;
        }
    }
}