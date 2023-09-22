using Connection;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;
using static InOut;

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
            List<string> output = new List<string>();
            foreach (var item in AgreementList)
                output.Add(item.ToString());
            return output;
        }

        public async Task GetFromSqlAsync(DBConnection user, string searchString = "", int id = 0)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = id == 0 ? $@"select *
                                    from agreements as agr, clients as cl 
                                    where agr.clientId=cl.Id 
                                    {searchString}
                                    order by agr.name" : $@"select *
                                    from agreements as agr, clients as cl 
                                    where agr.clientId=cl.Id 
                                    and agr.id = {id}
                                    order by agr.name";

                var temp = await user.Connection.QueryAsync<Agreement, Client, Agreement>(selectQuery, (agr, cl) =>
                {
                    agr.Client = cl;
                    return agr;
                });
                AgreementList = temp.ToList();
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
                await user.Connection.ExecuteAsync(selectQuery, AgreementList);
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
                await user.Connection.ExecuteAsync(selectQuery, AgreementList);
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
                await user.Connection.ExecuteAsync(selectQuery, AgreementList);
                user.Close();
            }
        }
        /// <summary>
        /// сохраненение нового agreement в SQL и получение agreement с присвоенным Id из SQL
        /// </summary>
        /// <param name="user">экземпляр DBConnection для связи с базой</param>
        /// <param name="agreement">экземпляр agreement != null, которому нужно присвоить Id</param>
        /// <returns>agreement с присвоенным Id</returns>
        public async Task<Agreement> SaveNewGetId(DBConnection user, Agreement agreement) // 
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
            await GetFromSqlAsync(user, id : agreement.Id);
            agreement = GetFromList();
            return agreement;
        }

    }


}