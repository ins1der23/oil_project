using Connection;
namespace Models
{
    public abstract class BaseList<E> : IEnumerable, IRepository<E>
    {

        protected List<E> dbList;
        internal bool IsEmpty => !dbList.Any();

        public BaseList()
        {
            dbList = new List<E>();
        }

        public abstract Task GetFromSqlAsync(DBConnection user, E item, string search = "", bool byId = false);
        public abstract Task AddSqlAsync(DBConnection user);
        public abstract Task ChangeSqlAsync(DBConnection user);
        public abstract Task DeleteSqlAsync(DBConnection user);
        
        public IEnumerator GetEnumerator() => dbList.GetEnumerator();
        public void Clear() => dbList.Clear();
        public void Append(E element) => dbList.Add(element);
        public E GetFromList(int index = 1) => dbList.ElementAt(index - 1);
        public List<E> ToWorkingList() => dbList.ToList(); // Список для работы с LINQ
        public void ToWriteList(List<E> toAddList)
        {
            dbList.Clear();
            dbList = toAddList.Select(c => c).ToList();
        }
        public async Task<bool> CheckExist(DBConnection user, E item) // Проверка, есть ли уже элемент в базе 
        {
            Clear();
            Append(item);
            await GetFromSqlAsync(user, item);
            if (IsEmpty) return false;
            if (GetFromList()!.Equals(item)) return true;
            return false;
        }
        public async Task<E> SaveGetId(DBConnection user, E item) // получение Id из SQL для нового элемента 
        {
            if (item == null) return item!;
            Clear();
            Append(item);
            await AddSqlAsync(user);
            await GetFromSqlAsync(user, item);
            item = GetFromList();
            return item;
        }
        public async Task<E> SaveChanges(DBConnection user, E item) // сохранение измениий и возврат измененного элемента
        {
            if (item == null) return item;
            Clear();
            Append(item);
            await ChangeSqlAsync(user);
            await GetFromSqlAsync(user, item, byId: true);
            item = GetFromList();
            return item;
        }
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (E item in dbList)
                output.Add(item!.ToString()!);
            return output;
        }
        public override string ToString()
        {
            string output = String.Empty;
            foreach (E item in dbList)
                output += item!.ToString()! + "\n";
            return output;
        }
    }
}
