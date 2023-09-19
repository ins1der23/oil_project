using static InOut;
using MenusAndChoices;
using Models;
using Controller;


namespace Handbooks

{
    public class AgreementControl
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

            ShowString(agreement.Summary());
            int choice;
            while (flag)
            {
                choice = MenuToChoice(AgrText.options, client.FullName, Text.choice, false);
                switch (choice)
                {
                    case 6:
                        return;
                }

            }

        }
    }
}