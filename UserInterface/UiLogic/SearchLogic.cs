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
    internal abstract class SearchLogic<I, L> : BaseLogic<I, L> where I : BaseElement<I> where L : BaseRepo<I>, IService<I>
    {
        // protected static string ItemFoundText { get => BaseLogic<I, L>.ItemFoundText; set => BaseLogic<I, L>.ItemFoundText = value; }
        // protected static string SearchStringText { get => BaseLogic<I, L>.SearchStringText; set => BaseLogic<I, L>.SearchStringText = value; }


        /// <summary>
        /// Логика для поиска элемента
        /// </summary>
        /// <returns> элемент обобщенного типа I</returns>
        public static async Task<I> Start()
        {
            string searchString = GetString(SearchString);
            await Items!.SearchAndGet(searchString);
            bool flag = true;
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(Items.ToStringList(), CommonText.itemsFound, CommonText.choiceOrEmpty);
                if (choice != 0) Item = Items.GetFromList(choice);
                flag = false;
            }
            return Item;
        }
    }
}