using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    class DelClient
    {
        public static async Task<Client> Start(Client clientToDelete)
        {
            bool flag = true;
            int choice;
            while (flag)
            {
                await ShowString(ClientText.Summary(clientToDelete), true, delay: 100);
                choice = await MenuToChoice(Text.yesOrNo, ClientText.delClient, Text.choice, clear: false, noNull: true); // Точно удалить?
                switch (choice)
                {
                    case 1: // Да
                        var clientList = new Clients();
                        clientList.Append(clientToDelete);
                        var user = Settings.User;
                        await clientList.DeleteSqlAsync(user);
                        await ShowString(ClientText.clientDeleted);
                        return new Client();
                    case 2: // Нет
                        flag = false;
                        await ShowString(ClientText.clientNotDeleted);
                        break;
                }
            }
            return clientToDelete;
        }
    }
}