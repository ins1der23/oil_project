using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class IssuedControl
    {
        public static async Task<IssuedBy> Start()
        {
            var user = Settings.User;
            bool mainFlag = true;
            while (mainFlag)
            {
                IssuedBy issuedBy = await FindIssuedBy.Start();
                if (issuedBy.Id != 0) return issuedBy;
                int choice = MenuToChoice()

            }



        }
    }
}