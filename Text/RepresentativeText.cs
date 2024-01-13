using Models;

namespace MenusAndChoices
{
    public static class RepresentativeText
    {
        public static string Summary(Representative representative)
        {
            string registration = representative.Passport.RegistrationId == 0 
            ? "Регистрация не указана" : representative.Passport.Registration.ShortString();
            string client = representative.ClientId == 0 
            ? "Клиент не назначен" : representative.Client.Address.ShortString();
            Console.Clear();
            return @$"
        ФИО:                      {representative}
        Клиент:                   {client}
        Серия и номер паспорта:   {representative.Passport.Number}
        Орган выдавший паспорт:   {representative.Passport.IssuedBy}
        Дата выдачи паспорта:     {representative.Passport.IssueDate.ToShortDateString()}
        Адрес регистрации:        {registration}
        ";
        }

        // Registartions MUI
        public static readonly string name = "Введите имя";
        public static readonly string middlename = "Введите отчество";
        public static readonly string surname = "Введите фамилию";
        public static readonly List<string> changeMenu = new() {
                                "Изменить ФИО",
                                "Изменить паспортные данные",
                                "Сохранить или вернуться в предыдущее меню"};
        public static readonly string changeName = "Введите новое имя или оставьте поле пустым, если не хотите менять ";
        public static readonly string changeMiddlename = "Введите новое отчество или оставьте поле пустым, если не хотите менять ";
        public static readonly string changeSurname = "Введите новую фамилию или оставьте поле пустым, если не хотите менять ";


        //RegistrationsUI
        public static readonly string menuName = "ПРЕДСТАВИТЕЛИ";
        public static readonly string searchString = "Давайте поищем ФИО. Введите название или оставьте поле пустым для показа всех вариантов";
        public static readonly string exist = "ТАКОЙ ЧЕЛОВЕК УЖЕ ЕСТЬ В БАЗЕ";
        public static readonly string added = "ЧЕЛОВЕК ДОБАВЛЕН";
        public static readonly string notAdded = "ЧЕЛОВЕК НЕ ДОБАВЛЕН";
        public static readonly string choosen = "ЧЕЛОВЕК ВЫБРАН";
        public static readonly string notChoosen = "ЧЕЛОВЕК НЕ ВЫБРАН";
        public static readonly List<string> saveOptions = new() {
                                "Сохранить человека",
                                "Изменить человека",
                                "Не сохранять человека"};

        public static readonly string changed = "ЧЕЛОВЕК ИЗМЕНЕН";
        public static readonly string changeCancel = "ЧЕЛОВЕК НЕ ИЗМЕНЕН";
        public static readonly string deleted = "ЧЕЛОВЕК УДАЛЕН";
        public static readonly string delCancel = "ЧЕЛОВЕК НЕ УДАЛЕН";
    }
}