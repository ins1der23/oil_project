using System;
using Models;

namespace MenusAndChoices
{
    public static class AgrText
    {
        public static readonly List<string> options = new List<string>() {
                                "Прикрепить скан",
                                "Показать скан",
                                "Изменить договор",
                                "Распечатать договор",
                                "Посмотреть все договоры",
                                "Ничего не делать"};

        public static readonly string scanPath = "Перетащите в это окно скан договора";
        public static readonly string noScan = "Скан договора отсутствует";
        public static readonly string changeName = "Введите новое название договора или оставьте поле пустым, если не хотите менять";
        public static readonly string changeDate = "Введите новую дату договора в формате dd-mm-yy";
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