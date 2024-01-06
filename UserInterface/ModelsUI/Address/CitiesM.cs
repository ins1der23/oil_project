using Handbooks;
using Interfaces;
using MenusAndChoices;
using Org.BouncyCastle.Asn1.X509;



namespace Models
{
    class Cities : CitiesRepo, IServiceUI<City>
    {
        public async Task<City> ChangeAndAdd(City item)
        {
            string name = GetString(CommonText.changeName, clear: false);
            if (name == string.Empty)
            {
                await ShowString(CityText.changeCancel);
                return item.SetEmpty();
            }
            Dictionary<string, object> parameters = new()
            {
                ["Name"] = name
            };
            item.Change(parameters);
            Clear();
            Append(item);
            return item;
        }

        public async Task<City> CreateAndAdd()
        {
            string name = await GetStringAsync(CityText.name);
            City item = new()
            {
                Name = name,
                Districts = new(),
                Locations = new(),
                Streets = new(),
                Parameters = new()
                {
                    ["Name"] = name
                }
            };
            Clear();
            Append(item);
            return item;
        }

        public override void CutOff<P>(P parameter)
        {
            dbList = dbList.Select(x => x).Where(x => x.Equals(parameter)).ToList();
        }
    }
}
