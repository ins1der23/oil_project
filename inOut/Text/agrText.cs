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
                                "Посмотреть все договоры",
                                "Ничего не делать"};

        public static string scanPath = "Перетащите в это окно скан договора";
        public static string Summary(this Agreement agreement)
        {
            Console.Clear();
            if (agreement == null) return "Договор отсутствует";
            string date = agreement.Date.ToShortDateString();
            string scanCheck = agreement.ScanCheck == true ? "Есть" : "Отсутствует";
            return @$"
        Название договора:        {agreement.Name}
        Дата договора             {date}
        Название клиента:         {agreement.Client.ShortName}
        Скан договора             {scanCheck}
        ";
        }





    }
}