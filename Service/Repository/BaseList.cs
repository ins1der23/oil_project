using Connection;
using Controller;
using Interfaces;

namespace Models
{
    public abstract class BaseList<E> : IEnumerable where E : BaseElement<E>
    {
        private static readonly DBConnection user = Settings.User;
        protected List<E> dbList;
        internal bool IsEmpty => !dbList.Any();

        protected static DBConnection User { get => user; }

        public BaseList()
        {
            dbList = new List<E>();
        }
                
        // Methods
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
        

        /// <summary>
        /// Формирование списка из ClientList для создания меню 
        /// </summary>
        /// <returns> список из Client.ToString()</returns>
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (E item in dbList)
                output.Add(item!.ToString()!);
            return output;
        }

        //Override
        public override string ToString()
        {
            string output = String.Empty;
            foreach (E item in dbList)
                output += item!.ToString()! + "\n";
            return output;
        }
    }
}
