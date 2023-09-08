using MySql.Data.MySqlClient;
using Dapper;
using Connection;
using static InOut;
using System.Collections;

namespace Models
{
    public class Location
    {
        static int nextId;
        public int Id { get; private set; }
        public string? Name { get; set; }
        public int CityId { get; private set; }
        public virtual City City { get; set; }

        public Location()
        {
            Id = Interlocked.Increment(ref nextId);
            City = new();
        }
        public void Create() => Name = GetString(MenuText.locationName);

        public override string ToString() => $"{City.Name}, ID:{Id}, {Name}";
    }

    public class Locations : IEnumerable
    {
        List<Location> LocationsList { get; set; }
        public bool IsEmpty
        {
            get => (!LocationsList.Any());
        }

        public Locations()
        {
            LocationsList = new();
        }
        public IEnumerator GetEnumerator() => LocationsList.GetEnumerator();
        public Location GetFromList(int index) => LocationsList[index - 1];
        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from locations as l, cities as c 
                                    where l.cityId=c.Id 
                                    and (l.name like ""%{search}%"")
                                    order by l.name";
                var temp = await user.Connection.QueryAsync<Location, City, Location>(selectQuery, (l, c) =>
                {
                    l.City = c;
                    return l;
                });
                LocationsList = temp.ToList();
                user.Close();
            }
        }
        public List<string> ToStringList()
        {
            List<string> output = new List<string>();
            foreach (var item in LocationsList)
                output.Add(item.ToString());
            return output;
        }
    }
}