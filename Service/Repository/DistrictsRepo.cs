using Connection;

namespace Models
{

    public abstract class DistrictsRepo : BaseRepo<District>
    {

        public override async Task GetFromSqlAsync(District? item = null, string search = "", bool byId = false)
        {
            int id = 0;
            if (search != "") search = search.PrepareToSearch();
            if (item != null)
            {
                if (byId) id = item.Id;
                if (item.SearchString() != string.Empty) search = item.SearchString();
            }
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"select *
                                    from districts as d, cities as c 
                                    where d.cityId=c.Id 
                                    order by d.name";
                var temp = await User.Connection!.QueryAsync<District, City, District>(selectQuery, (d, c) =>
                {
                    d.City = c;
                    d.Parameters["Name"] = d.Name;
                    d.Parameters["City"] = d.City;
                    return d;
                });
                DbList = temp.Where(d => id == 0 ? d.SearchString().Contains(search) : d.Id == id)
                             .Where(d => d.Id != 0).OrderBy(d => d.City.Name).ThenBy(d => d.Name).ToList();
                User.Close();
            }
        }
        public override async Task AddSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"insert districts 
                    (name, cityId)
                    values (@{nameof(District.Name)},
                            @{nameof(District.CityId)})";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }
        public override async Task ChangeSqlAsync()
        {
            await User.ConnectAsync();
            if (User.IsConnect)
            {
                string selectQuery = $@"update districts set
                    name = @{nameof(District.Name)},
                    cityId = @{nameof(District.CityId)}
                    where Id = @{nameof(District.Id)};";
                await User.Connection!.ExecuteAsync(selectQuery, DbList);
                User.Close();
            }
        }

        public override Task DeleteSqlAsync()
        {
            throw new NotImplementedException();
        }

    }
}