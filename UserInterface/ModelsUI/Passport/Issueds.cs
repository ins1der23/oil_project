using Interfaces;
using MenusAndChoices;
using Models;

class Issueds : IssuedsRepo, IServiceUI<IssuedBy>
{
    /// <summary>
    ///  Изменение, добавление в dbList и возврат элемента
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public async Task<IssuedBy> ChangeAndAdd(IssuedBy item) // 
    {
        Clear();
        string name = await GetStringAsync(CommonText.changeName, clear: false);
        Dictionary<string, object> parameters = new();
        if (name == string.Empty)
        {
            await ShowString(IssuedByText.changeCancel);
            return item.SetEmpty();
        }
        parameters.Add("Name", name);
        item.Change(parameters);
        Append(item);
        return item;
    }

    public async Task<IssuedBy> CreateAndAdd() // Создание, добавление в dbList и возврат элемента
    {
        await Task.Delay(0);
        var item = new IssuedBy()
        {
            Name = await GetStringAsync(IssuedByText.name),
        };
        Append(item);
        return item;
    }

    public override void CutOff<P>(P parameter)
    {
        dbList = dbList.Select(x => x).Where(x => x.Equals(parameter)).ToList();
    }
}