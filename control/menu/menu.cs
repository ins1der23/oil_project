using System.Linq;
using static InOut;
using System;

namespace Models
{
    public class Menu
    {
        public string Name { get; set; }
        public List<string> Choices { get; set; }
        public Menu(List<string> choices, string name = "")
        {
            Name = name;
            Choices = choices;
        }
        public int MenuChoice(string menuChoice)
        {
            while (true)
            {
                int choice = GetInteger(menuChoice);
                if (choice > 0 && choice <= Choices.Count) return choice;
            }
        }
        public override string ToString()
        {
            string output = String.Empty;
            for (int i = 0; i < this.Choices.Count; i++)
                output += $"{i + 1}. {this.Choices[i]}\n";
            return $"{this.Name}\n{output}";
        }
    }
}
