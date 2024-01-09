using Handbooks;
using Interfaces;
using MenusAndChoices;
using Models;

class Passports : PassportsRepo, IServiceUI<Passport>
{
    public async Task<Passport> ChangeAndAdd(Passport item)
    {
        int choice;
        bool flag = true;
        var parameters = item.GetEmptyParameters();
        while (flag)
        {
            choice = await MenuToChoice(RegistrationText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
            switch (choice)
            {
                case 1: // Изменить номер
                    City city = await CityUI.Start();
                    item.City = city;
                    parameters["City"] = city;
                    item.Change(parameters);
                    break;
                case 2: // Изменить место и дату выдачи
                    Street street = await StreetsUI.Start(cutOffBy: item.City);
                    item.Street = street;
                    parameters["Street"] = street;
                    item.Change(parameters);
                    break;
                case 3: // Изменить адрес регистрации
                    string houseNum = await GetStringAsync(RegistrationText.changeHouseNum);
                    string flatNum = await GetStringAsync(RegistrationText.changeFlatNum);
                    parameters["HouseNum"] = houseNum;
                    parameters["FlatNum"] = flatNum;
                    item.Change(parameters);
                    break;
                case 4: // Изменить клиента
                    break;
                case 5: // Выйти
                    flag = false;
                    break;
            }
        }
        Clear();
        Append(item);
        return item;
    }

    public async Task<Passport> CreateAndAdd() // Создание, добавление в dbList и возврат элемента
    {
        var number = GetDouble(PassportText.number);
        IssuedBy issuedBy = await IssuedsUI.Start();
        Registration registration = new();
        var issueDate = GetDate(PassportText.date);
        int choice = await MenuToChoice(CommonText.yesOrNo, PassportText.addRegistration, CommonText.choice, noNull: true);
        if (choice == 1) registration = await RegistrationsUI.Start();
        var item = new Passport()
        {
            Number = number,
            IssuedBy = issuedBy,
            IssuedId = issuedBy.Id,
            IssueDate = issueDate,
            Registration = registration,
            RegistrationId = registration.Id
        };
        item.UpdateParameters();
        Clear();
        Append(item);
        return item;
    }

    public override void CutOff(object parameter)
    {
        DbList = DbList.Select(x => x).Where(x => x.IssuedBy.Equals(parameter)).ToList();
    }
}

