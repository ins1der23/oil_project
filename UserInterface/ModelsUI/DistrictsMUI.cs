using Handbooks;
using Interfaces;
using MenusAndChoices;
using Service;



namespace Models
{
    class Districts : DistrictsRepo, IServiceUI<District>
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
            string name = string.Empty;
            Dictionary<string, object> parameters = new();
            while (flag)
            {
                choice = await MenuToChoice(DistrictText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
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
                    case 3: // Выйти
                        flag = false;
                        break;
                }
            }
            return item;
        }

        /// <summary>
        /// Элемент пользовательского интерфейса для создания элемента. 
        /// Проверяет есть ли уже созданный в StreetsUI город для присвоения Street
        /// </summary>
        /// <returns>Созданный элемент</returns>
        public async Task<District> CreateAndAdd()
        {
            City city;
            if (BaseLogic<Street, City, Streets>.CutOffBy != null)
                city = BaseLogic<Street, City, Streets>.CutOffBy;
            else
            {
                city = await CityUI.Start();
                BaseLogic<Location, City, Locations>.CutOffBy = city;
            }
            string name = await GetStringAsync(DistrictText.name);
            District item = new()
            {
                Name = name,
                City = city,
                CityId = city.Id,
                Parameters = new()
                {
                    ["Name"] = name,
                    ["CityId"] = city.Id
                }

            };
            BaseLogic<Location, City, Locations>.CutOffBy = null;
            return item;
        }

        public override void CutOff<P>(P parameter)
        {
            dbList = dbList.Select(x => x).Where(x => x.City.Equals(parameter)).ToList();
        }
    }

}
