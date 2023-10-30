namespace MenusAndChoices
{
    public static class LocationText
    {
        public static readonly string locations = "МИКРОРАЙОНЫ";
        public static readonly string locationNameOrEmpty = "Введите название микрорайона или оставьте поле пустым для показа всех";
        public static readonly List<string> searchAgain = new() {
                                "Повторить поиск микрорайона",
                                "Не выбирать микрорайон"};
        public static readonly string locationChoosen = "МИКРОРАЙОН ВЫБРАН";
        public static readonly string locationNotChoosen = "МИКРОРАЙОН НЕ ВЫБРАН";
        public static readonly List<string> options = new() {
                                "Выбрать ",
                                "Вернуться к поиску микрорайона",
                                "Не выбирать микрорайон"};
        public static readonly List<string> tryAgain = new() {
                                "Вернуться к поиску микрорайона еще раз",
                                "Не выбирать адрес и вернуться в предыдущее меню"};
    }
}