using Connection;

namespace Models

{
    public class Streets : BaseList<Street>
    {
        public override async Task GetFromSqlAsync(DBConnection user, Street? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            int cityId = 0;
            if (item != null)
            {
                cityId = item.CityId;
                if (byId) id = item.Id;
                else search = item.SearchString();
            }
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select *
                                    from streets as s, cities as c 
                                    where s.cityId=c.Id 
                                    and c.Id = {cityId}
                                    order by s.name";
                var temp = await user.Connection!.QueryAsync<Street, City, Street>(selectQuery, (s, c) =>
                {
                    s.City = c;
                    return s;
                });
                dbList = temp.Where(s => id == 0 ? s.SearchString().Contains(search) : s.Id == id).ToList();
                user.Close();
            }
        }

        public override async Task AddSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert streets
                    (name, cityId)
                    values (
                    @{nameof(Street.Name)},
                    @{nameof(Street.CityId)})";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
        public override async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update streets set
                    name = @{nameof(Street.Name)},
                    cityId = @{nameof(Street.CityId)}
                    where Id = @{nameof(Street.Id)};";
                _ = await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
        public override async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from streets 
                                        where Id = @{nameof(Street.Id)};";
                await user.Connection!.ExecuteAsync(selectQuery, dbList);
                user.Close();
            }
        }
    }
}