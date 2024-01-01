using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class AddPassport
    {
        public static async Task<Passport> Start()
        {
            var passportToAdd = new Passport()
            {
                Number = GetDouble(PassportText.number),
                IssuedBy  = await StartIssuedByUI.Start(),
                IssueDate = GetDate(PassportText.date),
            };

            int choice = await MenuToChoice(CommonText.yesOrNo, PassportText.addRegistration, CommonText.choice, noNull: true);
            switch (choice)
            {
                case 1: // Добавить регистрацию
                    await ShowString(AddrText.addressChoosing, delay: 100);
                    bool flag = true;
                    while (flag)
                    {
                        Registration registration = await FindRegistration.Start();
                        if (registration.Id != 0)
                        {
                            passportToAdd.RegistrationId = registration.Id;
                            passportToAdd.Registration = registration;
                            await ShowString(AddrText.addressChoosen);
                            flag = false;
                        }
                        else
                        {
                            bool levOneFlag = true;
                            while (levOneFlag)
                            {
                                choice = await MenuToChoice(AddrText.searchOrAddContinue, AddrText.addressNotChoosen, CommonText.choice);
                                switch (choice)
                                {
                                    case 1: // Повторить поиск
                                        levOneFlag = false;
                                        break;
                                    case 2: // Добавить адрес
                                        registration = await AddRegistration.Start();
                                        if (registration.Id != 0)
                                        {
                                            passportToAdd.RegistrationId = registration.Id;
                                            passportToAdd.Registration = registration;
                                            levOneFlag = false;
                                            flag = false;
                                        }
                                        break;
                                    case 3: // Продолжить без добавления адреса
                                        await ShowString(AddrText.addressNotChoosen);
                                        levOneFlag = false;
                                        flag = false;
                                        break;
                                }
                            }
                        }
                    }
                    break;
                case 2: // Не добавлять регистрацию
                    await ShowString(AddrText.addressNotChoosen);
                    break;
            }
            await ShowString(PassportText.Summary(passportToAdd), delay: 100);
            choice = await MenuToChoice(CommonText.yesOrNo, PassportText.savePassport, CommonText.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                var passportList = new Passports();
                // passportToAdd = await clientList.SaveGetId(user, passportToAdd);
                await ShowString(ClientText.clientAdded);
                // return clientToAdd;
            }
            await ShowString(ClientText.clientNotAdded);
            return new Passport();




        }
    }
}
