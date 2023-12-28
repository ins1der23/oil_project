using Connection;


public class TempClient
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public int CityId { get; set; }
    public string? District { get; set; }
    public int DistrictId { get; set; }
    public string? Location { get; set; }
    public int LocationId { get; set; }
    public string? Street { get; set; }
    public int StreetId { get; set; }
    public string? HouseNum { get; set; }
    public string FullName
    {
        get
        {
            return $"{Name,-35}{Location,-15}{Street,-35}{HouseNum,-20}";
        }
    }
    public int AddressId { get; set; }
    public double Phone { get; set; }
    public string? Comment { get; set; }

    public override string ToString()
    {
        return $"{Id,-5}{FullName}{Phone,-10}";
    }
}
public class TempClients : IEnumerable
{
    List<TempClient> ClientList { get; set; }
    public TempClients()
    {
        ClientList = new List<TempClient>();
    }
    public IEnumerator GetEnumerator() => ClientList.GetEnumerator();
    public void Clear() => ClientList.Clear();
    public void Add(TempClient client) => ClientList.Add(client);
    public int Count() => ClientList.Count();

    public void ToWriteList(List<TempClient> toAddList)
    {
        ClientList.Clear();
        ClientList = toAddList.Select(c => c).ToList();
    }

    public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from tempClients as c
                                    where c.name like ""%{search}%""
                                    order by c.id";
                var temp = await user.Connection!.QueryAsync<TempClient>(selectQuery);
                ClientList = temp.ToList();
                user.Close();
            }
        }
        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert tempClients
                    (id, name, city, cityId, district, districtId, location, locationId, street, streetId, houseNum, addressId, phone, comment)
                    values (
                    @{nameof(TempClient.Id)},
                    @{nameof(TempClient.Name)},
                    @{nameof(TempClient.City)},
                    @{nameof(TempClient.CityId)},
                    @{nameof(TempClient.District)},
                    @{nameof(TempClient.DistrictId)},
                    @{nameof(TempClient.Location)},
                    @{nameof(TempClient.LocationId)},
                    @{nameof(TempClient.Street)},
                    @{nameof(TempClient.StreetId)},
                    @{nameof(TempClient.HouseNum)},
                    @{nameof(TempClient.AddressId)},
                    @{nameof(TempClient.Phone)},
                    @{nameof(TempClient.Comment)})";
                await user.Connection!.ExecuteAsync(selectQuery, ClientList);
                user.Close();
            }
        }

        public async Task WriteToAddressesSqlAsync(DBConnection user) // запись в основные адреса
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert Addresses
                    (id, cityId, districtId, locationId, streetId, houseNum)
                    values (
                    @{nameof(TempClient.AddressId)},
                    @{nameof(TempClient.CityId)},
                    @{nameof(TempClient.DistrictId)},
                    @{nameof(TempClient.LocationId)},
                    @{nameof(TempClient.StreetId)},
                    @{nameof(TempClient.HouseNum)})";
                await user.Connection!.ExecuteAsync(selectQuery, ClientList);
                user.Close();
            }
        }
        public async Task WriteToClientsSqlAsync(DBConnection user) // запись в основных клиентов 
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert Clients
                    (id, name, addressId, phone, comment)
                    values (
                    @{nameof(TempClient.Id)},
                    @{nameof(TempClient.Name)},
                    @{nameof(TempClient.AddressId)},
                    @{nameof(TempClient.Phone)},
                    @{nameof(TempClient.Comment)})";
                await user.Connection!.ExecuteAsync(selectQuery, ClientList);
                user.Close();
            }
        }
        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update tempClients set 
                    name = @{nameof(TempClient.Name)},
                    city = @{nameof(TempClient.City)},
                    cityId = @{nameof(TempClient.CityId)},
                    district = @{nameof(TempClient.District)},
                    districtId = @{nameof(TempClient.DistrictId)},
                    location = @{nameof(TempClient.Location)},
                    LocationId = @{nameof(TempClient.LocationId)},
                    Street = @{nameof(TempClient.Street)},
                    StreetId = @{nameof(TempClient.StreetId)},
                    HouseNum = @{nameof(TempClient.HouseNum)},
                    AddressId = @{nameof(TempClient.AddressId)},
                    Phone = @{nameof(TempClient.Phone)},
                    Comment = @{nameof(TempClient.Comment)}
                    where Id = @{nameof(TempClient.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, ClientList);
                user.Close();
            }
        }
        public async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from tempClients 
                                        where Id = @{nameof(TempClient.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, ClientList);
                user.Close();
            }
        }

    public override string ToString()
    {
        string output = String.Empty;
        foreach (var item in ClientList)
            output += item.ToString() + "\n";
        return output;
    }
}

