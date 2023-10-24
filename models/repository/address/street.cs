using Connection;
using static InOut;

namespace Models

{
    public class Street
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public Street()
        {
            Id = 999;
            City = new();
            Name = String.Empty;
        }
        public override string ToString() => $"{City.Name}, {Name}";
    }

    public class Streets : IEnumerable
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

        public IEnumerator GetEnumerator() => StreetsList.GetEnumerator();
        public void Clear() => StreetsList.Clear();
        public void Append(Street street) => StreetsList.Add(street);
        public Street GetFromList(int index = 1) => StreetsList[index - 1];
        public Street GetByName(string name) => StreetsList.Where(s => s.Name == name).First();
        public async Task GetFromSqlAsync(DBConnection user, string search = "", int id = 1)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from streets as s, cities as c 
                                    where s.cityId=c.Id 
                                    and c.Id = {id}
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
        public async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from streets 
                                        where Id = @{nameof(Street.Id)};";
                await user.Connection.ExecuteAsync(selectQuery, StreetsList);
                user.Close();
            }
        }

        public async Task<Street> SaveGetId(DBConnection user, Street street) // получение Id из SQL для новой улицы 
        {
            if (street.Name == String.Empty) return street;
            Clear();
            Append(street);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, street.Name, street.CityId);
            street = GetFromList();
            return street;
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