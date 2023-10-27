using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class ChangeIssuedBy
    {
        public static async Task<IssuedBy> Start(IssuedBy issuedBy)
        {
            var issuedByNew = (IssuedBy)issuedBy.Clone();
            await ShowString(issuedBy.Name, clear: true);
            string name = GetString(IssuedByText.changeName, clear: false);
            if (name == string.Empty)
            {
                await ShowString(IssuedByText.changeCancel);
                return issuedBy;
            }
            issuedByNew.Change(name);
            await ShowString(issuedByNew.Name, clear: true);
            int choice = await MenuToChoice(Text.yesOrNo, IssuedByText.changeConfirm, Text.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                Issueds issueds = new();
                issueds.Append(issuedBy);
                var user = Settings.User;
                await issueds.ChangeSqlAsync(user);
                await ShowString(IssuedByText.changed);
                return issuedByNew;
            }
            await ShowString(IssuedByText.changeCancel);
            return issuedBy;
        }
    }
}