using Connection;

namespace Models

{
    public class City : IModels, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SearchString => Name.PrepareToSearch();
        public virtual Districts Districts { get; private set; }
        public virtual Locations Locations { get; private set; }
        public virtual Streets Streets { get; private set; }

        public City()
        {
            Name = string.Empty;
            Districts = new();
            Locations = new();
            Streets = new();
        }
        public void Change(string name)
        {
            if (name != string.Empty) Name = name;
        }

        public override string ToString() => $"{Name}";

        public object Clone()
        {
            City city = (City)MemberwiseClone();
            city.Districts = Districts;
            city.Locations = Locations;
            city.Streets = Streets;
            return city;
        }
    }





    public class Cities
    {
        List<City> CitiesList { get; set; }
        public bool IsEmpty => !CitiesList.Any();
        public Cities()
        {
            CitiesList = new();
        }
        public void Clear() => CitiesList.Clear();
        public void Append(City city) => CitiesList.Add(city);
        public City GetFromList(int index = 1) => CitiesList[index - 1];
        public City GetByName(string name) => CitiesList.Where(s => s.Name == name).First();
        public async Task GetFromSqlAsync(DBConnection user, string search = "", int id = 0)
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from cities as c 
                                    where c.name like ""%{search}%""
                                    order by c.Id";
                var temp = await user.Connection!.QueryAsync<City>(selectQuery);
                CitiesList = temp.Where(c => id == 0 ? c.SearchString.Contains(search) : c.Id == id).ToList();
                user.Close();
            }
        }
        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert cities 
                    (name)
                    values (@{nameof(City.Name)})";
                await user.Connection!.ExecuteAsync(selectQuery, CitiesList);
                user.Close();
            }
        }

        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update cities set
                    name = @{nameof(City.Name)}
                    where Id = @{nameof(City.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, CitiesList);
                user.Close();
            }
        }

        public async Task<bool> CheckExist(DBConnection user, City city) // Проверка, есть ли уже клиент в базе 
        {
            Clear();
            Append(city);
            await GetFromSqlAsync(user, city.SearchString);
            if (IsEmpty) return false;
            else return true;
        }
        public async Task<City> SaveGetId(DBConnection user, City city) // получение Id из SQL для нового города 
        {
            if (city.Name == string.Empty) return city;
            Clear();
            Append(city);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, city.SearchString);
            city = GetFromList();
            return city;
        }

        public async Task<City> SaveChanges(DBConnection user, City city) // получение Id из SQL для нового района
        {
            if (city.Name == String.Empty) return city;
            Clear();
            Append(city);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, id: city.Id);
            city = GetFromList();
            return city;
        }

        public List<string> ToStringList()
        {
            List<string> output = new();
            for (int i = 0; i < CitiesList.Count; i++)
            {
                output.Add(CitiesList[i].ToString());
            }
            return output;
        }
    }
}
