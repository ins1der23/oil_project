using Connection;

namespace Models
{
    public class Clients : IRepository
    {
        List<Client> ClientList { get; set; }

        public Clients()
        {
            ClientList = new();
        }
        public bool IsEmpty
        {
            get => (!ClientList.Any());
        }
        public Client GetFromList(int index = 1) => ClientList[index - 1];
        public void Clear() => ClientList.Clear();
        public void Append(Client client) => ClientList.Add(client);
        public List<Client> ToWorkingList() => ClientList.Select(c => c).ToList();
        public void ToWriteList(List<Client> toAddList)
        {
            ClientList.Clear();
            ClientList = toAddList.Select(c => c).ToList();
        }

        /// <summary>
        /// Формирование списка из ClientList для создания меню 
        /// </summary>
        /// <returns> список из Client.ToString()</returns>
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (var item in ClientList)
                output.Add(item.ToString());
            return output;
        }
        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            search = search.PrepareToSearch();
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
                    ClientList = clients.Select(x => new Client
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
                    }).Where(c => c.FullName.PrepareToSearch().Contains(search)).ToList();
                }
                user.Close();
            }
        }

        public async Task AddSqlAsync(DBConnection user)
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
                await user.Connection!.ExecuteAsync(selectQuery, ClientList);
                user.Close();
            }
        }

        public async Task ChangeSqlAsync(DBConnection user)
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
                await user.Connection!.ExecuteAsync(selectQuery, ClientList);
                user.Close();
            }
        }
        public async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from clients 
                                        where Id = @{nameof(Client.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, ClientList);
                user.Close();
            }
        }

        public async Task<bool> CheckExist(DBConnection user, Client client) // Проверка, есть ли уже клиент в базе 
        {
            Clear();
            Append(client);
            await GetFromSqlAsync(user, client.SearchString);
            if (IsEmpty) return false;
            else return true;
        }

        public async Task<Client> SaveGetId(DBConnection user, Client client) // получение Id из SQL для нового клиента 
        {
            if (client.AddressId == 0 || client.Name == string.Empty) return client;
            Clear();
            Append(client);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, client.SearchString);
            client = GetFromList();
            return client;
        }

        public async Task<Client> SaveChanges(DBConnection user, Client client) // сохранение измениий и возврат измененного клиента
        {
            if (client.Id == 0) return client;
            Clear();
            Append(client);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, client.FullName);
            client = GetFromList();
            return client;
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