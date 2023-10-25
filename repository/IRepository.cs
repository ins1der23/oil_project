using Connection;
namespace Models
{
    public interface IRepository
    {
        void Clear();
        Task GetFromSqlAsync(DBConnection user, string search = "");
        Task AddSqlAsync(DBConnection user);
        List<string> ToStringList()
        {
            List<string> output = new();
            return output;
        }

    }
}
