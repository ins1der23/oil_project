using Connection;

namespace Models

{
    public class Street : ICloneable, IModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public string SearchString => Name.PrepareToSearch();

        public Street()
        {
            City = new();
            Name = string.Empty;
        }
        public void Change(string name)
        {
            if (name != string.Empty) Name = name;
        }
        public override string ToString() => $"{City.Name}, {Name}";

        public object Clone()
        {
            Street street = (Street)MemberwiseClone();
            street.City = City;
            return street;
        }


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
        public async Task GetFromSqlAsync(DBConnection user, int cityId, string search = "", int id = 0)
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from streets as s, cities as c 
                                    where s.cityId=c.Id 
                                    and c.Id = {cityId}
                                    and s.name like ""%{search}%""
                                    order by s.name";
                var temp = await user.Connection!.QueryAsync<Street, City, Street>(selectQuery, (s, c) =>
                {
                    s.City = c;
                    return s;
                });
                StreetsList = temp.Where(s => id == 0 ? s.SearchString.Contains(search) : s.Id == id).ToList();
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
                await user.Connection!.ExecuteAsync(selectQuery, StreetsList);
                user.Close();
            }
        }
        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update streets set
                    name = @{nameof(Street.Name)},
                    cityId = @{nameof(Street.CityId)}
                    where Id = @{nameof(Street.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, StreetsList);
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
                await user.Connection!.ExecuteAsync(selectQuery, StreetsList);
                user.Close();
            }
        }

        public async Task<bool> CheckExist(DBConnection user, Street street) // Проверка, есть ли уже клиент в базе 
        {
            Clear();
            Append(street);
            await GetFromSqlAsync(user, cityId: street.CityId, street.SearchString);
            if (IsEmpty) return false;
            else return true;
        }
        // получение Id из SQL для новой улицы 
        public async Task<Street> SaveGetId(DBConnection user, Street street)
        {
            if (street.Name == String.Empty) return street;
            Clear();
            Append(street);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, cityId: street.CityId, street.SearchString);
            street = GetFromList();
            return street;
        }
        // сохранение именений и получение из SQL измененной улицы
        public async Task<Street> SaveChanges(DBConnection user, Street street)
        {
            if (street.Name == string.Empty) return street;
            Clear();
            Append(street);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, cityId: street.City.Id, id: street.Id);
            street = GetFromList();
            return street;
        }
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (var item in StreetsList)
                output.Add(item.ToString());
            return output;
        }

    }
}