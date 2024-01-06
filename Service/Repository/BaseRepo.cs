using Interfaces;

namespace Models
{
    public abstract class BaseRepo<I> : BaseList<I>, IRepository<I> where I : BaseElement<I>
    {

        public BaseRepo() : base() { }



        //Abstract
        public abstract Task GetFromSqlAsync(I? item = null, string search = "", bool byId = false);
        public abstract Task AddSqlAsync();
        public abstract Task ChangeSqlAsync();
        public abstract Task DeleteSqlAsync();

        public async Task SearchAndGet(String searchString) // Поиск и возврат dbList с результатом поиска
        {
            Clear();
            await GetFromSqlAsync(search: searchString);
        }

        public async Task<bool> CheckExist(I item) // Проверка, есть ли уже элемент в базе 
        {
            Clear();
            Append(item);
            await GetFromSqlAsync(item);
            if (IsEmpty) return false;
            if (GetFromList()!.Equals(item)) return true;
            return false;
        }
        public async Task<I> SaveGetId(I item) // получение Id из SQL для нового элемента 
        {
            if (item == null) return item!;
            Clear();
            Append(item);
            await AddSqlAsync();
            await GetFromSqlAsync(item);
            item = GetFromList();
            return item;
        }
        public async Task<I> SaveChanges(I item) // сохранение измениий и возврат измененного элемента
        {
            if (item == null) return item!;
            Clear();
            Append(item);
            await ChangeSqlAsync();
            await GetFromSqlAsync(item, byId: true);
            item = GetFromList();
            return item;
        }
    }
}