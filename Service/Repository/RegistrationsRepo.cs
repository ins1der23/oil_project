namespace Models
{
    public abstract class RegistrationsRepo : BaseRepo<Registration>
    {
        public RegistrationsRepo() : base() { }

        public override async Task GetFromSqlAsync(Registration? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from streets as s;
                            select * from registrations as r";

                using (var temp = await User.Connection!.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var streets = temp.Read<Street>();
                    var registrations = temp.Read<Registration>();
                    DbList = registrations.Select(x => new Registration
                    {
                        Id = x.Id,
                        CityId = x.CityId,
                        City = x.CityId != 0 ? cities.First(c => c.Id == x.CityId) : new(),
                        StreetId = x.StreetId,
                        Street = x.StreetId != 0 ? streets.First(s => s.Id == x.StreetId) : new(),
                        HouseNum = x.HouseNum,
                        FlatNum = x.FlatNum,
                        Parameters = x.UpdateParameters()
                    }).Where(r => id == 0 ? r.SearchString().Contains(search) : r.Id == id)
                      .Where(r => r.Id != 0).ToList();
                }
                User.Close();
            }
        }
        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert registrations
                    (cityId, streetId, houseNum, flatNum)
                    values (
                    @{nameof(Registration.CityId)},
                    @{nameof(Registration.StreetId)},
                    @{nameof(Registration.HouseNum)},
                    @{nameof(Registration.FlatNum)})";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update Registrations set 
                    cityId = @{nameof(Registration.CityId)},
                    streetId = @{nameof(Registration.StreetId)},
                    houseNum = @{nameof(Registration.HouseNum)},
                    flatNum = @{nameof(Registration.FlatNum)}
                    where Id = @{nameof(Registration.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
        public override async Task DeleteSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"delete from registrations 
                                        where Id = @{nameof(Registration.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
    }
}
