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
        var parameters = item.GetEmptyParameters();
        while (flag)
        {
            choice = await MenuToChoice(RegistrationText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
            switch (choice)
            {
                case 1: // Изменить город
                    City city = await CityUI.Start();
                    item.City = city;
                    parameters["City"] = city;
                    item.Change(parameters);
                    break;
                case 2: // Изменить улицу
                    Street street = await StreetsUI.Start(cutOffBy: item.City);
                    item.Street = street;
                    parameters["Street"] = street;
                    item.Change(parameters);
                    break;
                case 3: // Изменить номер дома и квартиру
                    string houseNum = await GetStringAsync(RegistrationText.changeHouseNum);
                    string flatNum = await GetStringAsync(RegistrationText.changeFlatNum);
                    parameters["HouseNum"] = houseNum;
                    parameters["FlatNum"] = flatNum;
                    item.Change(parameters);
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

    public async Task<Registration> CreateAndAdd()
    {
        {
            City city = await CityUI.Start();
            Street street = await StreetsUI.Start(cutOffBy: city);
            string houseNum = await GetStringAsync(RegistrationText.houseNum);
            string flatNum = await GetStringAsync(RegistrationText.flatNum);
            Registration item = new()
            {
                City = city,
                CityId = city.Id,
                Street = street,
                StreetId = street.Id,
                HouseNum = houseNum,
                FlatNum = flatNum,
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
