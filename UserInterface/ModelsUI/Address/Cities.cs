using Handbooks;
using Interfaces;
using MenusAndChoices;



namespace Models
{
    class Cities<E> : CitiesRepo<E>, IServiceUI<City> where E : BaseElement<E>
    {
        public async Task<City> ChangeAndAdd(City item)
        {
            string name = GetString(CommonText.changeName, clear: false);
            if (name == string.Empty)
            {
                await ShowString(CityText.changeCancel);
                return item.SetEmpty();
            }
            item.Change(name);
            Clear();
            Append(item);
            return item;
        }

        public async Task<City> CreateAndAdd()
        {
            City item = new()
            {
                Name = await GetStringAsync(CityText.name),
                Districts = new(),
                Locations = new(),
                Streets = new(),
            };
            Clear();
            Append(item);
            return item;
        }

        public override void CutOff(E parameter)
        {
            dbList = dbList.Select(x => x).Where(x => x.Equals(parameter)).ToList();
        }
    }
}
