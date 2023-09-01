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
                                "Тест",
                                "Выход"};
    public static List<string> claimMenu = new List<string>() {
                                "Добавить заявку",
                                "Найти заявку",
                                "Возврат"};

    // Common
    public static string choice = "Выберете вариант";
    public static List<string> showOrFind = new List<string>() {
                                "Показать всё",
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
    // Clients
    public static List<string> client = new List<string>() {
                                "Добавить заявку",
                                "Показать все заявки",
                                "Изменить данные клиента",
                                "Договоры",
                                "Возврат"};

    // Adresses
    public static string addrName = "Введите название";
    public static string houseNum = "Введите номер дома";


    // Agreements
    public static List<string> agreement = new List<string>() {
                                "Прикрепить скан",
                                "Посмотреть скан",
                                "Создать договор",
                                "Распечатать договор",
                                "Изменить договор",
                                "Возврат"};

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


