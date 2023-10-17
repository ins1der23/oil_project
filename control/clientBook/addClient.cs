using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    class AddClient
    {
        public static async Task<Client> Start()
        {
            var user = Settings.user;
            var clientToAdd = new Client();
            int choice;
            do
            {
                clientToAdd.Name = InOut.GetString(ClientText.nameRestrict);
            } while (clientToAdd.Name == String.Empty);
            ShowString(ClientText.addressChoosing);
            bool flag = true;
            while (flag)
            {
                Address address = await FindAddress.Start();
                if (address.Id != 0)
                {
                    clientToAdd.AddressId = address.Id;
                    clientToAdd.Address = address;
                    ShowString(ClientText.addressChoosen);
                    await Task.Delay(1000);
                    flag = false;
                }
                else
                {
                    bool levOneFlag = true;
                    while (levOneFlag)
                    {
                        choice = MenuToChoice(AddrText.searchAgainOrAddAddress, ClientText.addressNotChoosen, Text.choice);
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
                                ShowString(ClientText.clientNotAdded);
                                await Task.Delay(1000);
                                return new Client();
                        }
                    }

                }
            }
            clientToAdd.Phone = GetDouble(ClientText.inputPhone);
            clientToAdd.OwnerId = user.UserId;
            clientToAdd.Comment = GetString(ClientText.inputComment);
            ShowString(ClientText.Summary(clientToAdd));
            choice = MenuToChoice(Text.yesOrNo, ClientText.saveClient, Text.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                var clientList = new Clients();
                clientToAdd = await clientList.SaveGetId(user, clientToAdd);
                ShowString(ClientText.clientAdded);
                await Task.Delay(1000);
                return clientToAdd;
            }
            ShowString(ClientText.clientNotAdded);
            await Task.Delay(1000);
            return new Client();
        }
    }
}
