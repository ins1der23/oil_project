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
using System.Diagnostics;


namespace Testing
{
    public class Test
    {
        public static async Task Start()
        {
            string filePath = @"C:\Users\Миша\Pictures\i.jpg";
            string name = "explorer.exe";
            string argument = "/separate";
            var process = new StartProcess(name, filePath, argument);
            await Task.Delay(1000);
        }

    }
}
