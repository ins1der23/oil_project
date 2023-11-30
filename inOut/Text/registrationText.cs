
using Models;

namespace MenusAndChoices
{
    public static class RegistrationText
    {
        public static readonly List<string> addOrChange = new() {
                                "Добавить новый адрес регистрации",
                                "Работать с сохраненными адресами регистрации",
                                "Продолжить без выбора адреса регистрации"};

        public static readonly List<string> saveOptions = new() {
                                "Сохранить адрес",
                                "Изменить адрес",
                                "Не сохранять адрес"};
        public static readonly string houseNum = "Введите номер дома";
        public static readonly string flatNum = "Введите номер квартиры";
        public static readonly string registrationAdded = "АДРЕС УПЕШНО ДОБАВЛЕН";
        public static readonly string registrationNotAdded = "АДРЕС НЕ ДОБАВЛЕН";

        public static string Summary(this Registration registration)
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