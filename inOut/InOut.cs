using System.Collections.Generic;
using System;
using Models;
using System.Linq;
using MenusAndChoices;


public static class InOut
{
    public static void ShowMenu(this Menu menu) => Console.WriteLine(menu);
    /// <summary>
    /// Вывести строку в консоль
    /// </summary>
    /// <param name="text"> Текст для вывода</param>
    /// <param name="clear"> Очистить консоль </param>
    public static void ShowString(string text, bool clear = false)
    {
        if (clear) Console.Clear();
        Console.WriteLine(text);
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
    public static int MenuToChoice(List<string> menuChoices, string menuName = "", string invite = "", bool clear = true, bool noNull = false)
    {
        if (clear) Console.Clear();
        var menu = new Menu(menuChoices, menuName);
        Console.WriteLine(menu);
        if (menuChoices.Count() == 0) ShowString(Text.notFound);
        if (noNull) return menu.MenuChoice(invite, true);
        else return menu.MenuChoice(invite);

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
    public static DateTime GetDate(string text)
    {
        Console.Clear();
        DateTime date = new DateTime();
        bool flag = true;
        do
        {
            Console.Write($"{text}: ");
            flag = DateTime.TryParse(Console.ReadLine(), out date);
        } while (!flag);
        return date;
    }


    /// <summary>
    /// Получение строки из консоли с приглашением ко вводу
    /// </summary>
    /// <param name="text"> Приглашение к вводу текста</param>
    /// <returns>Строка из консоли</returns>
    public static string GetString(string text)
    {
        string output = String.Empty;
        Console.Clear();
        Console.Write($"{text}: ");
        output += Console.ReadLine()!.Trim();
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