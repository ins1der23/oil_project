using System;
using Models;

namespace MenusAndChoices
{
    public static class ClientText
    {
        // ClientControl

        public static string menuName = "КЛИЕНТЫ";
        public static List<string> findClient = new List<string>() {
                                "Поиск клиента",
                                "Возврат в главное меню"};

        public static string clientFound = "Найденные клиенты";
        public static List<string> options = new List<string>() {
                                "Добавить заявку",
                                "Показать все заявки",
                                "Изменить данные клиента",
                                "Создать новый договор или посмотреть существующий",
                                "Удалить клиента",
                                "Вернуться к поиску клиента"};

        public static List<string> searchAgainOrAddClient = new List<string>() {
                                "Повторить поиск клиента",
                                "Добавить клиента",
                                "Возврат в предыдущее меню"};
        public static string Summary(Client client)
        {
            Console.Clear();
            string yesOrNo = client.AgreementCheck == true ? "Есть" : "Отсутствует";
            return @$"
        Название клиента:         {client.Name}
        Адрес клиента:            {client.Address.FullAddress}
        Телефон клиента:          {client.Phone}
        Комментарий:              {client.Comment}
        Договор:                  {yesOrNo}
        Ответственный сотрудник:  {client.Owner.FullName}
        ";
        }

        // AddClient
        public static string nameRestrict = "ОБЯЗАТЕЛЬНО введите название клиента";
        public static string addressChoosing = "\nВЫБОР АДРЕСА";
        public static string addressNotChoosen = "АДРЕС НЕ ВЫБРАН";
        public static string clientNotAdded = "КЛИЕНТ НЕ ДОБАВЛЕН";
        public static string inputPhone = "Введите телефон";
        public static string inputComment = "Введите комментарий";
        public static string saveClient = "Сохранить клиента?";
        public static string clientAdded = "КЛИЕНТ УСПЕШНО ДОБАВЛЕН";

        // ChangeClient

        public static string changeName = "Введите новое название клиента или оставьте поле пустым, если не хотите менять";
        public static string changeAddress = "Изменить адрес?";
        public static string addressChoosen = "АДРЕС ВЫБРАН";
        public static string addressNotChanged = "АДРЕС НЕ ИЗМЕНЕН";
        public static string changePhone = "Введите новый телефон или оставьте поле пустым, если не хотите менять";
        public static string changeComment = "Введите новый комментарий или оставьте поле пустым, если не хотите менять";
        public static string confirmChanges = "Применить изменения?";
        public static string clientChanged = "КЛИЕНТ ИЗМЕНЕН";
        public static string clientNotChanged = "КЛИЕНТ НЕ ИЗМЕНЕН";

        // DelClient
        public static string delClient = "ТОЧНО УДАЛИТЬ ЭТОГО КЛИЕНТА???";
        public static string clientDeleted = "КЛИЕНТ УСПЕШНО УДАЛЕН";


    }
}