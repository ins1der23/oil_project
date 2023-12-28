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
                // IssuedBy = await FindIssuedBy.Start();
                IssueDate = GetDate(PassportText.date),
            };

            int choice = await MenuToChoice(Text.yesOrNo, PassportText.addRegistration, Text.choice, noNull: true);
            switch (choice)
            {
                case 1: // Добавить регистрацию
                    await ShowString(AddrText.addressChoosing, delay: 100);
                    bool flag = true;
                    while (flag)
                    {
                        Address address = await FindAddress.Start();
                        if (address.Id != 0)
                        {
                            passportToAdd.RegistrationId = address.Id;
                            passportToAdd.Registration = address;
                            await ShowString(AddrText.addressChoosen);
                            flag = false;
                        }
                        else
                        {
                            bool levOneFlag = true;
                            while (levOneFlag)
                            {
                                choice = await MenuToChoice(AddrText.searchOrAddContinue, AddrText.addressNotChoosen, Text.choice);
                                switch (choice)
                                {
                                    case 1: // Повторить поиск
                                        levOneFlag = false;
                                        break;
                                    case 2: // Добавить адрес
                                        address = await AddAddress.Start();
                                        if (address.Id != 0)
                                        {
                                            passportToAdd.RegistrationId = address.Id;
                                            passportToAdd.Registration = address;
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
            choice = await MenuToChoice(Text.yesOrNo, PassportText.savePassport, Text.choice, clear: false, noNull: true);
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
