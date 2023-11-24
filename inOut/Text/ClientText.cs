using Models;

namespace MenusAndChoices
{
    public static class ClientText
    {
        // ClientControl

        public static readonly string menuName = "КЛИЕНТЫ";
        public static readonly List<string> findClient = new List<string>() {
                                "Поиск клиента",
                                "Возврат в главное меню"};

        public static readonly string clientFound = "Найденные клиенты";
        public static readonly List<string> options = new List<string>() {
                                "Добавить заявку",
                                "Показать все заявки",
                                "Изменить данные клиента",
                                "Создать новый договор или посмотреть существующий",
                                "Удалить клиента",
                                "Вернуться к поиску клиента"};

        public static readonly List<string> searchAgainOrAddClient = new List<string>() {
                                "Повторить поиск клиента",
                                "Добавить клиента",
                                "Возврат в предыдущее меню"};
        public static string Summary(this Client client)
        {
            Console.Clear();
            string yesOrNo = client.AgreementCheck == true ? "Есть" : "Отсутствует";
            return @$"
        Название клиента:         {client.Name}
        Адрес клиента:            {client.Address.LongString}
        Телефон клиента:          {client.Phone}
        Комментарий:              {client.Comment}
        Договор:                  {yesOrNo}
        Ответственный сотрудник:  {client.Owner.FullName}
        ";
        }
        public static readonly string returnToSearch = "ВОЗВРАЩАЕМСЯ К ПОИСКУ КЛИЕНТА";


        // AddClient
        public static readonly string nameRestrict = "ОБЯЗАТЕЛЬНО введите название клиента";
        public static readonly string clientNotAdded = "КЛИЕНТ НЕ ДОБАВЛЕН";
        public static readonly string inputPhone = "Введите телефон";
        public static readonly string inputComment = "Введите комментарий";
        public static readonly string saveClient = "Сохранить клиента?";
        public static readonly string clientAdded = "КЛИЕНТ УСПЕШНО ДОБАВЛЕН";
        public static readonly string clientExist = "ТАКОЙ КЛИЕНТ УЖЕ ЕСТЬ В БАЗЕ";


        // ChangeClient

        public static readonly string changeName = "Введите новое название клиента или оставьте поле пустым, если не хотите менять";

        public static readonly string changePhone = "Введите новый телефон или оставьте поле пустым, если не хотите менять";
        public static readonly string changeComment = "Введите новый комментарий или оставьте поле пустым, если не хотите менять";
        public static readonly string confirmChanges = "Данные клиента были изменены. Сохранить изменения?";
        public static readonly string clientChanged = "КЛИЕНТ ИЗМЕНЕН";
        public static readonly string clientNotChanged = "КЛИЕНТ НЕ ИЗМЕНЕН";

        // DelClient
        public static readonly string delClient = "ТОЧНО УДАЛИТЬ ЭТОГО КЛИЕНТА???";
        public static readonly string clientDeleted = "КЛИЕНТ УСПЕШНО УДАЛЕН";
        public static readonly string clientNotDeleted = "КЛИЕНТ НЕ УДАЛЕН";


    }
}