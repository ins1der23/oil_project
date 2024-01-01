using Handbooks;
using Interfaces;
using MenusAndChoices;
using Models;

class Passports : PassportsRepo, IService<Passport>
{
    public Task<Passport> ChangeAndAdd(Passport item)
    {
        throw new NotImplementedException();
    }

    public async Task<Passport> CreateAndAdd() // Создание, добавление в dbList и возврат элемента
    {
        var item = new Passport()
        {
                Number = GetDouble(PassportText.number),
                IssuedBy  = await StartIssuedByUI.Start(),
                IssueDate = GetDate(PassportText.date),
        };
        Append(item);
        return item;
    }
}

