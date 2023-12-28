using Connection;


namespace Models
{
    public class Clients : BaseList<Client>
    {

        public override async Task GetFromSqlAsync(DBConnection user, Client? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            Addresses addressSql = new();
            await addressSql.GetFromSqlAsync(user);
            var addressList = addressSql.ToWorkingList();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from workers as w;
                            select * from agreements as agr;
                            select * from passports as p;
                            select * from clients";
                using (var temp = await user.Connection!.QueryMultipleAsync(sql))
                {
                    var workers = temp.Read<Worker>();
                    var agreementList = temp.Read<Agreement>();
                    var passportList = temp.Read<Passport>();
                    var clients = temp.Read<Client>();
                    dbList = clients.Select(x => new Client
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Phone = x.Phone,
                        AddressId = x.AddressId,
                        Address = addressList.Where(a => a.Id == x.AddressId).First(),
                        Agreements = agreementList.Where(agr => agr.ClientId == x.Id).ToList(),
                        Passports = passportList.Where(p => p.ClientId == x.Id).ToList(),
                        Comment = x.Comment,
                        OwnerId = x.OwnerId,
                        Owner = workers.Where(w => w.Id == x.OwnerId).First(),
                        ToDelete = x.ToDelete
                    }).Where(c => id == 0 ? c.SearchString().Contains(search) : c.Id == id).ToList();
                }
                user.Close();
            }
        }
        public override async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert clients
                    (name, addressId, phone, comment, ownerId, toDelete)
                    values (
                    @{nameof(Client.Name)},
                    @{nameof(Client.AddressId)},
                    @{nameof(Client.Phone)},
                    @{nameof(Client.Comment)},
                    @{nameof(Client.OwnerId)},
                    @{nameof(Client.ToDelete)})";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
        public override async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update clients set
                    name = @{nameof(Client.Name)},
                    addressId = @{nameof(Client.AddressId)},
                    phone = @{nameof(Client.Phone)},
                    comment = @{nameof(Client.Comment)},
                    ownerId = @{nameof(Client.OwnerId)},
                    toDelete = @{nameof(Client.ToDelete)}
                    where Id = @{nameof(Client.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
        public override async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from clients 
                                        where Id = @{nameof(Client.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
    }
}