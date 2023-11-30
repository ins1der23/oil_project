using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class ChangeRegistration
    {
        public static async Task<Registration> Start(Registration registration, bool toSql)
        {
            await Task.Delay(1000);
            return new Registration();
        }

    }
}