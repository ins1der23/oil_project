using static InOut;
using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks
{
    public class AddPassport
    {
        public static async Task<Passport> Start()
        {
            var user = Settings.User;
            var passportToAdd = new Passport
            {
                Number = GetDouble(PassportText.number),
                // IssuedBy = await FindIssuedBy.Start();
                IssueDate = GetDate(PassportText.date),
            };

            int choice = MenuToChoice(Text.yesOrNo, PassportText.addRegistration, Text.choice, noNull: true);
            switch (choice)
            {
                case 1: // Добавить регистрацию
                    ShowString(AddrText.addressChoosing);
                    bool flag = true;
                    while (flag)
                    {
                        Address address = await FindAddress.Start();
                        if (address.Id != 0)
                        {
                            passportToAdd.RegistrationId = address.Id;
                            passportToAdd.Registration = address;
                            ShowString(AddrText.addressChoosen);
                            await Task.Delay(1000);
                            flag = false;
                        }
                        else
                        {
                            bool levOneFlag = true;
                            while (levOneFlag)
                            {
                                choice = MenuToChoice(AddrText.searchOrAddContinue, AddrText.addressNotChoosen, Text.choice);
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
                                        ShowString(AddrText.addressNotChoosen);
                                        await Task.Delay(1000);
                                        levOneFlag = false;
                                        flag = false;
                                        break;
                                }
                            }
                        }
                    }
                    break;
                case 2: // Не добавлять регистрацию
                    ShowString(AddrText.addressNotChoosen);
                    await Task.Delay(1000);
                    break;
            }
            ShowString(PassportText.Summary(passportToAdd));
            choice = MenuToChoice(Text.yesOrNo, PassportText.savePassport, Text.choice, clear: false, noNull: true);
            if (choice == 1)
            {
                var passportList = new Passports();
                // passportToAdd = await clientList.SaveGetId(user, passportToAdd);
                ShowString(ClientText.clientAdded);
                await Task.Delay(1000);
                // return clientToAdd;
            }
            ShowString(ClientText.clientNotAdded);
            await Task.Delay(1000);
            return new Passport();



            await Task.Delay(1000);
            return passportToAdd;
        }
    }
}
