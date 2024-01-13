using Connection;


namespace Models
{
    public abstract class AddressesRepo : BaseRepo<BaseAddress>
    {
        public override async Task GetFromSqlAsync(BaseAddress? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            search = search.PrepareToSearch();
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from districts as d;
                            select * from locations as l;
                            select * from streets as s;
                            select * from addresses as a where roleId = 1";

                using (var temp = await User.Connection!.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var districts = temp.Read<District>();
                    var locations = temp.Read<Location>();
                    var streets = temp.Read<Street>();
                    var addresses = temp.Read<Address>();
                    DbList = addresses.Select(x => (BaseAddress)new Address
                    {
                        Id = x.Id,
                        RoleId = x.RoleId,
                        CityId = x.CityId,
                        City = x.CityId != 0 ? cities.First(c => c.Id == x.CityId) : new(),
                        DistrictId = x.DistrictId,
                        District = x.DistrictId != 0 ? districts.First(d => d.Id == x.DistrictId) : new(),
                        LocationId = x.LocationId,
                        Location = x.LocationId != 0 ? locations.First(l => l.Id == x.LocationId) : new(),
                        StreetId = x.StreetId,
                        Street = x.StreetId != 0 ? streets.First(s => s.Id == x.StreetId) : new(),
                        HouseNum = x.HouseNum,
                        Parameters = x.UpdateParameters()
                    }).Where(a => id == 0 ? a.SearchString().Contains(search) : a.Id == id)
                      .Where(a => a.Id != 0).OrderBy(a => a.City.Name).ThenBy(a => a.Street.Name).ToList();
                }
                User.Close();
            }
        }
        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert addresses
                    (roleId, cityId, districtId, locationId, streetId, houseNum)
                    values (
                    @{nameof(Address.RoleId)},
                    @{nameof(Address.CityId)},
                    @{nameof(Address.DistrictId)},
                    @{nameof(Address.LocationId)},
                    @{nameof(Address.StreetId)},
                    @{nameof(Address.HouseNum)})";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }

        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update addresses set 
                    roleId = @{nameof(Address.RoleId)},
                    cityId = @{nameof(Address.CityId)},
                    districtId = @{nameof(Address.DistrictId)},
                    locationId = @{nameof(Address.LocationId)},
                    streetId = @{nameof(Address.StreetId)},
                    houseNum = @{nameof(Address.HouseNum)}
                    where Id = @{nameof(Address.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
        public override async Task DeleteSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"delete from addresses 
                                        where Id = @{nameof(Address.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
    }
}