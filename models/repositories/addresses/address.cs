using MySql.Data.MySqlClient;
using Dapper;
using Connection;
using System.Linq;
using static InOut;

namespace Models
{
    public class Address
    {
        static int nextId;
        public int Id { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int StreetId { get; set; }
        public virtual Street Street { get; set; }
        public string? HouseNum { get; set; }

        public Address()
        {
            Id = Interlocked.Increment(ref nextId);
            City = new();
            District = new();
            Location = new();
            Street = new();
        }

        public void Create()
        {
            City.Create();
            District.Name = GetString(MenuText.houseNum);
            Location = Location.Create();
            Street = Street.Create();
            HouseNum = GetString(MenuText.houseNum);
        }

        public override string ToString() => $"ID:{Id}, {City.Name}, {Location.Name}, {Street.Name}, {HouseNum}";
    }
    class Addresses
    {
        List<Address> AddressList { get; set; }
        public Addresses()
        {
            AddressList = new();
        }
        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select c.id, c.name from cities as c;
                            select d.id, d.name from districts as d;
                            select l.id, l.name from locations as l;
                            select s.id, s.name from streets as s;
                            select a.Id, a.cityId, a.districtId, a.locationId, a.streetId, a.houseNum
                            from addresses as a";

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
                        HouseNum = x.HouseNum
                    }).ToList();
                }
                user.Close();
            }
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