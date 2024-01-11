using Handbooks;
using Interfaces;
using MenusAndChoices;
using Models;

namespace UserInterface;
public class Passports : PassportsRepo, IServiceUI<Passport>
{
    public async Task<Passport> ChangeAndAdd(Passport item)
    {
        int choice;
        bool flag = true;
        var parameters = item.Parameters;
        while (flag)
        {
            choice = await MenuToChoice(PassportText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
            switch (choice)
            {
                case 1: // Изменить номер
                    await ShowString(item.Number.ToString(), clear:true);
                    var number = await GetDoubleAsync(PassportText.changeNumber, clear: false);
                    if(number !=0) parameters["Number"] = number;
                    item.Change(parameters);
                    break;
                case 2: // Изменить место выдачи
                    IssuedBy issuedBy = await IssuedsUI.Start(); 
                    parameters["IssuedBy"] = issuedBy;
                    item.Change(parameters);
                    break;
                case 3: // Изменить дату выдачи
                    await ShowString(item.IssueDate.ToShortDateString(), clear:false);
                    DateTime issueDate = await GetDateAsync(PassportText.changeDate);
                    parameters["IssueDate"] = issueDate;
                    item.Change(parameters);
                    break;
                case 4: // Изменить адрес регистрации
                    Registration registration = await RegistrationsUI.Start();
                    parameters["Registration"] = registration;
                    item.Change(parameters);
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
        if (parameter is IssuedBy)
            DbList = DbList.Select(x => x).Where(x => x.IssuedBy.Equals(parameter)).ToList();
        if (parameter is Registration)
            DbList = DbList.Select(x => x).Where(x => x.Registration.Equals(parameter)).ToList();
    }
}

