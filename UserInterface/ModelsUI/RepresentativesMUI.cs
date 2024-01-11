using Interfaces;
using MenusAndChoices;
using Models;

namespace UserInterface;
public class Representatives : RepresentativesRepo, IServiceUI<Human>
{
    public async Task<Human> ChangeAndAdd(Human item)
    {
        int choice;
        bool flag = true;
        var parameters = item.GetEmptyParameters();
        while (flag)
        {
            choice = await MenuToChoice(RepresentativeText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
            switch (choice)
            {
                case 1: // Изменить ФИО
                    await ShowString(item.ToString(), clear: true);
                    string name = await GetStringAsync(RepresentativeText.changeName, clear: false);
                    string middlename = await GetStringAsync(RepresentativeText.changeMiddlename, clear: false);
                    string surname = await GetStringAsync(RepresentativeText.changeSurname, clear: false);
                    parameters["Name"] = name;
                    parameters["Middlename"] = middlename;
                    parameters["Surname"] = surname;
                    item.Change(parameters);
                    break;
                case 2: // Изменить паспортные данные
                    Passport passport = await ChangePassportUI.Start(item.Passport);
                    parameters["Passport"] = passport;
                    item.Change(parameters);
                    break;
                case 3: // Выйти
                    flag = false;
                    break;
            }
        }
        Clear();
        Append(item);
        return item;
    }
    public async Task<Human> CreateAndAdd()
    {
        {
            string name = await GetStringAsync(RepresentativeText.name);
            string middlename = await GetStringAsync(RepresentativeText.middlename);
            string surname = await GetStringAsync(RepresentativeText.surname);
            Passport passport = await AddPassportUI.Start();
            Representative item = new()
            {
                Name = name,
                Middlename = middlename,
                Surname = surname,
                Passport = passport,
                PassportId = passport.Id
            };
            item.UpdateParameters();
            Clear();
            Append(item);
            return item;
        }
    }
    public override void CutOff(object parameter)
    {
        DbList = DbList.Select(x => x).Where(x => x.Passport.Equals(parameter)).ToList();
    }

}