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
            if (client.Agreements.Any()) agreement = client.Agreements.OrderBy(agr => agr.Date).Last();
            else
            {
                agreement.ClientId = client.Id;
                agreement.Client = client;
                client.Agreements.Add(agreement);
            }
            int choice;
            while (flag)
            {
                ShowString(agreement.Summary());
                choice = MenuToChoice(AgrText.options, invite: Text.choice, clear : false);
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