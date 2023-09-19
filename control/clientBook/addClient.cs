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
                clientToAdd.Name = InOut.GetString("Обязательно введите имя клиента");
            } while (clientToAdd.Name == String.Empty);
            ShowString("\nВЫБОР АДРЕСА");
            bool flag = true;
            while (flag)
            {
                Addresses addressList = await FindAddresses.Start();
                choice = MenuToChoice(addressList.ToStringList(), "Найденные адреса", Text.choiceOrEmpty);
                if (choice != 0) 
                {
                    clientToAdd.AddressId = addressList.GetFromList(choice).Id;
                    clientToAdd.Address = addressList.GetFromList(choice);
                }
                else
                {
                    choice = MenuToChoice(Text.searchAgainOrAdd, "АДРЕС НЕ ВЫБРАН");
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Добавить
                            Address address = await AddAddress.Start();
                            if (address.Id != 0)
                            {
                                clientToAdd.AddressId = address.Id;
                                clientToAdd.Address = address;
                                flag = false;
                            }
                            break;
                        case 3: // Возврат в предыдущее меню
                            return clientToAdd;
                    }
                }
                flag = false;
            }
            clientToAdd.Phone = GetDouble("Введите телефон");
            clientToAdd.OwnerId = user.UserId;
            clientToAdd.Comment = GetString("Введите комментарий");
            ShowString(ClientText.Summary(clientToAdd));
            choice = MenuToChoice(Text.yesOrNo, "Сохранить клиента?", Text.choice);
            if (choice == 1)
            {
                var clientList = new Clients();
                clientList.Append(clientToAdd);
                await clientList.AddSqlAsync(user);
                ShowString("КЛИЕНТ УСПЕШНО ДОБАВЛЕН");
            }
            return clientToAdd;
        }
    }
}
