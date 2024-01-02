using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class AddRegistration
    {
        public static async Task<Registration> Start()
        {
            var user = Settings.User;
            bool flag = true;
            int choice;

            City city = new();
            while (flag)
            {
                city = await CityControl.Start();
                if (city.Id == 0)
                {
                    choice = await MenuToChoice(CityText.tryAgain, CityText.cityNotChoosen, CommonText.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Вернуться к поиску города еще раз
                            break;
                        case 2: // Отменить добавление адреса и вернуться в предыдущее меню
                            await ShowString(AddrText.addressNotChoosen);
                            return new Registration();
                    }
                }
                else flag = false;
            }

            flag = true;
            Street street = new();
            while (flag)
            {
                street = await StreetControl.Start(city);
                if (street.Id == 0)
                {
                    choice = await MenuToChoice(StreetText.tryAgain, StreetText.streetNotChoosen, CommonText.choice, noNull: true);
                    switch (choice)
                    {
                        case 1: // Вернуться к поиску улицы еще раз
                            break;
                        case 2: // Отменить добавление адреса и вернуться в предыдущее меню
                            await ShowString(AddrText.addressNotChoosen);
                            return new Registration();
                    }
                }
                else flag = false;
            }

            Registration registration = new()
            {
                CityId = city.Id,
                City = city,
                StreetId = street.Id,
                Street = street,
                HouseNum = GetString(RegistrationText.houseNum),
                FlatNum = GetString(RegistrationText.flatNum)
            };

            flag = true;
            while (flag)
            {
                await ShowString(registration.Summary(), delay: 0);
                choice = await MenuToChoice(RegistrationText.saveOptions, string.Empty, CommonText.choice, clear: false, noNull: true);
                switch (choice)
                {
                    case 1: // Сохранить адрес
                        Registrations<Registration> registrations = new();
                        bool exist = await registrations.CheckExist(registration);
                        if (exist) await ShowString(CityText.cityExist);
                        else
                        {
                            registration = await registrations.SaveGetId(registration);
                            await ShowString(RegistrationText.registrationAdded);
                            flag = false;
                        }
                        break;
                    case 2: // Изменить адрес
                        registration = await ChangeRegistration.Start(registration, toSql: false);
                        break;
                    case 3: // Не сохранять адрес
                        await ShowString(RegistrationText.registrationNotAdded);
                        registration = new();
                        flag = false;
                        break;
                }
            }
            return registration;
        }
    }
}