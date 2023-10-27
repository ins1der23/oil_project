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
            var user = Settings.User;
            var agreement = new Agreement();
            if (clientToChange.Agreements.Any()) agreement = clientToChange.Agreements.OrderBy(agr => agr.Date).Last();
            else
            {
                agreement.ClientId = clientToChange.Id;
                clientToChange.Agreements.Add(agreement);
                var agrList = new Agreements();
                agreement = await agrList.SaveGetId(user, agreement);
            }
            agreement.Client = clientToChange;
            int choice;
            bool flag = true;
            while (flag)
            {
                await ShowString(agreement.Summary(), delay: 100);
                choice = await MenuToChoice(AgrText.options, invite: Text.choice, clear: false);
                switch (choice)
                {
                    case 1: // Прикрепить скан
                        agreement = await AttachScan.Start(agreement);
                        break;
                    case 2: // Показать скан
                        await OpenScan.Start(agreement);
                        break;
                    case 3: // Изменить договор
                        agreement = await ChangeAgr.Start(agreement);
                        break;
                    case 6: // Ничего не делать
                        return;
                }
            }
        }
    }
}