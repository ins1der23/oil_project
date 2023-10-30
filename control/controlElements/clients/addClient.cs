using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    class AddClient
    {
        public static async Task<Client> Start()
        {
            var user = Settings.User;
            var clientToAdd = new Client();
            int choice;
            do
            {
                clientToAdd.Name = GetString(ClientText.nameRestrict);
            } while (clientToAdd.Name == string.Empty);
            await ShowString(AddrText.addressChoosing, delay: 100);
            bool flag = true;
            while (flag)
            {
                Address address = await FindAddress.Start();
                if (address.Id != 0)
                {
                    clientToAdd.AddressId = address.Id;
                    clientToAdd.Address = address;
                    await ShowString(AddrText.addressChoosen);
                    flag = false;
                }
                else
                {
                    bool levOneFlag = true;
                    while (levOneFlag)
                    {
                        choice = await MenuToChoice(AddrText.searchAgainOrAddAddress, AddrText.addressNotChoosen, Text.choice);
                        switch (choice)
                        {
                            case 1: // Повторить поиск
                                levOneFlag = false;
                                break;
                            case 2: // Добавить адрес
                                address = await AddAddress.Start();
                                if (address.Id != 0)
                                {
                                    clientToAdd.AddressId = address.Id;
                                    clientToAdd.Address = address;
                                    levOneFlag = false;
                                    flag = false;
                                }
                                break;
                            case 3: // Возврат в предыдущее меню
                                await ShowString(ClientText.clientNotAdded);
                                return new Client();
                        }
                    }

                }
            }
            clientToAdd.Phone = GetDouble(ClientText.inputPhone);
            clientToAdd.OwnerId = user.UserId;
            clientToAdd.Comment = GetString(ClientText.inputComment);
            await ShowString(clientToAdd.Summary(), delay: 100);
            choice = await MenuToChoice(Text.yesOrNo, ClientText.saveClient, Text.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                var clientList = new Clients();
                clientToAdd = await clientList.SaveGetId(user, clientToAdd);
                await ShowString(ClientText.clientAdded);
                return clientToAdd;
            }
            await ShowString(ClientText.clientNotAdded);
            return new Client();
        }
    }
}
