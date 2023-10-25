using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class AddIssued
    {
        public static async Task<IssuedBy> Start()
        {
            var user = Settings.User;
            IssuedBy issuedBy = new()
            {
                Name = GetString(IssuedByText.name)
            };
            Issueds issuedList = new();
            issuedBy = await issuedList.SaveGetId(user, issuedBy);
            ShowString(IssuedByText.added);
            await Task.Delay(1000);
            return issuedBy;
        }
    }
}
