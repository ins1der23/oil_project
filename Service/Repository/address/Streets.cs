using Connection;

namespace Models

{
    public class Streets : BaseRepo<Street>
    {
        public override async Task GetFromSqlAsync(Street? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            int cityId = 0;
            if (item != null)
            {
                cityId = item.CityId;
                if (byId) id = item.Id;
                if (item.SearchString() != string.Empty) search = item.SearchString();
            }
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"select *
                                    from streets as s, cities as c 
                                    where s.cityId=c.Id 
                                    and c.Id = {cityId}
                                    order by s.name";
                var temp = await User.Connection!.QueryAsync<Street, City, Street>(selectQuery, (s, c) =>
                {
                    s.City = c;
                    return s;
                });
                dbList = temp.Where(s => id == 0 ? s.SearchString().Contains(search) : s.Id == id).ToList();
                User.Close();
            }
        }

        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert streets
                    (name, cityId)
                    values (
                    @{nameof(Street.Name)},
                    @{nameof(Street.CityId)})";
                await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }
        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update streets set
                    name = @{nameof(Street.Name)},
                    cityId = @{nameof(Street.CityId)}
                    where Id = @{nameof(Street.Id)};";
                _ = await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }
        public override async Task DeleteSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"delete from streets 
                                        where Id = @{nameof(Street.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, dbList);
                User.Close();
            }
        }
    }
}