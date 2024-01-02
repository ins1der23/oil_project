namespace MenusAndChoices
{
    public static class StreetText
    {
        public static readonly string menuName = "УЛИЦЫ";
        public static readonly string searchString = "Давайте поищем улицу. Введите название или оставьте поле пустым для показа всех вариантов";
        public static readonly string name = "Введите название улицы";
        public static readonly string exist = "ТАКАЯ УЛИЦА УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly string added = "УЛИЦА ДОБАВЛЕНА";
        public static readonly string notAdded = "УЛИЦА НЕ ДОБАВЛЕНА";
        public static readonly string choosen = "УЛИЦА ВЫБРАНА";
        public static readonly string notChoosen = "УЛИЦА НЕ ВЫБРАНА";
        public static readonly List<string> saveOptions = new() {
                                "Сохранить улицу",
                                "Изменить улицу",
                                "Не сохранять улицу"};

        public static readonly string changed = "УЛИЦА ИЗМЕНЕНА";
        public static readonly string changeCancel = "УЛИЦА НЕ ИЗМЕНЕНА";
        public static readonly string deleted = "УЛИЦА УДАЛЕНА";
        public static readonly string delCancel = "УЛИЦА НЕ УДАЛЕНА";
        public static readonly List<string> changeMenu = new() {
                                "Изменить город",
                                "Изменить название",
                                "Вернуться"
         };











        public static readonly string streets = "УЛИЦЫ";
        public static readonly string streetName = "Введите название улицы";
        public static readonly string streetNameOrEmpty = "Введите название улицы или оставьте поле пустым для показа всех";
        public static readonly List<string> searchAgainOrAddStreet = new() {
                                "Повторить поиск улицы",
                                "Добавить улицу",
                                "Не выбирать улицу"};


        public static readonly string streetChoosen = "УЛИЦА ВЫБРАНА";
        public static readonly string streetNotChoosen = "УЛИЦА НЕ ВЫБРАНА";
        public static readonly string streetAdded = "УЛИЦА ДОБАВЛЕНА";
        public static readonly string streetNotAdded = "УЛИЦА НЕ ДОБАВЛЕНА";
        public static readonly string streetExist = "ТАКАЯ УЛИЦА УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly List<string> options = new() {
                                "Выбрать",
                                "Изменить",
                                "Вернуться к поиску улицы",
                                "Не выбирать улицу"};

        public static readonly List<string> tryAgain = new() {
                                "Вернуться к поиску улицы еще раз",
                                "Не выбирать адрес и вернуться в предыдущее меню"};
        public static readonly string changeName = "Введите новое название улицы или оставьте поле пустым, если не хотите менять";

        public static readonly string changeConfirm = "ТОЧНО ИЗМЕНИТЬ УЛИЦУ?";


    }
}