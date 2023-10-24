using System;
using Models;

namespace MenusAndChoices
{
    public static class PassportText
    {
        public static string Summary(Passport passport)
        {
            Console.Clear();
            return @$"
        Серия и номер паспорта:   {passport.Serial} {passport.Number}
        Орган выдавший паспорт:   {passport.IssueAuthority}
        Дата выдачи паспорта:     {passport.IssueDate}
        Адрес регистрации:        {passport.Registration.RegAddress}
        ";
        }


        // AddPasport
        public static readonly string serial = "Введите серию паспорта";
        public static readonly string number = "Введите номер паспорта";
        public static readonly string date = "Когда выдан паспорт?";
        public static readonly string authority = "Кем выдан паспорт?";
        public static readonly string addRegistration = "Указать адрес регистрации?";
        public static readonly string passportNotAdded = "ПАСПОРТ НЕ ДОБАВЛЕН";
        public static readonly string savePassport = "Сохранить паспортные данные?";



    }
}