using Service;

namespace Models
{
    public abstract class RepresentativesRepo : BaseRepo<Human>
    {
        public override async Task GetFromSqlAsync(Human? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != string.Empty) search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            Passports passports = new();
            await passports.GetFromSqlAsync();
            var passportList = passports.GetDbList();
            Clients clients = new();
            await clients.GetFromSqlAsync();
            var clientList = clients.GetDbList();
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string sql = @"select * from humans as h;";

                using (var temp = await User.Connection!.QueryMultipleAsync(sql))
                {
                    var representers = temp.Read<Representative>();
                    DbList = representers.Select(x => (Human)new Representative
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Middlename = x.Middlename,
                        Surname = x.Surname,
                        PassportId = x.PassportId,
                        Passport = x.PassportId != 0 ? passportList.Where(p => p.Id == x.PassportId).First() : new(),
                        ClientId = x.ClientId,
                        Client = x.ClientId != 0 ? clientList.Where(c => c.Id == x.ClientId).First() : new(),
                        Parameters = x.UpdateParameters()
                    }).Where(r => id == 0 ? r.SearchString().Contains(search) : r.Id == id)
                      .Where(r => r.Id != 0).OrderBy(r => r.Surname).ThenBy(r => r.Name).ToList();
                }
                User.Close();
            }
        }

        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert humans 
                    (name, middlename, surname, passportId, ClientId)
                    values (@{nameof(Representative.Name)},
                            @{nameof(Representative.Middlename)},
                            @{nameof(Representative.Surname)},
                            @{nameof(Representative.PassportId)},
                            @{nameof(Representative.ClientId)})";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update locations set
                    name = @{nameof(Representative.Name)},
                    middlename = @{nameof(Representative.Middlename)},
                    surname = @{nameof(Representative.Surname)},
                    passportId = @{nameof(Representative.PassportId)},
                    clientId = @{nameof(Representative.ClientId)}
                    where Id = @{nameof(Representative.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }

        public override async Task DeleteSqlAsync()
        {
           await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"delete from humans 
                                        where Id = @{nameof(Representative.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }


    }
}