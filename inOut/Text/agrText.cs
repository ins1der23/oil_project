using System;
using Models;

namespace MenusAndChoices
{
    public static class AgrText
    {
        public static List<string> options = new List<string>() {
                                "Прикрепить скан",
                                "Показать скан",
                                "Распечатать договор",
                                "Изменить договор",
                                "Создать новый договор",
                                "Ничего не делать"};

        public static string scanPath = "Укажите путь к скану договора";
        public static string Summary(this Agreement agreement)
        {
            Console.Clear();
            if (agreement == null) return "Договор отсутствует";
            DateTime date = agreement.Date;
            string scanCheck = agreement.ScanCheck == true ? "Есть" : "Отсутствует";
            return @$"
        Название договора:        {agreement.Name}
        Дата договора             {date}
        Название клиента:         {agreement.Client.FullName}
        Скан договора             {scanCheck}
        ";
        }





    }
}