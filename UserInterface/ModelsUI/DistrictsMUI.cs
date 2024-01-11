using Handbooks;
using Interfaces;
using MenusAndChoices;
using Models;


namespace UserInterface;
public class Districts : DistrictsRepo, IServiceUI<District>
{
    /// <summary>
    /// Элемент пользовательского интерфейса для изменения элемента
    /// </summary>
    /// <param name="item">Изменяемый элемент</param>
    /// <returns>Измененный элемент</returns>
    public async Task<District> ChangeAndAdd(District item)
    {
        int choice;
        bool flag = true;
        Dictionary<string, object> parameters = new();
        while (flag)
        {
            choice = await MenuToChoice(DistrictText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
            switch (choice)
            {
                case 1: // Изменить название
                    string name = await GetStringAsync(CommonText.changeName);
                    parameters.Add("Name", name);
                    item.Change(parameters);
                    break;
                case 2: // Изменить город
                    City city = await CityUI.Start();
                    item.City = city;
                    parameters.Add("CityId", city.Id);
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

    /// <summary>
    /// Элемент пользовательского интерфейса для создания элемента. 
    /// Проверяет есть ли уже созданный в StreetsUI город для присвоения Street
    /// </summary>
    /// <returns>Созданный элемент</returns>
    public async Task<District> CreateAndAdd()
    {
        City city = await CityUI.Start();
        string name = await GetStringAsync(DistrictText.name);
        District item = new()
        {
            Name = name,
            City = city,
            CityId = city.Id,
        };
        item.UpdateParameters();
        Clear();
        Append(item);
        return item;
    }

    public override void CutOff(object parameter)
    {
        DbList = DbList.Select(x => x).Where(x => x.City.Equals(parameter)).ToList();
    }
}


