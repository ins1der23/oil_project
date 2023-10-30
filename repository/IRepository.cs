using Connection;
namespace Models
{
    public interface IRepository
    {
        void Clear();
        Task GetFromSqlAsync(DBConnection user, string search = "", int id = 0);
        Task AddSqlAsync(DBConnection user);
        Task ChangeSqlAsync(DBConnection user);
        Task DeleteSqlAsync(DBConnection user);
        List<string> ToStringList()
        {
            List<string> output = new();
            return output;
        }

    }
}
