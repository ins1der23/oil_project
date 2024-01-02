
namespace MenusAndChoices
{
    public static class CommonText
    {
        public static readonly string[] menuNames = new string[] {
                                "Главное меню",
                                "Клиенты",
                                "Заявки",
                                "Сотрудники",
                                "Должности"};
        public static readonly List<string> mainMenu = new() {
                                "Клиенты",
                                "Заявки",
                                "Расписание",
                                "Сотрудники",
                                "Тест",
                                "Выход"};
        public static readonly List<string> claimMenu = new() {
                                "Добавить заявку",
                                "Найти заявку",
                                "Возврат"};

        // Common

        public static readonly string itemsFound = "Найденные варианты";
        public static readonly string inputError = "Неверный ввод";
        public static readonly string choice = "Выберете вариант";
        public static readonly string saveError = "Ошибка записи";
        public static readonly string choiceOrEmpty = "Выберете вариант или просто нажмите Enter, если нет подходящего";
        public static readonly string inputName = "Введите название";
        public static readonly string notFound = "\nНе найдено\n";
        public static readonly string chooseSome = "Выбрать?";
        public static readonly string emptyList = "Список пустой";
        public static readonly string searchString =
        "Введите текст для поиска или оставьте поле пустым для показа всех";

        public static readonly string returnToSearch = "ВОЗВРАЩАЕМСЯ К ПОИСКУ";
        public static readonly string changeName = "Введите новое название или оставьте поле пустым, если не хотите менять";
        public static readonly string changeConfirm = "Данные были изменены. Сохранить изменения?";
        public static readonly string delConfirm = "ТОЧНО УДАЛИТЬ?";

        public static readonly List<string> options = new() {
                                "Выбрать ",
                                "Изменить",
                                "Удалить",
                                "Вернуться к поиску",
                                "Вернуться в предыдущее меню"};

        public static readonly List<string> searchAgainMenu = new() {
                                "Повторить поиск",
                                "Добавить",
                                "Возврат в предыдущее меню"};

        public static readonly List<string> showOrFind = new() {
                                "Показать всё",
                                "Найти",
                                "Возврат в главное меню"};
        public static readonly List<string> addOrchoose = new() {
                                "Выбрать",
                                "Добавить",
                                "Возврат",
                                "Выход"};
        public static readonly List<string> choose = new() {
                                "Выбрать",
                                "Возврат"};
        public static readonly List<string> changeOrDelete = new() {
                                "Изменить",
                                "Удалить",
                                "Возврат"};
        public static readonly List<string> yesOrNo = new() {
                                "Да",
                                "Нет"};

        public static readonly string notAvailable = "НЕДОСТУПНО";




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
