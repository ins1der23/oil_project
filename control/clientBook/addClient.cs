using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using Connection;
using MySql.Data.MySqlClient;

namespace Handbooks
{
    class AddClient
    {
        public static async Task Start()
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
                if (choice != 0) clientToAdd.AddressId = addressList.GetFromList(choice).Id;
                else
                {
                    choice = MenuToChoice(Text.searchAgainOrAdd, "АДРЕС НЕ ВЫБРАН");
                    switch (choice)
                    {
                        case 1: // Повторить поиск
                            break;
                        case 2: // Добавить
                            Address newAddress = await AddAddress.Start();
                            if (newAddress.Id != 0)
                            {
                                clientToAdd.AddressId = newAddress.Id;
                                flag = false;
                            }
                            break;
                        case 3: // Возврат в предыдущее меню
                            return;
                    }
                }
                flag = false;
            }
            clientToAdd.Phone = GetDouble("Введите телефон");
            // clientToAdd.Agreement = GetString("Укажите путь к договору");
            clientToAdd.OwnerId = user.UserId;
            clientToAdd.Comment = GetString("Введите комментарий");
            var clientList = new Clients();
            clientList.Append(clientToAdd);
            await clientList.AddSqlAsync(user);
            await clientList.GetFromSqlAsync(user, clientToAdd.FullName);
            clientToAdd = clientList.GetFromList();
            ShowString(Text.ClientSum(clientToAdd));
            choice = MenuToChoice(Text.yesOrNo, "Сохранить клиента?", Text.choice);
            if (choice != 1) await clientList.DeleteSqlAsync(user);
            ShowString("КЛИЕНТ УСПЕШНО ДОБАВЛЕН");



        }
    }
}
