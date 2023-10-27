using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class ChangeClient
    {
        public static async Task<Client> Start(Client client)
        {
            Client clientOld = (Client)client.Clone();
            string name = GetString(ClientText.changeName);
            Address address = new();
            int choice = await MenuToChoice(Text.yesOrNo, AddrText.changeAddress, Text.choice); // Изменить адрес?
            switch (choice)
            {
                case 1: // Да
                    await ShowString(AddrText.addressChoosing, delay: 100);
                    bool flag = true;
                    while (flag)
                    {
                        address = await FindAddress.Start();
                        if (address.Id != 0)
                        {
                            await ShowString(AddrText.addressChoosen);
                            flag = false;
                        }
                        else
                        {
                            choice = await MenuToChoice(AddrText.searchAgainOrAddAddress, AddrText.addressNotChoosen, Text.choice); // Повторить поиск или добавить адрес
                            switch (choice)
                            {
                                case 1: // Повторить поиск
                                    break;
                                case 2: // Добавить
                                    address = await AddAddress.Start();
                                    if (address.Id != 0) flag = false;
                                    await ShowString(AddrText.addressChoosen);
                                    break;
                                case 3: // Возврат в предыдущее меню
                                    await ShowString(AddrText.addressNotChanged, true);
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
            await ShowString(client.Summary(), delay: 100);
            choice = await MenuToChoice(Text.yesOrNo, ClientText.confirmChanges, Text.choice, false);
            switch (choice)
            {
                case 1:
                    var clientList = new Clients();
                    var user = Settings.User;
                    client = await clientList.SaveChanges(user, client);
                    await ShowString(ClientText.clientChanged);
                    return client;
                case 2:
                    await ShowString(ClientText.clientNotChanged);
                    return clientOld;
            }
            return client;

        }
    }
}