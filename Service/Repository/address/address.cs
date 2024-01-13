using Connection;
using Service;

namespace Models
{
    public class Addresses
    {
        public List<Address> AddressList { get; set; }
        public bool IsEmpty => !AddressList.Any();
        public Addresses()
        {
            AddressList = new();
        }
        public void Append(Address address) => AddressList.Add(address);
        public void Clear() => AddressList.Clear();
        public Address GetFromList(int index = 1)
        {
            if (!IsEmpty) return AddressList[index - 1];
            return new Address();
        }


        public List<Address> ToWorkingList() => AddressList.Select(c => c).ToList(); // Список для работы с LINQ
        public void ToWriteList(List<Address> toAddList) // Список для записи в БД 
        {
            AddressList.Clear();
            AddressList = toAddList.Select(c => c).ToList();
        }


        /// <summary>
        /// Формирование списка из AddressList для создания меню 
        /// </summary>
        /// <returns> список из Address.ToString()</returns>
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (var item in AddressList)
                output.Add(item.LongString);
            return output;
        }


        // Работа с SQL
        public async Task GetFromSqlAsync(DBConnection user, string search = "", int id = 0)
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from districts as d;
                            select * from locations as l;
                            select * from streets as s;
                            select * from addresses as a where roleId = 1";

                using (var temp = await user.Connection!.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var districts = temp.Read<District>();
                    var locations = temp.Read<Location>();
                    var streets = temp.Read<Street>();
                    var addresses = temp.Read<Address>();
                    AddressList = addresses.Select(x => new Address
                    {
                        Id = x.Id,
                        RoleId = x.RoleId,
                        CityId = x.CityId,
                        City = cities.First(c => c.Id == x.CityId),
                        DistrictId = x.DistrictId,
                        District = x.DistrictId != 0 ? districts.First(d => d.Id == x.DistrictId) : new(),
                        LocationId = x.LocationId,
                        Location = x.LocationId != 0 ? locations.First(l => l.Id == x.LocationId) : new(),
                        StreetId = x.StreetId,
                        Street = x.StreetId != 0 ? streets.Where(s => s.Id == x.StreetId).First() : new(),
                        HouseNum = x.HouseNum
                        // Parameters = x.UpdateParameters()
                    }).Where(a => id == 0 ? a.SearchString().Contains(search) : a.Id == id).ToList();
                }
                user.Close();
            }
        }
        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert Addresses
                    (roleId, cityId, districtId, locationId, streetId, houseNum)
                    values (
                    @{nameof(Address.RoleId)},
                    @{nameof(Address.CityId)},
                    @{nameof(Address.DistrictId)},
                    @{nameof(Address.LocationId)},
                    @{nameof(Address.StreetId)},
                    @{nameof(Address.HouseNum)})";
                await user.Connection!.ExecuteAsync(selectQuery, AddressList);
                user.Close();
            }
        }

        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update Addresses set 
                    roleId = @{nameof(Address.RoleId)},
                    cityId = @{nameof(Address.CityId)},
                    districtId = @{nameof(Address.DistrictId)},
                    locationId = @{nameof(Address.LocationId)},
                    streetId = @{nameof(Address.StreetId)},
                    houseNum = @{nameof(Address.HouseNum)}
                    where Id = @{nameof(Address.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, AddressList);
                user.Close();
            }
        }

        public async Task<bool> CheckExist(DBConnection user, Address address) // Проверка, есть ли уже клиент в базе 
        {
            Clear();
            Append(address);
            await GetFromSqlAsync(user, address.SearchString());
            if (IsEmpty) return false;
            else return true;
        }

        public async Task<Address> SaveGetId(DBConnection user, Address address) // получение Id из SQL для нового клиента 
        {
            if (address.CityId == 0) return address;
            Clear();
            Append(address);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, address.SearchString());
            address = GetFromList();
            return address;
        }
        public async Task<Address> SaveChanges(DBConnection user, Address address) // получение Id из SQL для нового микрорайона
        {
            if (address.CityId == 0) return address;
            Clear();
            Append(address);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, id: address.Id);
            address = GetFromList();
            return address;
        }

        public override string ToString()
        {
            string output = String.Empty;
            foreach (var address in AddressList)
                output += address.ToString() + "\n";
            return output;
        }

        public async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from addresses 
                                        where Id = @{nameof(Address.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, AddressList);
                user.Close();
            }
        }
    }
}