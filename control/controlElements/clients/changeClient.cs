using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class ChangeClient
    {
        public static async Task<Client> Start(Client client, bool toSql)
        {
            Client clientOld = (Client)client.Clone();
            string name = GetString(ClientText.changeName);
            Address address = new();
            int choice = await MenuToChoice(Text.yesOrNo, AddrText.changeAddress, Text.choice); // Изменить адрес?
            switch (choice)
            {
                case 1: // Да
                    await ShowString(AddrText.addressChoosing, delay: 100);
                    address = await AddressControl.Start();
                    break;
                case 2: // Нет
                    break;
            }
            double phone = GetDouble(ClientText.changePhone);
            string comment = GetString(ClientText.changeComment);
            client.Change(name, address, phone, comment);
            await ShowString(client.Summary(), delay: 100);
            if (client.SearchString != clientOld.SearchString)
            {
                choice = await MenuToChoice(Text.yesOrNo, ClientText.confirmChanges, Text.choice, clear: false, noNull: true);
                if (choice == 1)
                {
                    Clients clients = new();
                    var user = Settings.User;
                    bool exist = await clients.CheckExist(user, client);
                    if (exist) await ShowString(ClientText.clientExist);
                    else
                    {
                        await ShowString(ClientText.clientChanged);
                        if (toSql) client = await clients.SaveChanges(user, client);
                        return client;
                    }
                }
            }
            await ShowString(ClientText.clientNotChanged);
            return clientOld;
        }
    }
}