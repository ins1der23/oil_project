using static InOut;
using MenusAndChoices;
using Handbooks;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;
using System.Linq;
using System.IO;
using System.Collections;
using FileWork;

namespace Testing
{
    public class Test
    {
        public static async Task Start()
        {
           await MainSettings.Set();
           Console.WriteLine(Setters.numResetter);
           Console.ReadLine();
           
           




        }
    }
}
