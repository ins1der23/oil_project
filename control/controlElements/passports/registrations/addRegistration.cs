using static InOut;
using MenusAndChoices;
using Controller;
using Models;
using System.Text.RegularExpressions;

namespace Handbooks
{
    public class AddRegistration
    {
        public async Task<Registration> Start()
        {
            await Task.Delay(1000);
            Registration registration = new();
            string searchString = GetString("");
            
            registration.CityId = 0;
            registration.StreetId = 0;
            registration.HouseNum = "";
            registration.FlatNum = "";



            return registration;
        }
    }
}