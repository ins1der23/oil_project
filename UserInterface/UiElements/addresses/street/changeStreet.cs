using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class ChangeStreet
    {
        public static async Task<Street> Start(Street street, bool toSql = true)
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
            await ShowString(street.Name, clear: true, delay: 100);
            if (street.SearchString != streetOld.SearchString)
            {
                int choice = await MenuToChoice(CommonText.yesOrNo, StreetText.changeConfirm, CommonText.choice, clear: false, noNull: true);
                if (choice == 1)
                {
                    Streets<Street> streets = new();
                    var user = Settings.User;
                    bool exist = await streets.CheckExist(street);
                    if (exist) await ShowString(StreetText.streetExist);
                    else
                    {
                        await ShowString(StreetText.changed);
                        if (toSql) street = await streets.SaveChanges( street);
                        return street;
                    }
                }
            }
            await ShowString(StreetText.changeCancel);
            return streetOld;
        }
    }
}