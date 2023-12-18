using Connection;
namespace Models
{

    public class Passports : IRepository
    {
        List<Passport> PassportList { get; set; }

        public Passports()
        {
            PassportList = new();
        }
        public void Clear() => PassportList.Clear();

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
                    PassportList = passports.Select(x => new Passport
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



        public Task AddSqlAsync(DBConnection user)
        {
            throw new NotImplementedException();
        }

        public List<string> ToStringList()
        {
            throw new NotImplementedException();
        }

        public Task ChangeSqlAsync(DBConnection user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteSqlAsync(DBConnection user)
        {
            throw new NotImplementedException();
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
}
