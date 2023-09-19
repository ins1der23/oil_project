using Connection;
public interface IRepository
{
    void Clear();
    Task GetFromSqlAsync(DBConnection user, string search = "");
    Task AddSqlAsync(DBConnection user);
    Task DeleteSqlAsync(DBConnection user);
    List<string> ToStringList();

}