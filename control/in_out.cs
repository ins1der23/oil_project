using System;
public static class InOut
{
    public static void ShowMenu(Menu menu) =>  Console.WriteLine(menu);
    
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