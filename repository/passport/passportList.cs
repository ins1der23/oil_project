using Connection;
namespace Models
{
    public class PassportList : BaseList
    {
        new private List<Passport> DbList;
        public PassportList()
        {
            DbList = new();
        }

        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            search = search.PrepareToSearch();
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
                    DbList = passports.Select(x => new Passport
                    {
                        Id = x.Id,
                        Number = x.Number,
                        IssuedId = x.IssuedId,
                        IssuedBy = x.IssuedId != 0 ? issued.First(i => i.Id == x.IssuedId) : new(),
                        IssueDate = x.IssueDate,
                        RegistrationId = x.RegistrationId,
                        Registration = x.RegistrationId != 0 ? regList.First(a => a.Id == x.RegistrationId) : new(),
                        Client = clients.First(cl => cl.Id == x.ClientId),
                    }).Where(p => p.SearchString.Contains(search)).ToList();
                }
                user.Close();
            }
        }



        public async Task<Passport> SaveGetId(DBConnection user, Passport passport) // получение Id из SQL для нового клиента 
        {
            if (passport.RegistrationId == 0 || passport.Number == 0) return passport;
            Clear();
            Append(passport);
            // await AddSqlAsync(user);
            await GetFromSqlAsync(user, passport.SearchString);
            passport = (Passport)GetFromList();
            return passport;
        }


    }
}