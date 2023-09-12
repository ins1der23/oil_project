using Connection;
public interface IRepository
{
    Task GetFromSqlAsync(DBConnection user, string search = "");


}