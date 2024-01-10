using Models;

namespace MenusAndChoices
{
    public static class PassportText
    {
        //Passport
        public static string Summary(Passport passport)
        {
            string registration = passport.RegistrationId == 0 ? "Регистрация не указана" : passport.Registration.ToString();
            Console.Clear();
            return @$"
        Серия и номер паспорта:   {passport.Number}
        Орган выдавший паспорт:   {passport.IssuedBy}
        Дата выдачи паспорта:     {passport.IssueDate.ToShortDateString()}
        Адрес регистрации:        {registration}
        ";
        }

        // PassportsMUI
        public static readonly string number = "Введите серию и номер паспорта без пробелов";
        public static readonly string date = "Когда выдан паспорт?";
        public static readonly string addRegistration = "Указать адрес регистрации?";
        public static readonly string passportNotAdded = "ПАСПОРТ НЕ ДОБАВЛЕН";
        public static readonly string savePassport = "Сохранить паспортные данные?";
        public static readonly List<string> changeMenu = new() {
                                "Изменить номер паспорта",
                                "Изменить место выдачи паспорта",
                                "Изменить дату выдачи паспорта",
                                "Изменить адрес регистрации",
                                "Сохранить или вернуться в предыдущее меню"};
        public static readonly string changeNumber = "Введите новую серию и номер паспорта без пробелов или оставьте поле пустым, если не хотите менять ";
        public static readonly string changeDate = "Введите новую дату выдачи в формате dd-mm-yy или оставьте поле пустым, если не хотите менять ";

        //PassportsUI

        public static readonly string menuName = "ПАСПОРТА";
        public static readonly string searchString = "Давайте поищем паспорт. Введите часть номера или оставьте поле пустым для показа всех вариантов";
        public static readonly string exist = "ТАКОЙ ПАСПОРТ УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly string added = "ПАСПОРТ ДОБАВЛЕН";
        public static readonly string notAdded = "ПАСПОРТ НЕ ДОБАВЛЕН";
        public static readonly string choosen = "ПАСПОРТ ВЫБРАН";
        public static readonly string notChoosen = "ПАСПОРТ НЕ ВЫБРАН";
        public static readonly List<string> saveOptions = new() {
                                "Сохранить паспорт",
                                "Изменить паспорт",
                                "Не сохранять паспорт"};

        public static readonly string changed = "ПАСПОРТ ИЗМЕНЕН";
        public static readonly string changeCancel = "ПАСПОРТ НЕ ИЗМЕНЕН";
        public static readonly string deleted = "ПАСПОРТ УДАЛЕН";
        public static readonly string delCancel = "ПАСПОРТ НЕ УДАЛЕН";

    }
}