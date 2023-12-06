using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class RegistrationControl
    {
        public static async Task<Registration> Start()
        {
            bool mainFlag = true;
            int choice;
            Registration registration = new();
            while (mainFlag)
            {
                choice = await MenuToChoice(RegistrationText.addOrChange, registration.LongString, invite: Text.choice, noNull: true);
                switch (choice)
                {
                    case 1: //Добавить новый адрес регистрации
                        registration = await AddRegistration.Start();
                        mainFlag = false;
                        break;
                    case 2: // Искать среди существующих
                        bool levOneFlag = true;
                        registration = await FindRegistration.Start();
                        if (registration.Id != 0)
                        {
                            while (levOneFlag)
                            {
                                choice = await MenuToChoice(RegistrationText.options, registration.LongString, invite: Text.choice, noNull: true);
                                switch (choice)
                                {
                                    case 1: // Выбрать 
                                        await ShowString(AddrText.addressChoosen);
                                        levOneFlag = false;
                                        mainFlag = false;
                                        break;
                                    case 2: //Изменить
                                        registration = await ChangeRegistration.Start(registration, toSql: true);
                                        break;
                                    case 3: // Удалить
                                        registration = await DelRegistration.Start(registration);
                                        levOneFlag = false;
                                        break;
                                    case 4: //  Не выбирать адрес и вернуться в предыдущее меню
                                        registration = new();
                                        await ShowString(AddrText.addressNotChoosen);
                                        levOneFlag = false;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            await ShowString(RegistrationText.registrationNotFound);
                            choice = await MenuToChoice(Text.yesOrNo, AddrText.addAddress, invite: Text.choice, noNull: true);
                            if (choice == 1) registration = await AddRegistration.Start();
                            else await ShowString(AddrText.addressNotChoosen);
                        }
                        break;
                    case 3: // Продолжить без выбора адреса регистрации
                        registration = new();
                        mainFlag = false;
                        break;
                }
            }
            Console.WriteLine(registration.LongString);
            Console.ReadLine();
            return registration;
        }
    }
}