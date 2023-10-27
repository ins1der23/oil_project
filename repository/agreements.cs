using Connection;

namespace Models
{
    public class Agreements
    {
        List<Agreement> AgreementList { get; set; }

        public Agreements()
        {
            AgreementList = new();
        }


        public void Clear() => AgreementList.Clear();
        public void Append(Agreement agreement) => AgreementList.Add(agreement);
        public Agreement GetFromList(int index = 1) => AgreementList[index - 1];


        /// <summary>
        /// Формирование списка из AgreementList для создания меню 
        /// </summary>
        /// <returns> список из Agreement.ToString()</returns>
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (var item in AgreementList)
                output.Add(item.ToString());
            return output;
        }
        public async Task GetFromSqlAsync(DBConnection user, string search = "", int id = 0)
        {
            search = search.PrepareToSearch();
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string sql = @"select * from clients as c;
                            select * from passports as p;
                            select * from agreements as a";

                using (var temp = await user.Connection!.QueryMultipleAsync(sql))
                {
                    var clients = temp.Read<Client>();
                    var passports = temp.Read<Passport>();
                    var agreements = temp.Read<Agreement>();
                    AgreementList = agreements.Select(x => new Agreement
                    {
                        Id = x.Id,
                        Date = x.Date,
                        Name = x.Name,
                        ScanPath = x.ScanPath,
                        ClientId = x.ClientId,
                        Client = clients.Where(c => c.Id == x.ClientId).First(),
                        PassportId = x.PassportId,
                        Passport = x.PassportId != 0 ? passports.Where(p => p.Id == x.PassportId).First() : new(),

                    }).Where(a => id == 0 ? a.SearchString.PrepareToSearch().Contains(search) : a.Id == id).ToList();
                }
                user.Close();
            }
        }
        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert agreements
                    (date, name, scanPath, clientId)
                    values (
                    @{nameof(Agreement.Date)},
                    @{nameof(Agreement.Name)},
                    @{nameof(Agreement.ScanPath)},
                    @{nameof(Agreement.ClientId)})";
                await user.Connection!.ExecuteAsync(selectQuery, AgreementList);
                user.Close();
            }
        }
        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update agreements set
                    date = @{nameof(Agreement.Date)},
                    name = @{nameof(Agreement.Name)},
                    scanPath = @{nameof(Agreement.ScanPath)},
                    clientId = @{nameof(Agreement.ClientId)}
                    where Id = @{nameof(Agreement.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, AgreementList);
                user.Close();
            }
        }
        public async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from agreements 
                                        where Id = @{nameof(Agreement.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, AgreementList);
                user.Close();
            }
        }
        /// <summary>
        /// сохраненение нового agreement в SQL и получение agreement с присвоенным Id из SQL
        /// </summary>
        /// <param name="user">экземпляр DBConnection для связи с базой</param>
        /// <param name="agreement">экземпляр agreement != null, которому нужно присвоить Id</param>
        /// <returns>agreement с присвоенным Id</returns>
        public async Task<Agreement> SaveGetId(DBConnection user, Agreement agreement) // 
        {
            if (agreement.ClientId == 0) return agreement;
            Clear();
            Append(agreement);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, agreement.SearchString);
            agreement = GetFromList();
            return agreement;
        }
        /// <summary>
        /// сохраненение в SQL и возврат измененного agreement
        /// </summary>
        /// <param name="user">экземпляр DBConnection для связи с базой</param>
        /// <param name="agreement">измененный экземпляр agreement != null</param>
        /// <returns></returns>
        public async Task<Agreement> SaveChanges(DBConnection user, Agreement agreement) // 
        {
            if (agreement.ClientId == 0) return agreement;
            Clear();
            Append(agreement);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, id: agreement.Id);
            agreement = GetFromList();
            return agreement;
        }

    }


}