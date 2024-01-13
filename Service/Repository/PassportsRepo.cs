using Service;
using UserInterface;

namespace Models
{
    public abstract class PassportsRepo : BaseRepo<Passport>
    {
        public override async Task GetFromSqlAsync(Passport? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            Registrations registrations = new();
            await registrations.GetFromSqlAsync();
            var regList = registrations.GetDbList();
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string sql = @"select * from clients as cl;
                               select * from issueds as i;
                               select * from passports;";
                using (var temp = await User.Connection!.QueryMultipleAsync(sql))
                {
                    var clients = temp.Read<Client>();
                    var issued = temp.Read<IssuedBy>();
                    var passports = temp.Read<Passport>();
                    DbList = passports.Select(x => new Passport
                    {
                        Id = x.Id,
                        Number = x.Number,
                        IssuedId = x.IssuedId,
                        IssuedBy = x.IssuedId != 0 ? issued.First(i => i.Id == x.IssuedId) : new(),
                        IssueDate = x.IssueDate,
                        RegistrationId = x.RegistrationId,
                        Registration = x.RegistrationId != 0 ? (Registration)regList.First(a => a.Id == x.RegistrationId) : new Registration(),
                    }).Where(p => id == 0 ? p.SearchString().Contains(search) : p.Id == id).ToList();
                }
                User.Close();
            }
        }
        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert passports
                    (number, issuedId, issueDate, registrationId)
                    values (
                    @{nameof(Passport.Number)},
                    @{nameof(Passport.IssuedId)},
                    @{nameof(Passport.IssueDate)},
                    @{nameof(Passport.RegistrationId)})";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }

        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update passports set
                    number = @{nameof(Passport.Number)},
                    issuedId = @{nameof(Passport.IssuedId)},
                    issueDate = @{nameof(Passport.IssueDate)},
                    registrationId = @{nameof(Passport.RegistrationId)}                    
                    where Id = @{nameof(Passport.Id)};"
                    ;
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }

        public override async Task DeleteSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"delete from passports 
                                        where Id = @{nameof(Passport.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }

    }
}
