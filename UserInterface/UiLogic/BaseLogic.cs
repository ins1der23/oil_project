using Interfaces;
using Models;

namespace Service
{

    /// <summary>
    /// Родительский Класс для создания логики интетфейса
    /// </summary>
    /// <typeparam name="I"></typeparam> тип элемента
    /// <typeparam name="L"></typeparam> тип списка элементов
    internal abstract class BaseLogic<I, L> where I : BaseElement<I> where L : BaseRepo<I>, IService<I>
    {
        /// <summary>
        /// Элемент обобщенного типа I
        /// </summary>
        private static I? item;

        /// <summary>
        /// Экземпляр BaseList элементов обобщенного типа I
        /// </summary>
        private static L? items;

        /// Переменные для подстановки текста
        private static List<string> saveOptions = new();
        private static string itemExist = string.Empty;
        private static string itemAdded = string.Empty;
        private static string itemNotAdded = string.Empty;
        private static string itemChanged = string.Empty;
        private static string changeCancel = string.Empty;
        private static string searchString = string.Empty;
        private static string itemsMenuName = string.Empty;
        private static string itemChoosen = string.Empty;
        private static string deleted = string.Empty;
        private static string delCancel = string.Empty;




        public static I Item { get => item!; set => item = value; }
        public static L? Items { get => items; set => items = value; }
        public static string Deleted { get => deleted; set => deleted = value; }
        public static string DelCancel { get => delCancel; set => delCancel = value; }
        protected static List<string> SaveOptions { get => saveOptions; set => saveOptions = value; }
        protected static string ItemExist { get => itemExist; set => itemExist = value; }
        protected static string ItemAdded { get => itemAdded; set => itemAdded = value; }
        protected static string ItemNotAdded { get => itemNotAdded; set => itemNotAdded = value; }
        protected static string ItemChanged { get => itemChanged; set => itemChanged = value; }
        protected static string ChangeCancel { get => changeCancel; set => changeCancel = value; }
        protected static string SearchString { get => searchString; set => searchString = value; }
        protected static string ItemsMenuName { get => itemsMenuName; set => itemsMenuName = value; }
        protected static string ItemChoosen { get => itemChoosen; set => itemChoosen = value; }
        protected static string ItemNotChoosen { get; set; } = string.Empty;










    }
}
