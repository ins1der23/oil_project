using Connection;
namespace Models;

public class Passports
{
    List<Passport> PassportList { get; set; }

    public Passports()
    {
        PassportList = new();
    }

    public async Task GetFromSqlAsync(DBConnection user, string search = "")
    {
        search = search.PrepareToSearch();
        await user.ConnectAsync();
        if (user.IsConnect)
        {
            string sql = @" select c.id, c.name from cities as c;
                            select d.id, d.name from districts as d;
                            select l.id, l.name from locations as l;
                            select s.id, s.name from streets as s;
                            select * from addresses as a;
                            select * from clients as cl;
                            select * from passports;";

            using (var temp = await user.Connection.QueryMultipleAsync(sql))
            {
                var cities = temp.Read<City>();
                var districts = temp.Read<District>();
                var locations = temp.Read<Location>();
                var streets = temp.Read<Street>();
                var addresses = temp.Read<Address>();
                var clients = temp.Read<Client>();
                var passports = temp.Read<Passport>();
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
                    HouseNum = x.HouseNum,
                    FlatNum = x.FlatNum
                }).ToList();
                PassportList = passports.Select(x => new Passport
                {
                    Id = x.Id,
                    Serial = x.Serial,
                    Number = x.Number,
                    IssueAuthority = x.IssueAuthority,
                    IssueDate = x.IssueDate,
                    RegistrationId = x.RegistrationId,
                    Registration = addressList.Where(a => a.Id == x.RegistrationId).First(),
                    Client = clients.Where(cl => cl.Id == x.ClientId).First(),
                }).Where(p => p.SearchString.PrepareToSearch().Contains(search)).ToList();
            }
            user.Close();
        }
    }

    // public async Task AddSqlAsync(DBConnection user)
    //     {
    //         await user.ConnectAsync();
    //         if (user.IsConnect)
    //         {
    //             string selectQuery = $@"insert passports
    //                 (name, addressId, phone, comment, ownerId, toDelete)
    //                 values (
    //                 @{nameof(Client.Name)},
    //                 @{nameof(Client.AddressId)},
    //                 @{nameof(Client.Phone)},
    //                 @{nameof(Client.Comment)},
    //                 @{nameof(Client.OwnerId)},
    //                 @{nameof(Client.ToDelete)})";
    //             await user.Connection.ExecuteAsync(selectQuery, ClientList);
    //             user.Close();
    //         }
    //     }
}
