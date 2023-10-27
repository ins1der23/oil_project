using Connection;

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

        public async Task GetFromSqlAsync(DBConnection user, string search = "", int id = 0, int cityId = 1)
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from districts as d;
                            select * from locations as l;";

                using (var temp = await user.Connection!.QueryMultipleAsync(sql))
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
                    }).Where(l => id == 0 ? l.Name.PrepareToSearch().Contains(search) : l.Id == id)
                      .Where(l=> l.CityId == cityId).ToList();
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
                await user.Connection!.ExecuteAsync(selectQuery, LocationsList);
                user.Close();
            }
        }
        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update locations set
                    name = @{nameof(Location.Name)},
                    cityId = @{nameof(Location.CityId)},
                    districtId = @{nameof(Location.DistrictId)}
                    where Id = @{nameof(Location.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, LocationsList);
                user.Close();
            }
        }
        public async Task<Location> SaveGetId(DBConnection user, Location location) // получение Id из SQL для нового микрорайона
        {
            if (location.Name == string.Empty) return location;
            Clear();
            Append(location);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, location.Name);
            location = GetFromList();
            return location;
        }

        public async Task<Location> SaveChanges(DBConnection user, Location location) // получение Id из SQL для нового микрорайона
        {
            if (location.Name == string.Empty) return location;
            Clear();
            Append(location);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, id: location.Id, cityId: location.City.Id);
            location = GetFromList();
            return location;
        }
        public List<string> ToStringList()
        {
            List<string> output = new();
            for (int i = 0; i < LocationsList.Count; i++)
            {
                output.Add(LocationsList[i].ToString());
            }
            return output;
        }
    }
}