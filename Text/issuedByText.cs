
namespace MenusAndChoices
{
    public static class IssuedByText
    {
        public static readonly string menuName = "ОРГАНЫ ВЫДАЧИ";
        public static readonly string searchString = "Давайте поищем орган, выдавший паспорт. Введите название или оставьте поле пустым для показа всех вариантов";
        public static readonly string name = "Введите название органа, выдавшего паспорт";
        public static readonly List<string> searchAgainMenu = new() {
                                "Повторить поиск",
                                "Добавить орган, выдавший паспорт",
                                "Возврат в предыдущее меню"};
        public static readonly string exist = "ТАКОЙ ОРГАН УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly string added = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, ДОБАВЛЕН";
        public static readonly string notAdded = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, НЕ ДОБАВЛЕН";
        public static readonly string choosen = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, ВЫБРАН";
        public static readonly string notChoosen = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, НЕ ВЫБРАН";
        public static readonly List<string> saveOptions = new() {
                                "Сохранить орган, выдавший паспорт",
                                "Изменить орган, выдавший паспорт",
                                "Не сохранять орган, выдавший паспорт"};

        public static readonly string changed = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, ИЗМЕНЕН";
        public static readonly string changeCancel = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, НЕ ИЗМЕНЕН";
        
        public static readonly string deleted = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, УДАЛЕН";
        public static readonly string delCancel = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, НЕ УДАЛЕН";
        




    }
}
