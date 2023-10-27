
namespace MenusAndChoices
{
    public static class IssuedByText
    {
        public static readonly string search = "Давайте поищем орган, выдавший паспорт. Введите название или оставьте поле пустым для показа всех вариантов";
        public static readonly string name = "Введите название органа, выдавшего паспорт";
        public static readonly string issuedFound = "Найденные варианты";
        public static readonly List<string> searchAgainOrAdd = new() {
                                "Повторить поиск",
                                "Добавить орган, выдавший паспорт",
                                "Возврат в предыдущее меню"};

        public static readonly string added = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, ДОБАВЛЕН";
        public static readonly string choosen = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, ВЫБРАН";
        public static readonly string notChoosen = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, НЕ ВЫБРАН";

        public static readonly List<string> options = new() {
                                "Выбрать ",
                                "Изменить",
                                "Удалить",
                                "Вернуться к поиску",
                                "Вернуться в предыдущее меню"};
        
        public static readonly string changeName = "Введите новое название или оставьте поле пустым, если не хотите менять";
        public static readonly string changed = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, ИЗМЕНЕН";
        public static readonly string changeCancel = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, НЕ ИЗМЕНЕН";
        public static readonly string changeConfirm = "ТОЧНО ИЗМЕНИТЬ?";




        public static readonly string deleted = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, УДАЛЕН";
        public static readonly string delCancel = "ОРГАН, ВЫДАВШИЙ ПАСПОРТ, НЕ УДАЛЕН";
        public static readonly string delConfirm = "ТОЧНО УДАЛИТЬ?";
        
        
        

    }
}
