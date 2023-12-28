using Connection;
namespace Models
{
    public class PassportList : BaseList<Passport>
    {

        public PassportList()
        : base() { }

        public override async Task GetFromSqlAsync(DBConnection user, Passport? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                search = item.SearchString();
                if (byId) id = item.Id;
            }
            Registrations registrations = new();
            await registrations.GetFromSqlAsync(user);
            var regList = registrations.ToWorkingList();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from clients as cl;
                               select * from issueds as i;
                               select * from passports;";
                using (var temp = await user.Connection!.QueryMultipleAsync(sql))
                {
                    var clients = temp.Read<Client>();
                    var issued = temp.Read<IssuedBy>();
                    var passports = temp.Read<Passport>();
                    dbList = passports.Select(x => new Passport
                    {
                        Id = x.Id,
                        Number = x.Number,
                        IssuedId = x.IssuedId,
                        IssuedBy = issued.First(i => i.Id == x.IssuedId),
                        IssueDate = x.IssueDate,
                        RegistrationId = x.RegistrationId,
                        Registration = x.RegistrationId != 0 ? regList.First(a => a.Id == x.RegistrationId) : new(),
                        ClientId = x.ClientId,
                        Client = x.ClientId != 0 ? clients.First(cl => cl.Id == x.ClientId) : new(),
                    }).Where(p => id == 0 ? p.SearchString().Contains(search) : p.Id == id).ToList();

                }
                user.Close();
            }
        }

        public override async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert passports
                    (number, issuedId, issueDate, registrationId, clientId)
                    values (
                    @{nameof(Passport.Number)},
                    @{nameof(Passport.IssuedId)},
                    @{nameof(Passport.IssueDate)},
                    @{nameof(Passport.RegistrationId)},
                    @{nameof(Passport.ClientId)})";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }

        public override async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update passports set
                    number = @{nameof(Passport.Number)},
                    issuedId = @{nameof(Passport.IssuedId)},
                    issueDate = @{nameof(Passport.IssueDate)},
                    registrationId = @{nameof(Passport.RegistrationId)},
                    clientId = @{nameof(Passport.ClientId)}
                    where Id = @{nameof(Passport.Id)};"
                    ;
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();

            }
        }

        public override Task DeleteSqlAsync(DBConnection user)
        {
            throw new NotImplementedException();
        }

    }
}