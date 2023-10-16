using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Handbooks

{
    public class ChangeClient
    {
        public static async Task<Client> Start(Client client)
        {
            var user = Settings.user;
            string name = GetString(ClientText.changeName);
            Address address = new Address();
            int choice = MenuToChoice(Text.yesOrNo, ClientText.changeAddress, Text.choice); // Изменить адрес?
            switch (choice)
            {
                case 1: // Да
                    ShowString(ClientText.addressChoosing);
                    bool flag = true;
                    while (flag)
                    {
                        address = await FindAddress.Start();
                        if (address.Id != 0)
                        {
                            ShowString(ClientText.addressChoosen);
                            await Task.Delay(1000);
                            flag = false;
                        }
                        else
                        {
                            choice = MenuToChoice(AddrText.searchAgainOrAddAddress, ClientText.addressNotChoosen, Text.choice); // Повторить поиск или добавить адрес
                            switch (choice)
                            {
                                case 1: // Повторить поиск
                                    break;
                                case 2: // Добавить
                                    address = await AddAddress.Start();
                                    if (address.Id != 0) flag = false;
                                    ShowString(ClientText.addressChoosen);
                                    await Task.Delay(1000);
                                    break;
                                case 3: // Возврат в предыдущее меню
                                    ShowString(ClientText.addressNotChanged, true);
                                    await Task.Delay(1000);
                                    flag = false;
                                    break;
                            }
                        }
                    }
                    break;
                case 2: // Нет
                    break;
            }
            double phone = GetDouble(ClientText.changePhone);
            string comment = GetString(ClientText.changeComment);
            client.Change(name, address, phone, comment);
            ShowString(ClientText.Summary(client));
            InOut.MenuToChoice(Text.yesOrNo, ClientText.confirmChanges, Text.choice, false);
            switch (choice)
            {
                case 1:
                    var clientList = new Clients();
                    await clientList.SaveChanges(user, client);
                    ShowString(ClientText.clientChanged);
                    await Task.Delay(1000);
                    return client;
                case 2:
                    ShowString(ClientText.clientNotChanged);
                    await Task.Delay(1000);
                    return client;
            }
            return client;

        }
    }
}