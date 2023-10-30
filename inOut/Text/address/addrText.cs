using Models;

namespace MenusAndChoices
{
    public static class AddrText
    {

        public static readonly List<string> searchAgainOrAddAddress = new() {
                                "Повторить поиск адреса",
                                "Добавить адрес",
                                "Отменить выбор адреса и вернуться в предыдущее меню"};
        public static readonly List<string> searchOrAddContinue = new() {
                                "Повторить поиск адреса",
                                "Добавить адрес",
                                "Продолжить без добавления адреса"};

        public static readonly List<string> saveAddress = new() {
                                "Сохранить адрес",
                                "Изменить адрес",
                                "Не сохранять адрес"};
        public static readonly string addressesFound = "Найденные адреса";
        public static readonly List<string> options = new() {
                                "Выбрать",
                                "Изменить",
                                "Удалить",
                                "Вернуться к поиску адреса",
                                "Не выбирать адрес и вернуться в предыдущее меню"};

        // AddAddress
        public static readonly string addressSearch = "Введите текст для поиска и добавления АДРЕСА или оставьте поле пустым для показа всех";
        public static readonly string addressNotAdded = "АДРЕС НЕ ДОБАВЛЕН";
        public static readonly string houseNum = "Введите номер дома";
        public static readonly string addressAdded = "АДРЕС УПЕШНО ДОБАВЛЕН";
        public static readonly string addressChoosing = "\nВЫБОР АДРЕСА";
        public static readonly string addressNotChoosen = "АДРЕС НЕ ВЫБРАН";
        public static readonly string changeAddress = "Изменить адрес?";
        public static readonly string addressChoosen = "АДРЕС ВЫБРАН";
        public static string Summary(this Address address)
        {
            Console.Clear();
            if (address.CityId == 1) return @$"
        Город:        {address.City.Name}
        Район:        {address.District.Name}
        Микрорайон:   {address.Location.Name}
        Улица:        {address.Street.Name}
        Номер дома:   {address.HouseNum}";
            return @$"
        Город:        {address.City.Name}
        Улица:        {address.Street.Name}
        Номер дома:   {address.HouseNum}";
        }
        // ChangeAddress
        public static List<string> ChangeOptions(this Address address)
        {
            List<string> changeOptions = address.CityId == 1
            ? new()
            {
                                "Выбрать другой город",
                                "Выбрать другой микрорайон",
                                "Выбрать другую улицу",
                                "Изменить номер дома",
                                "Вернуться в предыдущее меню"
            }
            : new()
            {
                                "Выбрать другой город",
                                "Выбрать другую улицу",
                                "Изменить номер дома",
                                "Вернуться в предыдущее меню"
            };
            return changeOptions;
        }
        public static readonly string addressChanged = "АДРЕС УСПЕШНО ИЗМЕНЕН";
        public static readonly string addressNotChanged = "АДРЕС НЕ ИЗМЕНЕН";
        public static readonly string changeHouseNum = "Введите новый номер дома или оставьте поле пустым, если не хотите менять ";
        public static readonly string confirmChanges = "Адрес был изменен. Сохранить изменения?";

        // Delete Addrress
        public static readonly string delAddress = "ТОЧНО УДАЛИТЬ ЭТОТ АДРЕС???";
        public static readonly string clientDeleted = "АДРЕС УСПЕШНО УДАЛЕН";
        public static readonly string clientNotDeleted = "АДРЕС НЕ УДАЛЕН";


    }


}