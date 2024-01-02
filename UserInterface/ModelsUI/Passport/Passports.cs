using Handbooks;
using Interfaces;
using MenusAndChoices;
using Models;

public class Passports<E> : PassportsRepo<E>, IServiceUI<Passport> where E : BaseElement<E>
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
            IssuedBy = await IssuedsUI.Start(),
            IssueDate = GetDate(PassportText.date),
        };
        Append(item);
        return item;
    }

    public override void CutOff(E issuedBy)
    {
        dbList = dbList.Select(x => x).Where(x => x.IssuedBy.Equals(issuedBy)).ToList();
    }
}

