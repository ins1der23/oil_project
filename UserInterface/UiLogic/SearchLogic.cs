using MenusAndChoices;
using Models;
using Interfaces;

namespace Service
{
    /// <summary>
    /// Класс для создания логики поиска элемента в базе даннных
    /// </summary>
    /// <typeparam name="I"></typeparam> тип элемента
    /// <typeparam name="L"></typeparam> тип списка элементов
    public abstract class SearchLogic<I,L> : BaseLogic<I, L>
    where I : BaseElement<I> where L : BaseRepo<I>, IServiceUI<I>
    {

        /// <summary>
        /// Логика для поиска элемента
        /// </summary>
        /// <returns> элемент обобщенного типа I</returns>
        public static async Task<I> Start(object cutOffBy = null!)
        {
            string search = GetString(SearchString);
            await items!.SearchAndGet(search);
            if (cutOffBy != null) items.CutOff(cutOffBy);
            bool flag = true;
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(items.ToStringList(), CommonText.itemsFound, CommonText.choiceOrEmpty);
                if (choice != 0) item = items.GetFromList(choice);
                flag = false;
            }
            return item!;
        }
    }
}