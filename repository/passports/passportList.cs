using Connection;
namespace Models
{
    public class PassportList : BaseList
    {
        private new List<Passport> DbList;
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
                        Client = x.ClientId != 0 ? clients.First(cl => cl.Id == x.ClientId) : new(),
                    }).Where(p => p.SearchString().Contains(search)).ToList();
                }
                user.Close();
            }
        }
        // public override async Task GetFromSqlAsync(DBConnection user, string search = "") //, bool searchById = false, object? someObject = null)
        // {
        //     int id = 0;
        //     // if (someObject is Passport)
        //     // {
        //     //     Passport item = (Passport)someObject!;
        //     //     if (searchById) id = item.Id;
        //     //     search = item.SearchString();
        //     // }
        //     search = search.PrepareToSearch();
        //     Registrations registrations = new();
        //     await registrations.GetFromSqlAsync(user);
        //     var regList = registrations.ToWorkingList();
        //     await user.ConnectAsync();
        //     if (user.IsConnect)
        //     {
        //         string sql = @"select * from clients as cl;
        //                        select * from issueds as i;
        //                        select * from passports;";
        //         using (var temp = await user.Connection!.QueryMultipleAsync(sql))
        //         {
        //             var clients = temp.Read<Client>();
        //             var issued = temp.Read<IssuedBy>();
        //             var passports = temp.Read<Passport>();
        //             DbList = passports.Select(x => new Passport
        //             {
        //                 Id = x.Id,
        //                 Number = x.Number,
        //                 IssuedId = x.IssuedId,
        //                 IssuedBy = x.IssuedId != 0 ? issued.First(i => i.Id == x.IssuedId) : new(),
        //                 IssueDate = x.IssueDate,
        //                 RegistrationId = x.RegistrationId,
        //                 Registration = x.RegistrationId != 0 ? regList.First(a => a.Id == x.RegistrationId) : new(),
        //                 // ClientId = x.ClientId,
        //                 Client = x.ClientId != 0 ? clients.First(cl => cl.Id == x.ClientId) : new(),
        //             }).Where(p => p.SearchString().Contains(search)).ToList();
        //             // Where(p => id == 0 ? p.SearchString().Contains(search) : p.Id == id).ToList();
        //         }
        //         user.Close();
        //     }
        // }

        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert passports
                    (id, number, issuedId, issuedDate, registrationId, clientId)
                    values (
                    @{nameof(Passport.Id)},
                    @{nameof(Passport.Number)},
                    @{nameof(Passport.IssuedId)},
                    @{nameof(Passport.IssueDate)},
                    @{nameof(Passport.RegistrationId)},
                    @{nameof(Passport.ClientId)})";
                await user.Connection!.ExecuteAsync(selectQuery, DbList);
                user.Close();
            }
        }


        // public override Task ChangeSqlAsync(DBConnection user)
        // {
        //     throw new NotImplementedException();
        // }

        // public override Task DeleteSqlAsync(DBConnection user)
        // {
        //     throw new NotImplementedException();
        // }



        // public async Task<Passport> SaveGetId(DBConnection user, Passport passport) // получение Id из SQL для нового клиента 
        // {
        //     if (passport.RegistrationId == 0 || passport.Number == 0) return passport;
        //     Clear();
        //     Append(passport);
        //     // await AddSqlAsync(user);
        //     await GetFromSqlAsync(user, passport.SearchString);
        //     passport = (Passport)GetFromList();
        //     return passport;
        // }


    }
}