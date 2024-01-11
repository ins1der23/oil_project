using MenusAndChoices;
using Models;
using UserInterface;

namespace Handbooks

{
    public class ClientControl
    {
        public static async Task Start()
        {
            var clientToChange = new Client();
            bool mainFlag = true;
            while (mainFlag)
            {
                int choice = await MenuToChoice(ClientText.findClient, ClientText.menuName, CommonText.choice); // Меню поиска
                switch (choice)
                {
                    case 1:  // Найти клиента
                        bool levelOneFlag = true;
                        while (levelOneFlag)
                        {
                            Clients clientList = await FindClients.Start();
                            choice = await MenuToChoice(clientList.ToStringList(), ClientText.clientFound, CommonText.choiceOrEmpty);
                            if (choice != 0)
                            {
                                clientToChange = clientList.GetFromList(choice);
                                levelOneFlag = false;
                            }
                            else
                            {
                                choice = await MenuToChoice(ClientText.searchAgainOrAddClient, invite: CommonText.choice);
                                switch (choice)
                                {
                                    case 0: // Повторить поиск
                                        break;
                                    case 2: // добавить клиента
                                        var clientNew = await AddClient.Start();
                                        if (clientNew.Name != String.Empty) clientToChange = clientNew;
                                        levelOneFlag = false;
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
                                await ShowString(clientToChange.SummaryText(), delay: 100);
                                choice = await MenuToChoice(ClientText.options, invite: CommonText.choice, clear: false, noNull: true); // Меню выбора клиента
                                switch (choice)
                                {
                                    case 3: // Изменить данные клиента
                                        clientToChange = await ChangeClient.Start(clientToChange, toSql: true);
                                        break;
                                    case 4: //Создать новый договор или посмотреть существующий
                                        await AgrControl.Start(clientToChange);
                                        break;
                                    case 5: //Удалить клиента
                                        clientToChange = await DelClient.Start(clientToChange);
                                        if (clientToChange.Id == 0)
                                        {
                                            levelOneFlag = false;
                                            await ShowString(ClientText.returnToSearch);
                                        }
                                        break;
                                    case 6: // Вернуться к поиску клиента
                                        levelOneFlag = false;
                                        await ShowString(ClientText.returnToSearch);
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

