using Handbooks;
using Interfaces;
using MenusAndChoices;
using Models;

namespace UserInterface;
public class Locations : LocationsRepo, IServiceUI<Location>
{
    public async Task<Location> ChangeAndAdd(Location item)
    {
        int choice;
        bool flag = true;
        var parameters = item.GetEmptyParameters();
        while (flag)
        {
            choice = await MenuToChoice(LocationText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
            switch (choice)

            {
                case 1: // Изменить название
                    await ShowString(item.ToString(), clear: true);
                    string name = await GetStringAsync(CommonText.changeName, clear: false);
                    parameters.Add("Name", name);
                    item.Change(parameters);
                    break;
                case 2: // Изменить город
                    City city = await CityUI.Start();
                    parameters.Add("City", city);
                    item.Change(parameters);
                    break;
                case 3: // Изменить район
                    if (item.CityId == 1)
                    {
                        District district = await SearchDistrictUI.Start(cutOffBy: item.City);
                        parameters.Add("District", district);
                        item.Change(parameters);
                    }
                    else await ShowString(CommonText.notAvailable);
                    break;
                case 4: // Выйти
                    flag = false;
                    break;
            }
        }
        Clear();
        Append(item);
        return item;
    }

    public async Task<Location> CreateAndAdd()
    {
        {
            City city = await CityUI.Start();
            District district = await SearchDistrictUI.Start();
            string name = await GetStringAsync(LocationText.name);
            Location item = new()
            {
                Name = name,
                City = city,
                CityId = city.Id,
                District = district,
                DistrictId = district.Id,
            };
            item.UpdateParameters();
            Clear();
            Append(item);
            return item;
        }
    }

    public override void CutOff(object parameter)
    {
        DbList = DbList.Select(x => x).Where(x => x.City.Equals(parameter)).ToList();
    }
}