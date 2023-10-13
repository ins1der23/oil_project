using static InOut;
using MenusAndChoices;
using Models;
using Controller;

namespace Handbooks

{
    public class ClientControl
    {
        public static async Task Start()
        {
            var user = Settings.user;
            var clientList = new Clients();
            var clientToChange = new Client();
            bool mainFlag = true;
            while (mainFlag)
            {
                var choice = MenuToChoice(ClientText.findClient, ClientText.menuName, Text.choice); // Меню поиска
                switch (choice)
                {
                    case 1:  // Найти клиента
                        bool levelOneFlag = true;
                        while (levelOneFlag)
                        {
                            clientList = await FindClients.Start();
                            choice = MenuToChoice(clientList.ToStringList(), ClientText.clientFound, Text.choiceOrEmpty);
                            if (choice != 0)
                            {
                                clientToChange = clientList.GetFromList(choice);
                                levelOneFlag = false;
                            }
                            else
                            {
                                choice = MenuToChoice(Text.searchAgainOrAdd, invite: Text.choice);
                                switch (choice)
                                {
                                    case 0: // Повторить поиск
                                        break;
                                    case 2: // добавить клиента
                                        var clientNew = await AddClient.Start();
                                        if (clientNew.Name != String.Empty)
                                        {
                                            clientToChange = clientNew;
                                            levelOneFlag = false;
                                        }
                                        break;
                                    case 3: // возврат в предыдущее меню
                                        levelOneFlag = false;
                                        break;
                                }
                            }
                        }
                        if (clientToChange.AddressId != 0)
                        {
                            levelOneFlag = true;
                            while (levelOneFlag)
                            {
                                ShowString(ClientText.Summary(clientToChange));
                                choice = MenuToChoice(ClientText.options, invite: Text.choice, clear: false); // Меню выбора клиента
                                switch (choice)
                                {
                                    case 0: // показ Summary по клиенту
                                        ShowString(ClientText.Summary(clientToChange));
                                        break;
                                    case 4: //А что с договорами?
                                        await AgrControl.Start(clientToChange);
                                        break;
                                    case 5: //Удалить клиента
                                        bool delCheck = await DelClient.Start(clientToChange);
                                        if (delCheck) levelOneFlag = false;
                                        break;
                                    case 6: // Вернуться к поиску клиента
                                        levelOneFlag = false;
                                        break;
                                }
                            }
                        }
                        break;
                    case 2: // Возврат в главное меню
                        return;
                }
            }

        }

    }
}

