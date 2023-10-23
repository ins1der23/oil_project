using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    class DelClient
    {
        public static async Task<bool> Start(Client clientToDelete)
        {
            var user = Settings.User;
            bool flag = true;
            int choice;
            while (flag)
            {
                ShowString(ClientText.Summary(clientToDelete), true);
                choice = MenuToChoice(Text.yesOrNo, ClientText.delClient, Text.choice, false); // Точно удалить?
                switch (choice)
                {
                    case 0:
                        break;
                    case 1: // Да
                        var clientList = new Clients();
                        clientList.Append(clientToDelete);
                        await clientList.DeleteSqlAsync(user);
                        ShowString(ClientText.clientDeleted);
                        await Task.Delay(1000);
                        return true;
                    case 2: // Нет
                        flag = false;
                        break;
                }
            }
            return false;
        }
    }
}