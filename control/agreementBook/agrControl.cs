using static InOut;
using MenusAndChoices;
using Models;
using Controller;


namespace Handbooks

{
    public class AgrControl
    {
        public static async Task Start(Client clientToChange)
        {
            var user = Settings.user;
            bool flag = true;
            var agreement = new Agreement();

            if (clientToChange.Agreements.Any())
            {
                agreement = clientToChange.Agreements.OrderBy(agr => agr.Date).Last();
                agreement.Client = clientToChange;
            }
            else
            {
                agreement.ClientId = clientToChange.Id;
                clientToChange.Agreements.Add(agreement);
                var agrList = new Agreements();
                agreement = await agrList.SaveNewGetId(user, agreement);
                agreement.Client = clientToChange;
            }
            int choice;
            while (flag)
            {
                ShowString(agreement.Summary());
                choice = MenuToChoice(AgrText.options, invite: Text.choice, clear: false);
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