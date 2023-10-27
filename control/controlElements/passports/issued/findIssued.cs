using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class FindIssuedBy
    {
        public static async Task<IssuedBy> Start()
        {
            var user = Settings.User;
            Issueds issueds = new();
            string search = GetString(IssuedByText.search);
            await issueds.GetFromSqlAsync(user, search);
            bool flag = true;
            IssuedBy issuedBy = new();
            int choice;
            while (flag)
            {
                choice = await MenuToChoice(issueds.ToStringList(), IssuedByText.issuedFound, Text.choiceOrEmpty);
                if (choice != 0) issuedBy = issueds.GetFromList(choice);
                flag = false;
            }
            return issuedBy;
        }
    }
}