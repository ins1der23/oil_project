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


namespace Testing
{
    public class Test
    {
        public static async Task Start()
        {
            Client client = new();
            client.Name = "Лучший друган-алкаш";
            await AgrControl.Start(client);



        }

    }
}
