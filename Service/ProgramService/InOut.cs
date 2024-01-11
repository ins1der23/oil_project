using Models;
using MenusAndChoices;
using System.Runtime.Versioning;

public static class InOut
{

    [SupportedOSPlatform("windows")]
    public static void SetWindowWidth(int width)
    {
        Console.WindowWidth = width;
    }

    public static void ShowMenu(this Menu menu) => Console.WriteLine(menu);
    /// <summary>
    /// Вывести строку в консоль
    /// </summary>
    /// <param name="text"> Текст для вывода</param>
    /// <param name="clear"> Очистить консоль </param>
    public static async Task ShowString(string text, bool clear = false, int delay = 1000)
    {
        if (clear) Console.Clear();
        Console.WriteLine(text);
        await Task.Delay(delay);
    }

    public static void ShowStringList(this List<string> someList)
    {
        foreach (var item in someList)
            Console.WriteLine($"{someList.IndexOf(item) + 1}. {item}");
    }

    /// <summary>
    /// Формирование и показ меню из списка string и возврат int значения выбора 
    /// </summary>
    /// <param name="menuChoices">Список для меню</param>
    /// <param name="menuName">Строка названия</param>
    /// <param name="invite">Строка ввода выбора</param>
    /// <param name="clear">Переключатель очистки экрана</param>
    /// <returns></returns>
    public static async Task<int> MenuToChoice(List<string> menuChoices, string menuName = "", string invite = "", bool clear = true, bool noNull = false)
    {
        if (clear) Console.Clear();
        var menu = new Menu(menuChoices, menuName);
        Console.WriteLine(menu);
        if (menuChoices.Count == 0) await ShowString(CommonText.notFound, delay: 100);
        if (noNull) return menu.MenuChoice(invite, noNull: true);
        else return menu.MenuChoice(invite);

    }

    public static T Wrap<T>(this object item)
    {
        try
        {
            return (T)item;
        }
        catch (Exception)
        {
            Console.WriteLine(CommonText.wrapError);
            throw;
        }
    }
    public static int GetInteger(string text)
    {
        int num = 0;
        bool flag = true;
        do
        {
            Console.Write($"{text}: ");
            flag = int.TryParse(Console.ReadLine(), out num) || num == 0;
        } while (!flag);
        return num;
    }

    public static double GetDouble(string text)
    {
        Console.Clear();
        double num = 0;
        bool flag = true;
        do
        {
            Console.Write($"{text}: ");
            flag = double.TryParse(Console.ReadLine(), out num) || num == 0;
        } while (!flag);
        return num;
    }

    public static async Task<double> GetDoubleAsync(string text, bool clear = true)
    {
        if (clear) Console.Clear();
        double num = 0;
        bool flag = true;
        do
        {
            Console.Write($"{text}: ");
            flag = await Task.Run(() => double.TryParse(Console.ReadLine(), out num)) || num == 0;
        } while (!flag);
        return num;
    }
    public static DateTime GetDate(string text)
    {
        Console.Clear();
        DateTime date = new();
        bool flag = true;
        do
        {
            Console.Write($"{text}: ");
            flag = DateTime.TryParse(Console.ReadLine(), out date);
        } while (!flag);
        return date;
    }

    public static async Task<DateTime> GetDateAsync(string text, bool clear = true)
    {
        if (clear) Console.Clear();
        DateTime date = new();
        bool flag = true;
        do
        {
            Console.Write($"{text}: ");
            flag = await Task.Run(() => DateTime.TryParse(Console.ReadLine(), out date)) || date == DateTime.MinValue;
        } while (!flag);
        return date;
    }


    /// <summary>
    /// Получение строки из консоли с приглашением ко вводу
    /// </summary>
    /// <param name="text"> Приглашение к вводу текста</param>
    /// <returns>Строка из консоли</returns>
    public static string GetString(string text, bool clear = true)
    {
        string output = string.Empty;
        if (clear) Console.Clear();
        Console.Write($"{text}: ");
        output += Console.ReadLine()!.Trim();
        return output;
    }
    public static async Task<string> GetStringAsync(string text, bool clear = true)
    {
        string output = string.Empty;
        if (clear) Console.Clear();
        Console.Write($"{text}: ");
        output += await Task.Run(() => Console.ReadLine()!.Trim());
        return output;
    }
    public static string PrepareToSearch(this string text) =>
    new string(text.Where(c => !char.IsPunctuation(c)).ToArray()).Replace(" ", "").ToLower();

    public static string PrepareToFileName(this string text)
    {
        string restrict = "<>:\"/\\|?*";
        text = new String(text.Select(ch => restrict.Contains(ch) ? '-' : ch).ToArray());
        return text;


    }
}