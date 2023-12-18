using Connection;
namespace Models
{

    public class Registrations : IRepository
    {
        List<Registration> RegistrationList { get; set; }
        public bool IsEmpty => !RegistrationList.Any();

        public Registrations()
        {
            RegistrationList = new();
        }

        public void Clear() => RegistrationList.Clear();
        public void Append(Registration registration) => RegistrationList.Add(registration);
        public Registration GetFromList(int index = 1)
        {
            if (!IsEmpty) return RegistrationList[index - 1];
            return new Registration();
        }
        public List<Registration> ToWorkingList() => RegistrationList.Select(c => c).ToList(); // Список для работы с LINQ

        public async Task GetFromSqlAsync(DBConnection user, string search = "", int id = 0)
        {
            search = search.PrepareToSearch();
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
                    RegistrationList = registrations.Select(x => new Registration
                    {
                        Id = x.Id,
                        CityId = x.CityId,
                        City = cities.First(c => c.Id == x.CityId),
                        StreetId = x.StreetId,
                        Street = streets.First(s => s.Id == x.StreetId),
                        HouseNum = x.HouseNum,
                        FlatNum = x.FlatNum
                    }).Where(r => id == 0 ? r.SearchString.Contains(search) : r.Id == id).ToList();
                }
                user.Close();
            }
        }

        public async Task AddSqlAsync(DBConnection user)
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
                await user.Connection!.ExecuteAsync(selectQuery, RegistrationList);
                user.Close();
            }
        }

        public async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from registrations 
                                        where Id = @{nameof(Registration.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, RegistrationList);
                user.Close();
            }
        }

        public async Task<bool> CheckExist(DBConnection user, Registration registration) // Проверка, есть ли уже клиент в базе 
        {
            Clear();
            Append(registration);
            await GetFromSqlAsync(user, registration.SearchString);
            if (IsEmpty) return false;
            else return true;
        }

        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (var item in RegistrationList)
                output.Add(item.LongString);
            return output;
        }

        public async Task ChangeSqlAsync(DBConnection user)
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
                await user.Connection!.ExecuteAsync(selectQuery, RegistrationList);
                user.Close();
            }
        }
        public async Task<Registration> SaveGetId(DBConnection user, Registration registration) // получение Id из SQL для нового клиента 
        {
            if (registration.CityId == 0) return registration;
            Clear();
            Append(registration);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, registration.SearchString);
            registration = GetFromList();
            return registration;
        }

        public async Task<Registration> SaveChanges(DBConnection user, Registration registration) // получение Id из SQL для нового экземпляра
        {
            if (registration.CityId == 0) return registration;
            Clear();
            Append(registration);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, id: registration.Id);
            registration = GetFromList();
            return registration;
        }
       
    }
}
