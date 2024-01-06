namespace MenusAndChoices
{
    public static class LocationText
    {

        public static readonly List<string> changeMenu = new() {
                                "Изменить название",
                                "Изменить город",
                                "Изменить район",
                                "Вернуться"
        };

        public static readonly List<string> searchAgain = new() {
                                "Повторить поиск микрорайона",
                                "Не выбирать микрорайон"};

        public static readonly List<string> tryAgain = new() {
                                "Вернуться к поиску микрорайона еще раз",
                                "Не выбирать адрес и вернуться в предыдущее меню"};




        public static readonly string menuName = "МИКРОРАЙОНЫ";
        public static readonly string searchString = "Давайте поищем микрорайон. Введите название или оставьте поле пустым для показа всех вариантов";
        public static readonly string name = "Введите название микрорайона";
        public static readonly List<string> searchAgainMenu = new() {
                                "Повторить поиск",
                                "Добавить микрорайон",
                                "Возврат в предыдущее меню"};
        public static readonly string exist = "ТАКОЙ МИКРОРАЙОН УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly string added = "МИКРОРАЙОН ДОБАВЛЕН";
        public static readonly string notAdded = "МИКРОРАЙОН НЕ ДОБАВЛЕН";
        public static readonly string choosen = "МИКРОРАЙОН ВЫБРАН";
        public static readonly string notChoosen = "МИКРОРАЙОН НЕ ВЫБРАН";
        public static readonly List<string> saveOptions = new() {
                                "Сохранить микрорайон",
                                "Изменить микрорайон",
                                "Не сохранять микрорайон"};
        public static readonly string changed = "МИКРОРАЙОН ИЗМЕНЕН";
        public static readonly string changeCancel = "МИКРОРАЙОН НЕ ИЗМЕНЕН";
        public static readonly string deleted = "МИКРОРАЙОН УДАЛЕН";
        public static readonly string delCancel = "МИКРОРАЙОН НЕ УДАЛЕН";


    }


}