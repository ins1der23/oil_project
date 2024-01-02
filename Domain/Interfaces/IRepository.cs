using Models;

namespace Interfaces
{
    public interface IRepository<I>
    {
        Task GetFromSqlAsync(I item, string search = "", bool byId = false);
        Task AddSqlAsync();
        Task ChangeSqlAsync();
        Task DeleteSqlAsync();
    }
}
