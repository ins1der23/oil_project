using static InOut;
using MenusAndChoices;
using Models;
using Controller;


namespace Handbooks

{
    public class AgrControl
    {
        public static async Task Start(Client client)
        {
            var user = Settings.user;
            bool flag = true;
            Agreement agreement = new();
            if (client.Agreements.Any()) agreement = client.Agreements.First();
            else
            {
                agreement.ClientId = client.Id;
                agreement.Client = client;
            }
            int choice;
            while (flag)
            {
                ShowString(agreement.Summary());
                choice = MenuToChoice(AgrText.options, client.FullName, Text.choice, false);
                switch (choice)
                {
                    case 1:
                        await AttachScan.Start(agreement);
                        break;
                    case 6:
                        return;
                }

            }

        }
    }
}