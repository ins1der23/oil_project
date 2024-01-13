
using Models;

namespace MenusAndChoices
{
    public static class RegistrationText
    {
        public static string Summary(Registration registration)
        {
            Console.Clear();
            return @$"
        Город:        {registration.City.Name}
        Улица:        {registration.Street.Name}
        Номер дома:   {registration.HouseNum}
        Квартира:     {registration.FlatNum}";
        }
        // Registartions MUI
        public static readonly string houseNum = "Введите номер дома";
        public static readonly string flatNum = "Введите номер квартиры";
        public static readonly List<string> changeMenu = new() {
                                "Выбрать другой город",
                                "Выбрать другую улицу",
                                "Изменить номер дома или квартиры",
                                "Сохранить или вернуться в предыдущее меню"};
        public static readonly string changeHouseNum = "Введите новый номер дома или оставьте поле пустым, если не хотите менять ";
        public static readonly string changeFlatNum = "Введите новый номер квартиры или оставьте поле пустым, если не хотите менять ";

        //RegistrationsUI
        public static readonly string menuName = "АДРЕСА";
        public static readonly string searchString = "Давайте поищем адрес. Введите название или оставьте поле пустым для показа всех вариантов";
        public static readonly string exist = "ТАКОЙ АДРЕС УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly string added = "АДРЕС ДОБАВЛЕН";
        public static readonly string notAdded = "АДРЕС НЕ ДОБАВЛЕН";
        public static readonly string choosen = "АДРЕС ВЫБРАН";
        public static readonly string notChoosen = "АДРЕС НЕ ВЫБРАН";
        public static readonly List<string> saveOptions = new() {
                                "Сохранить адрес",
                                "Изменить адрес",
                                "Не сохранять адрес"};

        public static readonly string changed = "АДРЕС ИЗМЕНЕН";
        public static readonly string changeCancel = "АДРЕС НЕ ИЗМЕНЕН";
        public static readonly string deleted = "АДРЕС УДАЛЕН";
        public static readonly string delCancel = "АДРЕС НЕ УДАЛЕН";



        // Other


        public static readonly List<string> addOrChange = new() {
                                "Добавить новый адрес регистрации",
                                "Работать с сохраненными адресами регистрации",
                                "Продолжить без выбора адреса регистрации"};
        public static readonly List<string> options = new() {
                                "Выбрать",
                                "Изменить",
                                "Удалить",
                                "Не выбирать адрес и вернуться в предыдущее меню"};






        public static readonly string registrationAdded = "АДРЕС УПЕШНО ДОБАВЛЕН";
        public static readonly string registrationNotAdded = "АДРЕС НЕ ДОБАВЛЕН";
        public static readonly string registrationNotFound = "Адрес не найден";
        public static readonly string registrationSearch = "Введите текст для поиска АДРЕСА или оставьте поле пустым для показа всех";
        public static readonly string addressesFound = "Найденные адреса";




    }
}