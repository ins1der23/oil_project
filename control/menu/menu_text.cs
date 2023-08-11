using System;
public static class MenuText
{
    public static string menuChoice = "Выберете вариант";
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
                                "Сохранить изменения",
                                "Выход"};
    public static List<string> clientMenu = new List<string>() {
                                "Показать всех клиентов",
                                "Добавить клиента",
                                "Найти клиента",
                                "Изменить клиента",
                                "Удалить клиента",
                                "Возврат"};
    public static List<string> claimMenu = new List<string>() {
                                "Добавить заявку",
                                "Найти заявку",
                                "Возврат"};
    public static List<string> workerMenu = new List<string>() {
                                "Добавить сотрудника",
                                "Показать всех сотрудников",
                                "Найти сотрудника",
                                "Возврат"};

    public static List<string> choiceMenu = new List<string>() {
                                "Выбрать запись",
                                "Возврат"};
    public static List<string> changeMenu = new List<string>() {
                                "Изменить запись",
                                "Удалить запись",
                                "Возврат"};

    public static List<string> yesMenu = new List<string>() {
                                "Да",
                                "Нет"};
    public static string setPosition = "Назначить должность?";

    public static string workerName = "Введите имя";
    public static string workerSurname = "Введите фамилию";
    public static string workerBirth = "Введите дату рождения Yyyy-mm-dd";

    public static string userName = "Введите имя ползователя ";
    public static string password = "Введите пароль ";
    public static string inputError = "Неверный ввод";



}


