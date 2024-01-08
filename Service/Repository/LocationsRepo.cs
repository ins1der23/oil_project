using Connection;

namespace Models
{
    public abstract class LocationsRepo : BaseRepo<Location>
    {
        public override async Task GetFromSqlAsync(Location? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != string.Empty) search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from districts as d;
                            select * from locations as l;";

                using (var temp = await User.Connection!.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var districts = temp.Read<District>();
                    var locations = temp.Read<Location>();
                    DbList = locations.Select(x => new Location
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CityId = x.CityId,
                        City = x.CityId != 0 ? cities.Where(c => c.Id == x.CityId).First() : new(),
                        DistrictId = x.DistrictId,
                        District = x.DistrictId != 0 ? districts.Where(d => d.Id == x.DistrictId).First() : new(),
                        Parameters = new()
                        {
                            ["Name"] = x.Name,
                            ["City"] = x.City,
                            ["District"] = x.District
                        }
                    }).Where(l => id == 0 ? l.SearchString().Contains(search) : l.Id == id)
                                     .OrderBy(l => l.City.Name).ThenBy(l => l.Name).ToList();
                }
                User.Close();
            }
        }

        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert locations 
                    (name, cityId, districtId)
                    values (@{nameof(Location.Name)},
                            @{nameof(Location.CityId)},
                            @{nameof(Location.DistrictId)})";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update locations set
                    name = @{nameof(Location.Name)},
                    cityId = @{nameof(Location.CityId)},
                    districtId = @{nameof(Location.DistrictId)}
                    where Id = @{nameof(Location.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }

        public override Task DeleteSqlAsync()
        {
            throw new NotImplementedException();
        }


    }
}