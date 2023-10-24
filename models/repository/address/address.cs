using Connection;
using static InOut;

namespace Models
{
    public class Address
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int StreetId { get; set; }
        public virtual Street Street { get; set; }
        public string HouseNum { get; set; }
        public string FlatNum { get; set; }
        public string FullAddress
        {
            get
            {
                if (CityId == 1) return $"{City.Name,-15}{Location.Name,-17}{Street.Name,-28}{HouseNum,-12}";
                else return $"{City.Name,-15}{Street.Name,-28}{HouseNum,-12}";
            }

        }
        public string ShortAddress
        {
            get
            {
                if (CityId == 1) return $"{Street.Name}, {HouseNum}";
                else return $"{City.Name}, {Street.Name}, {HouseNum}";
            }
        }
        public string RegAddress => $"{City.Name,-15}{Street.Name,-28}{HouseNum,-12}-{FlatNum,-10}";

        public Address()
        {
            Id = 999;
            City = new();
            District = new();
            Location = new();
            Street = new();
            HouseNum = string.Empty;
            FlatNum = string.Empty;
        }
        public override string ToString() => $"{FullAddress}";
    }
    public class Addresses
    {
        public List<Address> AddressList { get; set; }
        public bool IsEmpty
        {
            get => !AddressList.Any();
        }
        public Addresses()
        {
            AddressList = new();
        }
        public void Append(Address address) => AddressList.Add(address);
        public void Clear() => AddressList.Clear();
        public Address GetFromList(int index = 1) => AddressList[index - 1];
        public Address GetById(int id) => AddressList.Where(a => a.Id == id).First();

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
        public List<string> ToStringList(bool forPassport = false)
        {
            List<string> output = new();
            if (forPassport)
                foreach (var item in AddressList)
                    output.Add(item.RegAddress);
            else
                foreach (var item in AddressList)
                    output.Add(item.FullAddress);
            return output;
        }


        // Работа с SQL
        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from cities as c;
                            select * from districts as d;
                            select * from locations as l;
                            select * from streets as s;
                            select * from addresses as a";

                using (var temp = await user.Connection.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var districts = temp.Read<District>();
                    var locations = temp.Read<Location>();
                    var streets = temp.Read<Street>();
                    var addresses = temp.Read<Address>();
                    AddressList = addresses.Select(x => new Address
                    {
                        Id = x.Id,
                        CityId = x.CityId,
                        City = cities.Where(c => c.Id == x.CityId).First(),
                        DistrictId = x.DistrictId,
                        District = districts.Where(d => d.Id == x.DistrictId).First(),
                        LocationId = x.LocationId,
                        Location = locations.Where(l => l.Id == x.LocationId).First(),
                        StreetId = x.StreetId,
                        Street = streets.Where(s => s.Id == x.StreetId).First(),
                        HouseNum = x.HouseNum,
                        FlatNum = x.FlatNum
                    }).Where(a => a.FullAddress.PrepareToSearch().Contains(search)).ToList();
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
                    (cityId, districtId, locationId, streetId, houseNum, flatNum)
                    values (
                    @{nameof(Address.CityId)},
                    @{nameof(Address.DistrictId)},
                    @{nameof(Address.LocationId)},
                    @{nameof(Address.StreetId)},
                    @{nameof(Address.HouseNum)},
                    @{nameof(Address.FlatNum)})";
                await user.Connection.ExecuteAsync(selectQuery, AddressList);
                user.Close();
            }
        }

        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update Addresses set 
                    cityId = @{nameof(Address.CityId)},
                    districtId = @{nameof(Address.DistrictId)},                    
                    locationId = @{nameof(Address.LocationId)},                    
                    streetId = @{nameof(Address.StreetId)},
                    houseNum = @{nameof(Address.HouseNum)},
                    flatNum = @{nameof(Address.FlatNum)}
                    where Id = @{nameof(Address.Id)};";
                await user.Connection.ExecuteAsync(selectQuery, AddressList);
                user.Close();
            }
        }

        public async Task<Address> SaveGetId(DBConnection user, Address address) // получение Id из SQL для нового клиента 
        {
            if (address.CityId == 0) return address;
            Clear();
            Append(address);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, address.RegAddress);
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


    }
}