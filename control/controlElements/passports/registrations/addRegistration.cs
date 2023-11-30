using Models;

namespace Handbooks
{
    public class AddRegistration
    {
        public async Task<Registration> Start()
        {
            var user = Settings.User;
            bool flag = true;
            int choice;
            City city = await CityControl.Start();
            if (city.Id == 0)
            {
                await ShowString(AddrText.addressNotChoosen);
                return new Address();
            }
            






            string searchString = GetString("");

            registration.CityId = 0;
            registration.StreetId = 0;
            registration.HouseNum = "";
            registration.FlatNum = "";



            return registration;
        }
    }
}