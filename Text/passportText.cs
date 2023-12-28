using Models;

namespace MenusAndChoices
{
    public static class PassportText
    {
        public static string Summary(Passport passport)
        {
            Console.Clear();
            return @$"
        Серия и номер паспорта:   {passport.Number}
        Орган выдавший паспорт:   {passport.IssuedId}
        Дата выдачи паспорта:     {passport.IssueDate}
        Адрес регистрации:        {passport.Registration}
        ";
        }


        // AddPasport
        public static readonly string number = "Введите серию и номер паспорта без пробелов";
        public static readonly string date = "Когда выдан паспорт?";
        public static readonly string addRegistration = "Указать адрес регистрации?";
        public static readonly string passportNotAdded = "ПАСПОРТ НЕ ДОБАВЛЕН";
        public static readonly string savePassport = "Сохранить паспортные данные?";



    }
}