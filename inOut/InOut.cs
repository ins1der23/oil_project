using System.Collections.Generic;
using System;
using Models;
using System.Linq;
using MenusAndChoices;


public static class InOut
{
    public static void ShowMenu(this Menu menu) => Console.WriteLine(menu);
    public static void ShowString(string text) => Console.WriteLine(text);

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
    public static int MenuToChoice(List<string> menuChoices, string menuName = "", string invite = "", bool clear = true)
    {
        if (clear) Console.Clear();
        var menu = new Menu(menuChoices, menuName);
        menu.ShowMenu();
        if (menuChoices.Count() == 0) ShowString(Text.notFound);
        return menu.MenuChoice(invite);
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
        DateTime date = new DateTime();
        bool flag = true;
        do
        {
            Console.Write($"{text}: ");
            flag = DateTime.TryParse(Console.ReadLine(), out date);
        } while (!flag);
        return date;
    }

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
}