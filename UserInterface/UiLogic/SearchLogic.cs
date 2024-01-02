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
    internal abstract class SearchLogic<I, E, L> : BaseLogic<I, E, L>
    where I : BaseElement<I> where E : BaseElement<E> where L : BaseRepo<I, E>, IServiceUI<I>
    {

        /// <summary>
        /// Логика для поиска элемента
        /// </summary>
        /// <returns> элемент обобщенного типа I</returns>
        public static async Task<I> Start(bool cutOff = false)
        {
            string search = GetString(SearchString);
            await items!.SearchAndGet(search);
            if (cutOff) items.CutOff(parameter!);
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