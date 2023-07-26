using System.Linq;
using static InOut;
using System;

public class Menu
{
    string name { get; set; }
    List<string> choices { get; set; }

    public Menu(string menuName, List<string> menu)
    {
        this.name = menuName;
        this.choices = menu;
    }

    public static int MenuChoice(Menu menu, string menuChoice)
    {
        while (true)
        {
            int choice = GetInteger(menuChoice);
            if (choice > 0 && choice <= menu.choices.Count) return choice;
        }
    }

    public override string ToString()
    {
        string output = String.Empty;
        for (int i = 0; i < this.choices.Count; i++)
            output += $"{i+1}. {this.choices[i]}\n";
        return $"{this.name}\n{output}";
    }
}