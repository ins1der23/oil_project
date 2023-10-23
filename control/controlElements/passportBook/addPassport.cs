using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class AddPassport
    {
        public static async Task<Passport> Start()
        {
            var user = Settings.User;
            var passportToAdd = new Passport
            {
                Serial = GetInteger(PassportText.serial),
                Number = GetDouble(PassportText.number),
                IssueDate = GetDate(PassportText.date),
                IssueAuthority = GetString(PassportText.authority)
            };
            int choice = MenuToChoice(Text.yesOrNo, PassportText.addRegistration, Text.choice, noNull: true);
            switch (choice)
            {
                case 1:
                    
                    break;
                case 2:
                    break;

            }
            await Task.Delay(1000);
            return passportToAdd;
        }
    }
}
