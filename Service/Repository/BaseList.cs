using Connection;
using Controller;


namespace Models
{
    public abstract class BaseList<I> : IEnumerable where I : BaseElement<I>
    {
        private static readonly DBConnection user = Settings.User;
        protected List<I> dbList;
        internal bool IsEmpty => !dbList.Any();

        protected static DBConnection User { get => user; }

        public BaseList()
        {
            dbList = new List<I>();
        }

        // Methods
        public IEnumerator GetEnumerator() => dbList.GetEnumerator();
        public void Clear() => dbList.Clear();
        public void Append(I element) => dbList.Add(element);
        public I GetFromList(int index = 1) => dbList.ElementAt(index - 1);
        public List<I> GetDbList() => dbList.ToList(); // Список для работы с LINQ
        public void ToWriteList(List<I> toAddList)
        {
            dbList.Clear();
            dbList = toAddList.Select(c => c).ToList();
        }

        public abstract void CutOff<P>(P parameter) where P : BaseElement<P>;



        /// <summary>
        /// Формирование списка из ClientList для создания меню 
        /// </summary>
        /// <returns> список из Client.ToString()</returns>
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (I item in dbList)
                output.Add(item!.ToString()!);
            return output;
        }

        //Override
        public override string ToString()
        {
            string output = String.Empty;
            foreach (I item in dbList)
                output += item!.ToString()! + "\n";
            return output;
        }
    }
}
