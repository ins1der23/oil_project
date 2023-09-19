using Connection;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;
using static InOut;

namespace Models
{
    public class Agreements : IRepository
    {
        List<Agreement> AgreementList { get; set; }

        public Agreements()
        {
            AgreementList = new();
        }


        public void Clear() => AgreementList.Clear();
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

        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from agreements as agr, clients as cl 
                                    where agr.clientId=cl.Id 
                                    and (agr.name like ""%{search}%"")
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
        
    }
}