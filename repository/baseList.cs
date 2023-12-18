using Connection;
namespace Models
{
    public class BaseList : IEnumerable
    {
        protected List<object> DbList { get; set; }
        public bool IsEmpty => !DbList.Any();

        public BaseList()
        {
            DbList = new List<object>();
        }

        // public abstract Task GetFromSqlAsync(DBConnection user, string search = "" ); //, bool byId = false, object? someObject = null);
        // public abstract Task AddSqlAsync(DBConnection user);
        // public abstract Task ChangeSqlAsync(DBConnection user);
        // public abstract Task DeleteSqlAsync(DBConnection user);


        public IEnumerator GetEnumerator() => DbList.GetEnumerator();

        public void Clear() => DbList.Clear();
        public void Append(Object obj) => DbList.Add(obj);
        public object GetFromList() => DbList.SingleOrDefault()!;
        public List<object> ToWorkingList() => DbList.ToList();
        public void ToWriteList(List<object> toAddList)
        {
            DbList.Clear();
            DbList = toAddList.Select(c => c).ToList();
        }

        // public async Task<object> SaveGetId(DBConnection user, object someObject) // получение Id из SQL для нового клиента 
        // {

        //     if (someObject == null) return someObject!;
        //     Clear();
        //     Append(someObject);
        //     await AddSqlAsync(user);
        //     await GetFromSqlAsync(user, byId: false, someObject: someObject);
        //     someObject = GetFromList();
        //     return someObject;
        // }


        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (var item in DbList)
                output.Add(item.ToString()!);
            return output;
        }


        public override string ToString()
        {
            string output = String.Empty;
            foreach (var item in DbList)
                output += item.ToString() + "\n";
            return output;
        }
    }
}
