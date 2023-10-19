using Connection;
using static InOut;

namespace Models
{
    public class District
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CityId { get; private set; }
        public virtual City City { get; set; }

        public District()
        {
            City = new();
        }

        public override string ToString() => $"{Name}, {City.Name}";
    }

    public class Districts : IEnumerable
    {
        List<District> DistrictsList { get; set; }
        public bool IsEmpty
        {
            get => (!DistrictsList.Any());
        }
        public Districts()
        {
            DistrictsList = new();
        }
        public IEnumerator GetEnumerator() => DistrictsList.GetEnumerator();
        public District GetFromList(int index) => DistrictsList[index - 1];
        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from districts as d, cities as c 
                                    where d.cityId=c.Id 
                                    and (d.name like ""%{search}%"")
                                    order by d.name";
                var temp = await user.Connection.QueryAsync<District, City, District>(selectQuery, (d, c) =>
                {
                    d.City = c;
                    return d;
                });
                DistrictsList = temp.ToList();
                user.Close();
            }
        }
        public List<string> ToStringList()
        {
            List<string> output = new List<string>();
            foreach (var item in DistrictsList)
                output.Add(item.ToString());
            return output;
        }

    }
}