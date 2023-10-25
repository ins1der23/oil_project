using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;

namespace Handbooks

{
    public class ChangeClient
    {
        public static async Task<Client> Start(Client client)
        {
            var clientOld = client;
            var user = Settings.User;
            string name = GetString(ClientText.changeName);
            Address address = new();
            int choice = MenuToChoice(Text.yesOrNo, AddrText.changeAddress, Text.choice); // Изменить адрес?
            switch (choice)
            {
                case 1: // Да
                    ShowString(AddrText.addressChoosing);
                    bool flag = true;
                    while (flag)
                    {
                        address = await FindAddress.Start();
                        if (address.Id != 0)
                        {
                            ShowString(AddrText.addressChoosen);
                            await Task.Delay(1000);
                            flag = false;
                        }
                        else
                        {
                            choice = MenuToChoice(AddrText.searchAgainOrAddAddress, AddrText.addressNotChoosen, Text.choice); // Повторить поиск или добавить адрес
                            switch (choice)
                            {
                                case 1: // Повторить поиск
                                    break;
                                case 2: // Добавить
                                    address = await AddAddress.Start();
                                    if (address.Id != 0) flag = false;
                                    ShowString(AddrText.addressChoosen);
                                    await Task.Delay(1000);
                                    break;
                                case 3: // Возврат в предыдущее меню
                                    ShowString(AddrText.addressNotChanged, true);
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
                    return clientOld;
            }
            return client;

        }
    }
}