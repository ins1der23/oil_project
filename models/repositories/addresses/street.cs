using MySql.Data.MySqlClient;
using Dapper;
using Connection;
using static InOut;

namespace Models

{
    public class Street
    {
        static int nextId;
        public int Id { get; private set; }
        public string? Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public Street()
        {
            Id = Interlocked.Increment(ref nextId);
            City = new();
        }
        public void Create() => Name = GetString(MenuText.streetName);
        public override string ToString() => $"{City.Name}, ID:{Id}, {Name}";
    }

    public class Streets
    {
        List<Street> StreetsList
        { get; set; }
        public bool IsEmpty
        {
            get => (!StreetsList.Any());
        }

        public Streets()
        {
            StreetsList = new();
        }

        public void Clear() => StreetsList = new();
        public void Append(Street street) => StreetsList.Add(street);
        public Street GetFromList(int index) => StreetsList[index - 1];
        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from streets as s, cities as c 
                                    where s.cityId=c.Id 
                                    and (s.name like ""%{search}%"")
                                    order by s.name";
                var temp = await user.Connection.QueryAsync<Street, City, Street>(selectQuery, (s, c) =>
                {
                    s.City = c;
                    return s;
                });
                StreetsList = temp.ToList();
                user.Close();
            }
        }

        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert streets
                    (name, cityId)
                    values (
                    @{nameof(Street.Name)},
                    @{nameof(Street.CityId)})";
                await user.Connection.ExecuteAsync(selectQuery, StreetsList);
                user.Close();
            }
        }
        public List<string> ToStringList()
        {
            List<string> output = new List<string>();
            foreach (var item in StreetsList)
                output.Add(item.ToString());
            return output;
        }
    }
}