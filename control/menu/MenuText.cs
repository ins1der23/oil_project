using System;
public static class MenuText
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

    // Common
    public static string choice = "Выберете вариант";
    public static List<string> showOrFind = new List<string>() {
                                "Показать",
                                "Найти",
                                "Возврат"};

    public static List<string> addOrchoose = new List<string>() {
                                "Добавить",
                                "Выбрать",
                                "Возврат"};

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
    // Workers
    public static string setPosition = "Назначить должность?";
    public static string workerName = "Введите имя";
    public static string workerSurname = "Введите фамилию";
    public static string workerBirth = "Введите дату рождения Yyyy-mm-dd";

    // Connection
    public static string userName = "Введите имя пользователя";
    public static string password = "Введите пароль";
    public static string inputError = "Неверный ввод";
    public static string isConnect = "Соединение установлено";
}


