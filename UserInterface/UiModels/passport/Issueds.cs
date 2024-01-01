using Interfaces;
using MenusAndChoices;
using Models;

class Issueds : IssuedsRepo, IService<IssuedBy>
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
        if (name == string.Empty)
        {
            await ShowString(IssuedByText.changeCancel);
            return item.SetEmpty();
        }
        item.Change(name);
        Append(item);
        return item;
    }

    public async Task<IssuedBy> CreateAndAdd() // Создание, добавление в dbList и возврат элемента
    {
        await Task.Delay(0);
        var item = new IssuedBy()
        {
            Name = GetString(IssuedByText.name),
        };
        Append(item);
        return item;
    }
}