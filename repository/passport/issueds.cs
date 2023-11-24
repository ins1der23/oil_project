using Connection;
namespace Models
{

    public class Issueds : IRepository
    {
        List<IssuedBy> IssuedList { get; set; }

        public Issueds()
        {
            IssuedList = new();
        }

        public void Clear() => IssuedList.Clear();
        public void Append(IssuedBy issuedBy) => IssuedList.Add(issuedBy);
        public IssuedBy GetFromList(int index = 1) => IssuedList[index - 1];

        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select * from issueds where name like ""%{search}%""";
                var temp = await user.Connection!.QueryAsync<IssuedBy>(selectQuery);
                IssuedList = temp.ToList();
                user.Close();
            }
        }
        public async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert issueds
                    (name)
                    values (
                    @{nameof(IssuedBy.Name)})";
                await user.Connection!.ExecuteAsync(selectQuery, IssuedList);
                user.Close();
            }
        }
        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update issueds set
                    name = @{nameof(IssuedBy.Name)}
                    where Id = @{nameof(IssuedBy.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, IssuedList);
                user.Close();
            }
        }
        public async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from issueds 
                                        where Id = @{nameof(IssuedBy.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, IssuedList);
                user.Close();
            }
        }
        public async Task<IssuedBy> SaveGetId(DBConnection user, IssuedBy issuedBy) // получение Id из SQL для нового клиента 
        {
            if (issuedBy.Name == string.Empty) return issuedBy;
            Clear();
            Append(issuedBy);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, issuedBy.Name);
            issuedBy = GetFromList();
            return issuedBy;
        }

        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (var item in IssuedList)
                output.Add(item.ToString());
            return output;
        }

        
    }
}