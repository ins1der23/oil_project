using Connection;
using MenusAndChoices;
using static InOut;

namespace Models

{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Districts Districts { get; private set; }
        public virtual Locations Locations { get; private set; }
        public virtual Streets Streets { get; private set; }

        public City()
        {
            Name = String.Empty;
            Districts = new();
            Locations = new();
            Streets = new();
        }

        public override string ToString() => $"{Name}";

    }
    public class Cities
    {
        List<City> CitiesList { get; set; }
        public bool IsEmpty
        {
            get => (!CitiesList.Any());
        }

        public Cities()
        {
            CitiesList = new();
        }
        public void Clear() => CitiesList.Clear();
        public void Append(City city) => CitiesList.Add(city);
        public City GetFromList(int index = 1) => CitiesList[index - 1];
        public City GetByName(string name) => CitiesList.Where(s => s.Name == name).First();
        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from cities as c 
                                    where c.name like ""%{search}%""
                                    order by c.Id";
                var temp = await user.Connection.QueryAsync<City>(selectQuery);
                CitiesList = temp.ToList();
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
                await user.Connection.ExecuteAsync(selectQuery, CitiesList);
                user.Close();
            }
        }

        public async Task<City> SaveGetId(DBConnection user, City city) // получение Id из SQL для новой улицы 
        {
            if (city.Name == String.Empty) return city;
            Clear();
            Append(city);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, city.Name);
            city = GetFromList();
            return city;
        }
        public List<string> ToStringList()
        {
            List<string> output = new();
            for (int i = 1; i < CitiesList.Count; i++)
            {
                output.Add(CitiesList[i].ToString());
            }
            return output;
        }
    }
}
