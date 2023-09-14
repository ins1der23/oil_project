using System;
using Models;

namespace MenusAndChoices
{
    public static class ClientText
    {
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

        public static List<string> options = new List<string>() {
                                "Добавить заявку",
                                "Показать все заявки",
                                "Изменить данные клиента",
                                "Добавить договор",
                                "Показать договор",
                                "Вернуться к поиску клиента"};
    }
}