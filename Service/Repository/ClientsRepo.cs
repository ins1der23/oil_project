using Connection;


namespace Models
{
    public abstract class ClientsRepo : BaseRepo<Client>
    {

        public override async Task GetFromSqlAsync(Client? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            Addresses addressSql = new();
            await addressSql.GetFromSqlAsync(User);
            var addressList = addressSql.ToWorkingList();
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string sql = @"select * from workers as w;
                            select * from agreements as agr;
                            select * from passports as p;
                            select * from clients";
                using (var temp = await User.Connection!.QueryMultipleAsync(sql))
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
                User.Close();
            }
        }
        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
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
                await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }
        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update clients set
                    name = @{nameof(Client.Name)},
                    addressId = @{nameof(Client.AddressId)},
                    phone = @{nameof(Client.Phone)},
                    comment = @{nameof(Client.Comment)},
                    ownerId = @{nameof(Client.OwnerId)},
                    toDelete = @{nameof(Client.ToDelete)}
                    where Id = @{nameof(Client.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }
        public override async Task DeleteSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"delete from clients 
                                        where Id = @{nameof(Client.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }
    }
}