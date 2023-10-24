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
            Id = 999;
            Name = string.Empty;
            City = new();
            District = new();
        }

        public override string ToString()
        {
            if (City.Id == 1)
                return $"{City.Name}, {District.Name}, {Name}";
            else
                return $"{Name}";
        }

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

        public void Clear() => LocationsList.Clear();
        public void Append(Location location) => LocationsList.Add(location);
        public List<Location> ToWorkingList() => LocationsList.Select(c => c).ToList(); // Список для работы с LINQ
        public Location GetFromList(int index = 1) => LocationsList[index - 1];

        public async Task GetFromSqlAsync(DBConnection user, string search = "", int id = 1)
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
                    }).Where(l => l.Name.PrepareToSearch().Contains(search)).Where(l => l.CityId == id).ToList();
                }
                user.Close();
            }
        }

        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert locations 
                    (name, cityId, districtId)
                    values (@{nameof(Location.Name)},
                            @{nameof(Location.CityId)},
                            @{nameof(Location.DistrictId)})";
                await user.Connection.ExecuteAsync(selectQuery, LocationsList);
                user.Close();
            }
        }

        public List<string> ToStringList()
        {
            List<string> output = new();
            for (int i = 1; i < LocationsList.Count; i++)
            {
                output.Add(LocationsList[i].ToString());
            }
            return output;
        }
    }
}