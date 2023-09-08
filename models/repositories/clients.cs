using Connection;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;


namespace Models
{
    public class Clients
    {
        List<Client> ClientList { get; set; }

        public Clients()
        {
            ClientList = new();
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
                            select * from addresses as a;
                            select w.id, w.name, w.surname from workers as w;
                            select * from clients";

                using (var temp = await user.Connection.QueryMultipleAsync(sql))
                {
                    var cities = temp.Read<City>();
                    var districts = temp.Read<District>();
                    var locations = temp.Read<Location>();
                    var streets = temp.Read<Street>();
                    var addresses = temp.Read<Address>();
                    var workers = temp.Read<Worker>();
                    var clients = temp.Read<Client>();
                    var addressList = addresses.Select(x => new Address
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
                    }).Where(a => a.Id == clients.Where(c => c.AddressId == a.Id).First().AddressId).ToList(); // !!!
                    ClientList = clients.Select(x => new Client
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Phone = x.Phone,
                        Address = addressList.Where(a => a.Id == x.AddressId).First(),
                        Contact = x.Contact,
                        Agreement = x.Agreement,
                        Comment = x.Comment,
                        Owner = workers.Where(w => w.Id == x.OwnerId).First(),
                        ToDelete = x.ToDelete
                    }).ToList();
                }
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
}