using Interfaces;

namespace Models
{
    public abstract class IssuedsRepo : BaseRepo<IssuedBy>
    {
        public override async Task GetFromSqlAsync(IssuedBy? item = null, string search = "", bool byId = false)
        {

            int id = 0;
            if (search != string.Empty) search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"select * from issueds";
                var temp = await User.Connection!.QueryAsync<IssuedBy>(selectQuery);
                DbList = temp.Where(i => id == 0 ? i.SearchString().Contains(search) : i.Id == id)
                .Where(i => i.Id != 0).ToList();
                User.Close();
            }
        }
        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert issueds
                    (name)
                    values (
                    @{nameof(IssuedBy.Name)})";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update issueds set
                    name = @{nameof(IssuedBy.Name)}
                    where Id = @{nameof(IssuedBy.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
        public override async Task DeleteSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"delete from issueds 
                                        where Id = @{nameof(IssuedBy.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
    }
}