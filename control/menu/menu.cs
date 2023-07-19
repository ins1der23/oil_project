using static InOut;
using System;

public class Menu
{
    string name { get; set; }
    string[] choices { get; set; }

    public Menu(string[] menu)
    {
        this.name = menu[0];
        this.choices = menu;
    }

    public static int MenuChoice(Menu menu,string menuChoice)
    {
        while (true)
        {
            int choice = GetInteger(menuChoice);
            if (choice > 0 && choice < menu.choices.Length) return choice;
        }
        
    }

    public override string ToString()
    {
        string output = String.Empty;
        for (int i = 1; i < this.choices.Length; i++)
            output += $"{i}. {this.choices[i]}\n";
        return $"{this.name}\n{output}";
    }
}