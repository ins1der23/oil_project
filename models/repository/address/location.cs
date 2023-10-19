using Connection;
using static InOut;

namespace Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public Location()
        {
            Name = String.Empty;
            City = new();
            District = new();
        }

        public override string ToString() => $"{City.Name}, {District.Name}, {Name}";
    }

    public class Locations : IEnumerable
    {
        List<Location> LocationsList { get; set; }
        public bool IsEmpty
        {
            get => (!LocationsList.Any());
        }

        public Locations()
        {
            LocationsList = new();
        }
        public IEnumerator GetEnumerator() => LocationsList.GetEnumerator();
        public List<Location> ToWorkingList() => LocationsList.Select(c => c).ToList(); // Список для работы с LINQ
        public Location GetFromList(int index) => LocationsList[index - 1];

        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from districts as d;
                            select * from locations as l;";

                using (var temp = await user.Connection.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var districts = temp.Read<District>();
                    var locations = temp.Read<Location>();
                    LocationsList = locations.Select(x => new Location
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CityId = x.CityId,
                        City = cities.Where(c => c.Id == x.CityId).First(),
                        DistrictId = x.DistrictId,
                        District = districts.Where(d => d.Id == x.DistrictId).First(),
                    }).Where(l => l.Name.PrepareToSearch().Contains(search)).ToList();
                }
                user.Close();
            }
        }

        public List<string> ToStringList()
        {
            List<string> output = new List<string>();
            foreach (var item in LocationsList)
                output.Add(item.ToString());
            return output;
        }
    }
}