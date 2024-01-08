using System.Reflection.Metadata.Ecma335;
using Handbooks;
using Interfaces;
using MenusAndChoices;
using Service;



namespace Models
{
    public class Streets : StreetsRepo, IServiceUI<Street>
    {
        /// <summary>
        /// Элемент пользовательского интерфейса для изменения элемента
        /// </summary>
        /// <param name="item">Изменяемый элемент</param>
        /// <returns>Измененный элемент</returns>
        public async Task<Street> ChangeAndAdd(Street item)
        {
            int choice;
            bool flag = true;
            var parameters = item.GetEmptyParameters();
            while (flag)
            {
                choice = await MenuToChoice(StreetText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
                switch (choice)
                {
                    case 1: // Изменить название
                        string name = await GetStringAsync(CommonText.changeName);
                        parameters["Name"] = name;
                        item.Change(parameters);
                        break;
                    case 2: // Изменить город
                        City city = await CityUI.Start();
                        item.City = city;
                        parameters["City"] = city;
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
        public async Task<Street> CreateAndAdd()
        {
            City city = await CityUI.Start();
            string name = await GetStringAsync(StreetText.name);
            Street item = new()
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
            DbList = DbList.Where(x => x.City.Equals(parameter)).ToList();
        }
    }

}
