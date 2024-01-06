using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class ChangeRegistration
    {
        public static async Task<Registration> Start(Registration registration, bool toSql = true)
        {
            bool flag = true;
            var registrationOld = (Registration)registration.Clone();
            City city = registration.City;
            Street street = registration.Street;
            string houseNum = string.Empty;
            string flatNum = string.Empty;
            var user = Settings.User;
            int choice;
            while (flag)
            {
                await ShowString(registration.Summary(), delay: 0);
                choice = await MenuToChoice(RegistrationText.changeOptions, string.Empty, CommonText.choice, clear: false);
                switch (choice)
                {
                    case 1: // Выбрать другой город
                        city = await CityControl.Start();
                        break;
                    case 2: // Выбрать другую улицу
                        street = await StreetControl.Start(city);
                        break;
                    case 3: // Изменить номер дома
                        houseNum = GetString(RegistrationText.changeHouseNum);
                        break;
                    case 4: // Изменить номер квартиры
                        flatNum = GetString(RegistrationText.changeFlatNum);
                        break;
                    case 5: // Вернуться в предыдущее меню
                        flag = false;
                        break;
                }
                registration.Change(city,street, houseNum, flatNum);
            }
            await ShowString(registration.Summary(), delay: 100);
            if (registration.SearchString != registrationOld.SearchString)
            {
                choice = await MenuToChoice(CommonText.yesOrNo, AddrText.confirmChanges, CommonText.choice, clear: false, noNull: true);
                if (choice == 1)
                {
                    Registrations registrations = new();
                    bool exist = await registrations.CheckExist(registration);
                    if (exist) await ShowString(AddrText.addressExist);
                    else
                    {
                        await ShowString(AddrText.addressChanged);
                        if (toSql) registration = await registrations.SaveChanges(registration);
                        return registration;
                    }
                }
            }
            await ShowString(AddrText.addressNotChanged);
            return registrationOld;
        }
    }
}

