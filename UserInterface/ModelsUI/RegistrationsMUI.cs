using Handbooks;
using Interfaces;
using MenusAndChoices;
using Models;
using Service;

class Registrations : RegistrationsRepo, IServiceUI<Registration>
{
    public async Task<Registration> ChangeAndAdd(Registration item)
    {
        int choice;
        bool flag = true;
        string name = string.Empty;
        Dictionary<string, object> parameters = new();
        while (flag)
        {
            choice = await MenuToChoice(RegistrationText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
            switch (choice)
            {
                case 1: // Изменить название
                    name = await GetStringAsync(CommonText.changeName);
                    parameters.Add("Name", name);
                    item.Change(parameters);
                    break;
                case 2: // Изменить город
                    City city = await CityUI.Start();
                    item.City = city;
                    parameters.Add("CityId", city.Id);
                    item.Change(parameters);
                    break;
                case 3: // Изменить улицу
                    City city = await CityUI.Start();
                    item.City = city;
                    parameters.Add("CityId", city.Id);
                    item.Change(parameters);
                    break;
                case 4: // Выйти
                    flag = false;
                    break;
            }
        }
        return item;
    }

    public async Task<Registration> CreateAndAdd()
    {
        {
            City city;
            if (BaseLogic<Registration, City, Registrations>.CutOffBy != null)
                city = BaseLogic<Registration, City, Registrations>.CutOffBy;
            else
            {
                city = await CityUI.Start();
                BaseLogic<Registration, City, Registrations>.CutOffBy = city;

            }
            Street street = await StreetsUI.Start(cutOff: true, city: false);
            string houseNum = await GetStringAsync(RegistrationText.name);
            string flatNum = await GetStringAsync(RegistrationText.name);
            Registration registration = new()
            {
                City = city,
                CityId = city.Id,
                Street = street,
                StreetId = street.Id,
                HouseNum = houseNum,
                FlatNum = flatNum,
                Parameters = new()
                {
                    ["City"] = city,
                    ["Street"] = street,
                    ["HouseNum"] = houseNum,
                    ["FlatNum"] = flatNum
                }
            };
            BaseLogic<Registration, City, Registrations>.CutOffBy = null;
            return registration;
        }
    }

    public override void CutOff<P>(P parameter)
    {
        dbList = dbList.Select(x => x).Where(x => x.City.Equals(parameter)).ToList();
    }

}
