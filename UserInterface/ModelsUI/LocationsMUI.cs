using Handbooks;
using Interfaces;
using MenusAndChoices;
using Models;
using Service;

class Locations : LocationsRepo, IServiceUI<Location>
{
    public async Task<Location> ChangeAndAdd(Location location)
    {
        int choice;
        bool flag = true;
        Dictionary<string, object> parameters = new();
        while (flag)
        {
            choice = await MenuToChoice(LocationText.changeMenu, location.Summary(), CommonText.choice, noNull: true);
            switch (choice)

            {
                case 1: // Изменить название
                    string name = await GetStringAsync(CommonText.changeName);
                    parameters.Add("Name", name);
                    location.Change(parameters);
                    break;
                case 2: // Изменить город
                    City city = await CityUI.Start();
                    parameters.Add("City", city);
                    location.Change(parameters);
                    break;
                case 3: // Изменить район
                    if (location.CityId == 1)
                    {
                        BaseLogic<Location, City, Locations>.CutOffBy = location.City;
                        District district = await SearchDistrictUI.Start(cutOff: true);
                        parameters.Add("District", district);
                        location.Change(parameters);
                    }
                    else await ShowString(CommonText.notAvailable);
                    break;
                case 4: // Выйти
                    BaseLogic<Location, City, Locations>.CutOffBy = null;
                    flag = false;
                    break;
            }
        }
        return location;
    }

    public async Task<Location> CreateAndAdd()
    {
        {
            City city;
            if (BaseLogic<Location, City, Locations>.CutOffBy != null)
                city = BaseLogic<Location, City, Locations>.CutOffBy;
            else
            {
                city = await CityUI.Start();
                BaseLogic<Location, City, Locations>.CutOffBy = city;

            }
            District district = await SearchDistrictUI.Start(cutOff: true);
            string name = await GetStringAsync(LocationText.name);
            Location location = new()
            {
                City = city,
                CityId = city.Id,
                District = district,
                DistrictId = district.Id,
                Parameters = new()
                {
                    ["Name"] = name,
                    ["CityId"] = city.Id,
                    ["DistrictId"] = district.Id
                }
            };
            BaseLogic<Location, City, Locations>.CutOffBy = null;
            return location;
        }
    }

    public override void CutOff<City>(City parameter)
    {
        dbList = dbList.Select(x => x).Where(x => x.City.Equals(parameter)).ToList();
    }
}