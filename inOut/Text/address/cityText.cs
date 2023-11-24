namespace MenusAndChoices
{
    public static class CityText
    {
        public static readonly string cities = "ГОРОДА";
        public static readonly string cityName = "Введите название города";
        public static readonly string cityNameOrEmpty = "Введите название города или оставьте поле пустым для показа всех";
        public static readonly List<string> searchAgainOrAddCity = new() {
                                "Повторить поиск города",
                                "Добавить город",
                                "Отменить добавление адреса и вернуться в предыдущее меню"};
        public static readonly List<string> saveOptions = new() {
                                "Сохранить город",
                                "Изменить город",
                                "Не сохранять город"};
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
        public static readonly string changeName = "Введите новое название города или оставьте поле пустым, если не хотите менять";
        public static readonly string changeCancel = "ГОРОД НЕ ИЗМЕНЕН";
        public static readonly string changeConfirm = "ТОЧНО ИЗМЕНИТЬ ГОРОД?";
        public static readonly string changed = "ГОРОД ИЗМЕНЕН";
    }
}