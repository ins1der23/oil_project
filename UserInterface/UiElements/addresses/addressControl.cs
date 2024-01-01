using MenusAndChoices;
using Controller;
using Models;

namespace Handbooks

{
    public class AddressControl
    {
        public static async Task<Address> Start()
        {
            bool flag = true;
            int choice;
            Address address = new();
            while (flag)
            {
                address = await FindAddress.Start();
                if (address.Id == 0)
                {
                    choice = await MenuToChoice(AddrText.searchAgainOrAddAddress, AddrText.addressNotChoosen, CommonText.choice);
                    switch (choice)
                    {
                        case 1: // Повторить поиск адреса
                            await ShowString(CommonText.returnToSearch);
                            break;
                        case 2: // Добавить адрес
                            var addressNew = await AddAddress.Start();
                            if (address.Id != 0)
                            {
                                address = addressNew;
                                await ShowString(AddrText.addressChoosen);
                                flag = false;
                            }
                            else await ShowString(AddrText.addressNotChoosen);
                            flag = false;
                            break;
                        case 3: // Отменить выбор адреса и вернуться в предыдущее меню
                            await ShowString(AddrText.addressNotChoosen);
                            flag = false;
                            break;
                    }
                }
                else
                {
                    bool levelOneFlag = true;
                    while (levelOneFlag)
                    {
                        choice = await MenuToChoice(AddrText.options, address.LongString, invite: CommonText.choice, noNull: true);
                        switch (choice)
                        {
                            case 1: // Выбрать 
                                await ShowString(AddrText.addressChoosen);
                                levelOneFlag = false;
                                flag = false;
                                break;
                            case 2: //Изменить
                                address = await ChangeAddress.Start(address, toSql: true);
                                break;
                            case 3: // Удалить
                                address = await DelAddress.Start(address);
                                break;
                            case 4: // Вернуться к поиску
                                levelOneFlag = false;
                                await ShowString(CommonText.returnToSearch);
                                break;
                            case 5: // Вернуться в предыдущее меню
                                levelOneFlag = false;
                                flag = false;
                                address = new();
                                await ShowString(AddrText.addressNotChoosen);
                                break;
                        }
                    }
                }
            }
            return address;
        }
    }
}
