using Connection;
namespace Models
{
    public interface IRepository<E>
    {
        Task GetFromSqlAsync(DBConnection user, E item, string search = "", bool byId = false);
        Task AddSqlAsync(DBConnection user);
        Task ChangeSqlAsync(DBConnection user);
        Task DeleteSqlAsync(DBConnection user);


    }
}
