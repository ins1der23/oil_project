using Handbooks;
using Interfaces;
using MenusAndChoices;
using Org.BouncyCastle.Asn1.X509;



namespace Models
{
    public class Cities : CitiesRepo, IServiceUI<City>
    {
        public async Task<City> ChangeAndAdd(City item)
        {
            var parameters = item.GetEmptyParameters();
            string name = await GetStringAsync(CommonText.changeName);
            parameters["Name"] = name;
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
            };
            item.UpdateParameters();
            Clear();
            Append(item);
            return item;
        }

        public override void CutOff(object parameter)
        {
            DbList = DbList.Where(x => x.Equals(parameter)).ToList();
        }
    }
}
