using Connection;

namespace Models

{
    public class Cities : BaseRepo<City>
    {
        public override async Task GetFromSqlAsync(City? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"select *
                                    from cities as c 
                                    order by c.Id";
                var temp = await User.Connection!.QueryAsync<City>(selectQuery);
                dbList = temp.Where(c => id == 0 ? c.SearchString().Contains(search) : c.Id == id).ToList();
                User.Close();
            }
        }
        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert cities 
                    (name)
                    values (@{nameof(City.Name)})";
                await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }

        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update cities set
                    name = @{nameof(City.Name)}
                    where Id = @{nameof(City.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }

        public override async Task DeleteSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"delete from cities 
                                        where Id = @{nameof(City.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }
    }
}