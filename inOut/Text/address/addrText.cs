namespace MenusAndChoices
{
    public static class AddrText
    {

        public static readonly List<string> searchAgainOrAddAddress = new List<string>() {
                                "Повторить поиск адреса",
                                "Добавить адрес",
                                "Возврат в предыдущее меню"};
        public static readonly List<string> searchOrAddContinue = new List<string>() {
                                "Повторить поиск адреса",
                                "Добавить адрес",
                                "Продолжить без добавления адреса"};
        public static readonly string addressesFound = "Найденные адреса";

        // AddAddress
        public static readonly string addressSearch = "Введите текст для поиска и добавления АДРЕСА или оставьте поле пустым для показа всех";

        




        public static readonly string addressNotAdded = "АДРЕС НЕ ДОБАВЛЕН";

        public static readonly string locations = "МИКРОРАЙОНЫ";
        public static readonly string streets = "УЛИЦЫ";
        public static readonly List<string> searchAgainOrAddStreet = new List<string>() {
                                "Повторить поиск улицы",
                                "Добавить улицу",
                                "Возврат в предыдущее меню"};
        public static readonly string streetAdded = "УЛИЦА ДОБАВЛЕНА";
        public static readonly string streetChoosen = "УЛИЦА ВЫБРАНА";
        public static readonly string houseNum = "Введите номер дома";
        public static readonly string flatNum = "Введите номер квартиры или оставьте поле пустым";
        public static readonly string addressAdded = "АДРЕС УПЕШНО ДОБАВЛЕН";

        public static readonly string addressChoosing = "\nВЫБОР АДРЕСА";
        public static readonly string addressNotChoosen = "АДРЕС НЕ ВЫБРАН";
        public static readonly string changeAddress = "Изменить адрес?";
        public static readonly string addressChoosen = "АДРЕС ВЫБРАН";
        public static readonly string addressNotChanged = "АДРЕС НЕ ИЗМЕНЕН";



    }
}