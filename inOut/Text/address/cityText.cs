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
                                "Возврат в предыдущее меню"};
        public static readonly string cityChoosen = "ГОРОД ВЫБРАН";
        public static readonly string cityNotChoosen = "ГОРОД НЕ ВЫБРАН";
        public static readonly string cityAdded = "ГОРОД ДОБАВЛЕН";
        public static readonly List<string> options = new() {
                                "Выбрать",
                                "Изменить",
                                "Вернуться к поиску",
                                "Вернуться в предыдущее меню"};
        public static readonly string changeName = "Введите новое название города или оставьте поле пустым, если не хотите менять";
        public static readonly string changeCancel = "ГОРОД НЕ ИЗМЕНЕН";
        public static readonly string changeConfirm = "ТОЧНО ИЗМЕНИТЬ ГОРОД?";
        public static readonly string changed = "ГОРОД ИЗМЕНЕН";
    }
}