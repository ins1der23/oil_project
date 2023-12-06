using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class DelIssuedBy
    {
        public static async Task<IssuedBy> Start(IssuedBy issuedBy)
        {

            await ShowString(issuedBy.Name, clear: true);
            int choice = await MenuToChoice(Text.yesOrNo, IssuedByText.delConfirm, Text.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                Issueds issueds = new();
                issueds.Append(issuedBy);
                var user = Settings.User;
                await issueds.DeleteSqlAsync(user);
                await ShowString(IssuedByText.deleted);
                return new IssuedBy();
            }
            await ShowString(IssuedByText.delCancel);
            return issuedBy;
        }
    }
}