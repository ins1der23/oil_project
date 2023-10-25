using Connection;
namespace Models
{

    public class Registrations : IRepository
    {
        List<Registration> RegistrationList { get; set; }

        public Registrations()
        {
            RegistrationList = new();
        }

        public void Clear() => RegistrationList.Clear();

        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from streets as s;
                            select * from registrations as r";

                using (var temp = await user.Connection.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var streets = temp.Read<Street>();
                    var registrations = temp.Read<Registration>();
                    RegistrationList = registrations.Select(x => new Registration
                    {
                        Id = x.Id,
                        CityId = x.CityId,
                        City = cities.First(c => c.Id == x.CityId),
                        StreetId = x.StreetId,
                        Street = streets.First(s => s.Id == x.StreetId),
                        HouseNum = x.HouseNum
                    }).Where(a => a.LongString.PrepareToSearch().Contains(search)).ToList();
                }
                user.Close();
            }
        }

        public Task AddSqlAsync(DBConnection user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSqlAsync(DBConnection user)
        {
            throw new NotImplementedException();
        }
        public List<string> ToStringList()
        {
            throw new NotImplementedException();
        }
    }
}
