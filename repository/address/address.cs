using Connection;

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
        public string LongString
        {
            get
            {
                if (CityId == 1) return $"{City.Name,-15}{Location.Name,-17}{Street.Name,-28}{HouseNum,-12}";
                else return $"{City.Name,-15}{Street.Name,-28}{HouseNum,-12}";
            }

        }
        public string ShortString
        {
            get
            {
                if (CityId == 1) return $"{Street.Name}, {HouseNum}";
                else return $"{City.Name}, {Street.Name}, {HouseNum}";
            }
        }
        public string SearchString => $"{City.Name}{Street.Name}{HouseNum}".PrepareToSearch();
        public Address()
        {
            City = new();
            District = new();
            Location = new();
            Street = new();
            HouseNum = string.Empty;
        }

        public void Change(City city, District district, Location location, Street street, string houseNum)
        {
            if (city.Id != 0)
            {
                City = city;
                CityId = city.Id;
            }
            if (district.Id != 0)
            {
                District = district;
                DistrictId = district.Id;
            }
            if (location.Id != 0)
            {
                Location = location;
                LocationId = location.Id;
            }
            if (street.Id != 0)
            {
                Street = street;
                StreetId = street.Id;
            }
            if (houseNum != string.Empty) HouseNum = houseNum;
        }

        public object Clone()
        {
            Address address = (Address)MemberwiseClone();
            address.City = City;
            address.District = District;
            address.Location = Location;
            address.Street = Street;
            return address;
        }

        public override string ToString() => $"{LongString}";
    }



    public class Addresses : IRepository
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
                            select * from addresses as a";

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
                        CityId = x.CityId,
                        City = cities.Where(c => c.Id == x.CityId).First(),
                        DistrictId = x.DistrictId,
                        District = districts.Where(d => d.Id == x.DistrictId).First(),
                        LocationId = x.LocationId,
                        Location = locations.Where(l => l.Id == x.LocationId).First(),
                        StreetId = x.StreetId,
                        Street = streets.Where(s => s.Id == x.StreetId).First(),
                        HouseNum = x.HouseNum
                    }).Where(a => id == 0 ? a.SearchString.Contains(search) : a.Id == id).ToList();
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
                    (cityId, districtId, locationId, streetId, houseNum)
                    values (
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
            await GetFromSqlAsync(user, address.SearchString);
            if (IsEmpty) return false;
            else return true;
        }

        public async Task<Address> SaveGetId(DBConnection user, Address address) // получение Id из SQL для нового клиента 
        {
            if (address.CityId == 0) return address;
            Clear();
            Append(address);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, address.SearchString);
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