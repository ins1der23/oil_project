
using Models;

namespace MenusAndChoices
{
    public static class RegistrationText
    {
        public static readonly List<string> addOrChange = new() {
                                "Добавить новый адрес регистрации",
                                "Работать с сохраненными адресами регистрации",
                                "Продолжить без выбора адреса регистрации"};
        public static readonly List<string> options = new() {
                                "Выбрать",
                                "Изменить",
                                "Удалить",
                                "Не выбирать адрес и вернуться в предыдущее меню"};
        public static readonly List<string> changeOptions = new() {
                                "Выбрать другой город",
                                "Выбрать другую улицу",
                                "Изменить номер дома",
                                "Изменить номер квартиры",
                                "Сохранить или вернуться в предыдущее меню"};

        public static readonly List<string> saveOptions = new() {
                                "Сохранить адрес",
                                "Изменить адрес",
                                "Не сохранять адрес"};
        public static readonly string houseNum = "Введите номер дома";
        public static readonly string flatNum = "Введите номер квартиры";
        public static readonly string registrationAdded = "АДРЕС УПЕШНО ДОБАВЛЕН";
        public static readonly string registrationNotAdded = "АДРЕС НЕ ДОБАВЛЕН";
        public static readonly string registrationNotFound = "Адрес не найден";
        public static readonly string registrationSearch = "Введите текст для поиска АДРЕСА или оставьте поле пустым для показа всех";
        public static readonly string addressesFound = "Найденные адреса";
        public static readonly string changeHouseNum = "Введите новый номер дома или оставьте поле пустым, если не хотите менять ";
        public static readonly string changeFlatNum = "Введите новый номер дома или оставьте поле пустым, если не хотите менять ";

        public static string Summary(Registration registration)
        {
            Console.Clear();
            return @$"
        Город:        {registration.City.Name}
        Улица:        {registration.Street.Name}
        Номер дома:   {registration.HouseNum}
        Квартира:     {registration.FlatNum}";
        }
    }
}