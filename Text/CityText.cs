namespace MenusAndChoices
{
    public static class CityText
    {
        public static readonly string menuName = "ГОРОДА";
        public static readonly string searchString = "Давайте поищем город. Введите название или оставьте поле пустым для показа всех вариантов";
        public static readonly string name = "Введите название города";
        public static readonly string exist = "ТАКОЙ ГОРОД УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly string added = "ГОРОД ДОБАВЛЕН";
        public static readonly string notAdded = "ГОРОД НЕ ДОБАВЛЕН";
        public static readonly string choosen = "ГОРОД ВЫБРАН";
        public static readonly string notChoosen = "ГОРОД НЕ ВЫБРАН";
        public static readonly List<string> saveOptions = new() {
                                "Сохранить город",
                                "Изменить город",
                                "Не сохранять город"};

        public static readonly string changed = "ГОРОД ИЗМЕНЕН";
        public static readonly string changeCancel = "ГОРОД НЕ ИЗМЕНЕН";
        public static readonly string deleted = "ГОРОД УДАЛЕН";
        public static readonly string delCancel = "ГОРОД НЕ УДАЛЕН";






        public static readonly string cities = "ГОРОДА";
        public static readonly string cityName = "Введите название города";
        public static readonly string cityNameOrEmpty = "Введите название города или оставьте поле пустым для показа всех";
        public static readonly List<string> searchAgainOrAddCity = new() {
                                "Повторить поиск города",
                                "Добавить город",
                                "Отменить добавление адреса и вернуться в предыдущее меню"};
        public static readonly string cityChoosen = "ГОРОД ВЫБРАН";
        public static readonly string cityNotChoosen = "ГОРОД НЕ ВЫБРАН";
        public static readonly string cityAdded = "ГОРОД ДОБАВЛЕН";
        public static readonly string cityNotAdded = "ГОРОД НЕ ДОБАВЛЕН";
        public static readonly string cityExist = "ТАКОЙ ГОРОД УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly List<string> options = new() {
                                "Выбрать",
                                "Изменить",
                                "Вернуться к поиску города",
                                "Не выбирать адрес и вернуться в предыдущее меню"};


        public static readonly string changeConfirm = "ТОЧНО ИЗМЕНИТЬ ГОРОД?";


        public static readonly List<string> tryAgain = new() {
                                "Вернуться к поиску города еще раз",
                                "Не выбирать адрес и вернуться в предыдущее меню"};
    }
}