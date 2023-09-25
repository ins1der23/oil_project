using System;
using Models;

namespace MenusAndChoices
{
    public static class Text
    {
        public static string[] menuNames = new string[] {
                                "Главное меню",
                                "Клиенты",
                                "Заявки",
                                "Сотрудники",
                                "Должности"};
        public static List<string> mainMenu = new List<string>() {
                                "Клиенты",
                                "Заявки",
                                "Расписание",
                                "Сотрудники",
                                "Тест",
                                "Выход"};
        public static List<string> claimMenu = new List<string>() {
                                "Добавить заявку",
                                "Найти заявку",
                                "Возврат"};

        // Common

        public static string connected = "Connected!";

        public static string inputError = "Неверный ввод";
        public static string choice = "Выберете вариант";
        public static string choiceOrEmpty = "Выберете вариант или просто нажмите Enter, если нет подходящего";
        public static string inputName = "Введите название";
        public static string notFound = "\nНе найдено\n";
        public static string addSome = "Добавить?";
        public static string added = "Успешно добавлено";
        public static string chooseSome = "Выбрать?";
        public static string searchString =
        "Введите текст для поиска или оставьте поле пустым для показа всех";

        public static List<string> findSome = new List<string>() {
                                "Найти",
                                "Возврат в главное меню"};
        public static List<string> showOrFind = new List<string>() {
                                "Показать всё",
                                "Найти",
                                "Возврат в главное меню"};
        public static List<string> searchAgain = new List<string>() {
                                "Повторить поиск",
                                "Возврат в главное меню"};
        public static List<string> chooseOrSearchAgain = new List<string>() {
                                "Выбрать добавленное",
                                "Повторить поиск",
                                "Возврат в главное меню"};
        public static List<string> searchAgainOrAdd = new List<string>() {
                                "Повторить поиск",
                                "Добавить",
                                "Возврат в предыдущее меню"};
        public static List<string> addOrchoose = new List<string>() {
                                "Выбрать",
                                "Добавить",
                                "Возврат",
                                "Выход"};
        public static List<string> choose = new List<string>() {
                                "Выбрать",
                                "Возврат"};
        public static List<string> changeOrDelete = new List<string>() {
                                "Изменить",
                                "Удалить",
                                "Возврат"};
        public static List<string> yesOrNo = new List<string>() {
                                "Да",
                                "Нет"};
        public static List<string> yesNoOrExit = new List<string>() {
                                "Да",
                                "Нет",
                                "Выход"};
        // Clients

        // Adresses
        public static string addressHead = "Добавьте адрес";
        public static string addAddress = "Добавить новый адрес?";
        public static string cityName =
        "Введите название города или оставьте поле пустым для показа всех";
        public static string districtName =
        "Введите название района или оставьте поле пустым для показа всех";
        public static string locationName =
        "Введите название микрорайона или оставьте поле пустым для показа всех";
        public static string streetName =
        "Введите название улицы или оставьте поле пустым для показа всех";
        public static string houseNum = "Введите номер дома";

        // Agreements


        // Workers
        public static string setPosition = "Назначить должность?";
        public static string workerName = "Введите имя";
        public static string workerSurname = "Введите фамилию";
        public static string workerBirth = "Введите дату рождения Yyyy-mm-dd";

        // Connection
        public static string userName = "Введите имя пользователя";
        public static string password = "Введите пароль";
        public static string isConnect = "Соединение установлено";
    }



}
