using Connection;

namespace Models

{
    public class Cities : BaseList<City>
    {
        public override async Task GetFromSqlAsync(DBConnection user, City? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from cities as c 
                                    order by c.Id";
                var temp = await user.Connection!.QueryAsync<City>(selectQuery);
                dbList = temp.Where(c => id == 0 ? c.SearchString().Contains(search) : c.Id == id).ToList();
                user.Close();
            }
        }
        public override async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert cities 
                    (name)
                    values (@{nameof(City.Name)})";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }

        public override async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update cities set
                    name = @{nameof(City.Name)}
                    where Id = @{nameof(City.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }

        public override async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from cities 
                                        where Id = @{nameof(City.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
    }
}
