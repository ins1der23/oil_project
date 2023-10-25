using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class AddIssuedBy
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
            return issuedBy;
        }
    }
}
