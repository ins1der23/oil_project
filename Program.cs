using System;
using static Client;
// Worker chief1 = new Worker("Михаил", "Задорин", (1982,8,23));
// Worker chief2 = new Worker("Константин", "Семигук", (1982,4,29));
// Console.WriteLine(chief1);
// Console.WriteLine(chief2);

// Position chief = new Position("Директор");
// Position factoryChief = new Position ("Директор производства");
// Position manager = new Position ("Менеджер");
// Position driver = new Position("Водитель");

// Console.WriteLine(chief);
// Console.WriteLine(factoryChief);
// Console.WriteLine(manager);
// Console.WriteLine(driver);

// chief1.name = "Миша";
// Console.WriteLine(chief1);

Client client1 = new Client("Екатеринбург", "+79221700106", "Прачечная", "Миша");
Console.WriteLine(client1);
Client.ChangeFields(client1,"", "", "Хуячечная", "Вася");
Console.WriteLine(client1);
Client client2 = new Client("Березовский", "+79224324", "Цирк", "Юра");
Client.AddAgreement(client2, "f:/path");

Client.ChangeAgreement(client2, "c:/path");

Client.DelAgreement(client2);




