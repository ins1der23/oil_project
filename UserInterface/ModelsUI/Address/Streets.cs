using System.Net.Mime;
using Handbooks;
using Interfaces;
using MenusAndChoices;
using Service;



namespace Models
{
    class Streets<E> : StreetsRepo<E>, IServiceUI<Street> where E : BaseElement<E>
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
            string name;
            while (flag)
            {
                choice = await MenuToChoice(StreetText.changeMenu, item.Summary(), CommonText.choice, noNull: true);
                switch (choice)
                {
                    case 1: // Изменить город
                        City city = await CityUI.Start();
                        item.City = city;
                        item.CityId = city.Id;
                        break;
                    case 2: // Изменить название
                        name = await GetStringAsync(CommonText.changeName);
                        item.Change(name);
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
        public async Task<Street> CreateAndAdd()
        {
            City city;
            if (BaseLogic<Street, City, Streets<City>>.Parameter != null)
                city = BaseLogic<Street, City, Streets<City>>.Parameter;
            else city = await CityUI.Start();
            Street street = new()
            {
                Name = await GetStringAsync(StreetText.name),
                City = city,
                CityId = city.Id
            };
            return street;
        }

        public override void CutOff(E parameter)
        {
            dbList = dbList.Select(x => x).Where(x => x.City.Equals(parameter)).ToList();
        }
    }

}
