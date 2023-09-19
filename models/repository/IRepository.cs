using Connection;
public interface IRepository
{
    void Clear();
    List<string> ToStringList();
    Task GetFromSqlAsync(DBConnection user, string search = "");
    Task AddSqlAsync(DBConnection user);
    Task DeleteSqlAsync(DBConnection user);

}