using Models;

namespace MenusAndChoices
{
    public static class RepresentativeText
    {
        public static string Summary(Representative representative)
        {
            Console.Clear();
            return @$"
        ФИО:                      {representative}
        Серия и номер паспорта:   {representative.Passport.Number}
        Орган выдавший паспорт:   {representative.Passport.IssuedId}
        Дата выдачи паспорта:     {representative.Passport.IssueDate}
        Адрес регистрации:        {representative.Passport.Registration}
        ";
        }
    }
}