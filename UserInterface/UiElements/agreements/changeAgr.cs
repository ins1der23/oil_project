using Models;
using MenusAndChoices;
using Controller;



namespace Handbooks
{
    public class ChangeAgr
    {
        public static async Task<Agreement> Start(Agreement agreement)
        {
            var user = Settings.User;
            string name = GetString(AgrText.changeName);
            DateTime date = GetDate(AgrText.changeDate);
            agreement.Change(name, date);
            var agrList = new Agreements();
            await agrList.SaveChanges(user, agreement);
            return agreement;
        }
        
    }
}