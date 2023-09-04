using System.Collections.Generic;
using System;
using Models;

public static class InOut
{
    public static void ShowMenu(this Menu menu) => Console.WriteLine(menu);
    public static void ShowString(string text) => Console.WriteLine(text);

    public static void ShowStringList(this List<string> someList)
    {
        foreach (var item in someList)
            Console.WriteLine($"{someList.IndexOf(item) + 1}. {item}");
    }

    public static int MenuToChoice(List<string> menuChoices, string menuName = "")
    {
        var menu = new Menu(menuChoices, menuName);
        menu.ShowMenu();
        return menu.MenuChoice(MenuText.choice);
    }
        public static int GetInteger(string text)
    {
        int num = 0;
        bool flag = true;
        do
        {
            Console.Write($"{text}: ");
            flag = int.TryParse(Console.ReadLine(), out num);
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
        Console.Write($"{text}: ");
        output += Console.ReadLine();
        return output;
    }

}