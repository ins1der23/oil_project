using Connection;

namespace Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string SearchString => Name.PrepareToSearch();

        public District()
        {
            Name = string.Empty;
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
        public void Clear() => DistrictsList.Clear();
        public void Append(District district) => DistrictsList.Add(district);
        public District GetFromList(int index = 1) => DistrictsList[index - 1];
        public async Task GetFromSqlAsync(DBConnection user, int cityId, string search = "", int id = 0)
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from districts as d, cities as c 
                                    where d.cityId=c.Id 
                                    and c.Id = {cityId}
                                    and d.name like ""%{search}%""
                                    order by d.name";
                var temp = await user.Connection!.QueryAsync<District, City, District>(selectQuery, (d, c) =>
                {
                    d.City = c;
                    return d;
                });
                DistrictsList = temp.Where(d => id == 0 ? d.SearchString.Contains(search) : d.Id == id).ToList();
                user.Close();
            }
        }
        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert districts 
                    (name, cityId)
                    values (@{nameof(District.Name)},
                            @{nameof(District.CityId)})";
                await user.Connection!.ExecuteAsync(selectQuery, DistrictsList);
                user.Close();
            }
        }
        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update districts set
                    name = @{nameof(District.Name)},
                    cityId = @{nameof(District.CityId)}
                    where Id = @{nameof(District.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, DistrictsList);
                user.Close();
            }
        }
        public async Task<District> SaveGetId(DBConnection user, District district) // получение Id из SQL для нового района
        {
            if (district.Name == String.Empty) return district;
            Clear();
            Append(district);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, cityId: district.CityId, district.Name);
            district = GetFromList();
            return district;
        }

        public async Task<District> SaveChanges(DBConnection user, District district) // получение Id из SQL для нового района
        {
            if (district.Name == String.Empty) return district;
            Clear();
            Append(district);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, cityId: district.CityId, id: district.Id);
            district = GetFromList();
            return district;
        }
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (var item in DistrictsList)
                output.Add(item.ToString());
            return output;
        }

    }
}