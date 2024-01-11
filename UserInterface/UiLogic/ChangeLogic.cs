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
    public abstract class ChangeLogic<I, L> : BaseLogic<I, L>
    where I : BaseElement<I> where L : BaseRepo<I>, IServiceUI<I>
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
                    if (toSql && !exist) item = await items.SaveChanges(item);
                    await ShowString(ItemChanged);
                    return item;
                }
            }
            await ShowString(ChangeCancel);
            return itemOld;
        }
    }
}