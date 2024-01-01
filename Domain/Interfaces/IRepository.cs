namespace Interfaces
{
    public interface IRepository<E>
    {
        Task GetFromSqlAsync(E item, string search = "", bool byId = false);
        Task AddSqlAsync();
        Task ChangeSqlAsync();
        Task DeleteSqlAsync();
    }
}
