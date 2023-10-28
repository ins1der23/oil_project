namespace MenusAndChoices
{
    public static class StreetText
    {
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
        public static readonly List<string> options = new() {
                                "Выбрать",
                                "Изменить",
                                "Вернуться к поиску улицы",
                                "Не выбирать улицу"};

        public static readonly List<string> tryAgain = new() {
                                "Вернуться к поиску улицы еще раз",
                                "Не выбирать адрес и вернуться в предыдущее меню"};
        public static readonly string changeName = "Введите новое название улицы или оставьте поле пустым, если не хотите менять";
        public static readonly string changeCancel = "УЛИЦА НЕ ИЗМЕНЕНА";
        public static readonly string changeConfirm = "ТОЧНО ИЗМЕНИТЬ УЛИЦУ?";
        public static readonly string changed = "УЛИЦА ИЗМЕНЕНА";
    }
}