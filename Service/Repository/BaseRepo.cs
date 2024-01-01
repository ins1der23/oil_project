using Interfaces;

namespace Models
{
    public abstract class BaseRepo<E> : BaseList<E>, IRepository<E> where E : BaseElement<E>
    {

        public BaseRepo() : base() { }



        //Abstract
        public abstract Task GetFromSqlAsync(E? item = null, string search = "", bool byId = false);
        public abstract Task AddSqlAsync();
        public abstract Task ChangeSqlAsync();
        public abstract Task DeleteSqlAsync();

        public async Task SearchAndGet(String searchString) // Поиск и возврат dbList с результатом поиска
        {
            Clear();
            await GetFromSqlAsync(search: searchString);
        }

        public async Task<bool> CheckExist(E item) // Проверка, есть ли уже элемент в базе 
        {
            Clear();
            Append(item);
            await GetFromSqlAsync(item);
            if (IsEmpty) return false;
            if (GetFromList()!.Equals(item)) return true;
            return false;
        }
        public async Task<E> SaveGetId(E item) // получение Id из SQL для нового элемента 
        {
            if (item == null) return item!;
            Clear();
            Append(item);
            await AddSqlAsync();
            await GetFromSqlAsync(item);
            item = GetFromList();
            return item;
        }
        public async Task<E> SaveChanges(E item) // сохранение измениий и возврат измененного элемента
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