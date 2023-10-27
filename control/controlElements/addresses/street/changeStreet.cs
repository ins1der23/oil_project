using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class ChangeStreet
    {
        public static async Task<Street> Start(Street street)
        {
            var streetOld = (Street)street.Clone();
            await ShowString(street.Name, clear: true, delay: 300);
            string name = GetString(StreetText.changeName, clear: false);
            if (name == string.Empty)
            {
                await ShowString(StreetText.changeCancel);
                return streetOld;
            }
            street.Change(name);
            await ShowString(street.Name, clear: true);
            int choice = await MenuToChoice(Text.yesOrNo, StreetText.changeConfirm, Text.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                Streets streets = new();
                var user = Settings.User;
                street = await streets.SaveChanges(user, street);
                await ShowString(StreetText.changed);
                return street;
            }
            await ShowString(StreetText.changeCancel);
            return streetOld;
        }
    }
}