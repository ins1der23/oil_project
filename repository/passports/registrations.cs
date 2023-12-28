using Connection;
namespace Models
{

    public class Registrations : BaseList<Registration>
    {
        public Registrations()
        : base() { }


        public override async Task GetFromSqlAsync(DBConnection user, Registration? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from streets as s;
                            select * from registrations as r";

                using (var temp = await user.Connection!.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var streets = temp.Read<Street>();
                    var registrations = temp.Read<Registration>();
                    dbList = registrations.Select(x => new Registration
                    {
                        Id = x.Id,
                        CityId = x.CityId,
                        City = cities.First(c => c.Id == x.CityId),
                        StreetId = x.StreetId,
                        Street = streets.First(s => s.Id == x.StreetId),
                        HouseNum = x.HouseNum,
                        FlatNum = x.FlatNum
                    }).Where(r => id == 0 ? r.SearchString().Contains(search) : r.Id == id).ToList();
                }
                user.Close();
            }
        }

        public override async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert registrations
                    (cityId, streetId, houseNum, flatNum)
                    values (
                    @{nameof(Registration.CityId)},
                    @{nameof(Registration.StreetId)},
                    @{nameof(Registration.HouseNum)},
                    @{nameof(Registration.FlatNum)})";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
        public override async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update Registrations set 
                    cityId = @{nameof(Registration.CityId)},
                    streetId = @{nameof(Registration.StreetId)},
                    houseNum = @{nameof(Registration.HouseNum)},
                    flatNum = @{nameof(Registration.FlatNum)}
                    where Id = @{nameof(Registration.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }

        public override async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from registrations 
                                        where Id = @{nameof(Registration.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
    }
}
