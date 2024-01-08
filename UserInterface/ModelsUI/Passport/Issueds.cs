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
        var parameters = item.GetEmptyParameters();
        string name = await GetStringAsync(CommonText.changeName);
        parameters["Name"] = name;
        item.Change(parameters);
        Clear();
        Append(item);
        return item;
    }

    public async Task<IssuedBy> CreateAndAdd() // Создание, добавление в dbList и возврат элемента
    {
        string name = await GetStringAsync(CityText.name);
        IssuedBy item = new()
        {
            Name = name,
        };
        item.UpdateParameters();
        Clear();
        Append(item);
        return item;
    }

    public override void CutOff(object parameter)
    {
        DbList = DbList.Where(x => x.Equals(parameter)).ToList();
    }
}