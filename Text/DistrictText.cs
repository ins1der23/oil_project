namespace MenusAndChoices
{
    public static class DistrictText
    {
        public static readonly string menuName = "РАЙОНЫ";
        public static readonly string searchString = "Давайте поищем район. Введите название или оставьте поле пустым для показа всех вариантов";
        public static readonly string name = "Введите название района";
        public static readonly string exist = "ТАКОЙ РАЙОН УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly string added = "РАЙОН ДОБАВЛЕН";
        public static readonly string notAdded = "РАЙОН НЕ ДОБАВЛЕН";
        public static readonly string choosen = "РАЙОН ВЫБРАН";
        public static readonly string notChoosen = "РАЙОН НЕ ВЫБРАН";
        public static readonly List<string> saveOptions = new() {
                                "Сохранить район",
                                "Изменить район",
                                "Не сохранять район"};

        public static readonly string changed = "РАЙОН ИЗМЕНЕН";
        public static readonly string changeCancel = "РАЙОН НЕ ИЗМЕНЕН";
        public static readonly string deleted = "РАЙОН УДАЛЕН";
        public static readonly string delCancel = "РАЙОН НЕ УДАЛЕН";
        public static readonly List<string> changeMenu = new() {
                                "Изменить название",
                                "Изменить город",
                                "Вернуться"
         };
    }
}
