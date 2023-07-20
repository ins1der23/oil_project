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

}