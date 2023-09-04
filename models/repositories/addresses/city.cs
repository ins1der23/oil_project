using MySql.Data.MySqlClient;
using Dapper;
using Connection;
using static InOut;
namespace Models

{
    public class City
    {
        static int nextId;
        public int Id { get; private set; }
        public string? Name { get; set; }
        public virtual Districts Districts { get; private set; }
        public virtual Locations Locations { get; private set; }
        public virtual Streets Streets { get; private set; }

        public City()
        {
            Id = Interlocked.Increment(ref nextId);
            Districts = new();
            Locations = new();
            Streets = new();
        }

        public void Create() => Name = GetString(MenuText.cityName);
        public override string ToString() => $"ID:{Id}, {Name}";
        
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
        public void Append(City city) => CitiesList.Add(city);
        public City GetFromList(int index) => CitiesList[index - 1];
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

        public List<string> ToStringList()
        {
            List<string> output = new List<string>();
            foreach (var item in CitiesList)
                output.Add(item.ToString());
            return output;
        }
    }
}
