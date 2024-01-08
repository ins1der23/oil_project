using Connection;
using Controller;


namespace Models
{
    public abstract class BaseList<I> : IEnumerable where I : BaseElement<I>
    {
        private static readonly DBConnection user = Settings.User;
        private List<I> dbList;
        internal bool IsEmpty => !DbList.Any();

        protected static DBConnection User { get => user; }
        public List<I> DbList { get => dbList; set => dbList = value; }

        public BaseList()
        {
            dbList = new List<I>();
        }

        // Methods
        public IEnumerator GetEnumerator() => DbList.GetEnumerator();
        public void Clear() => DbList.Clear();
        public void Append(I element) => DbList.Add(element);
        public I GetFromList(int index = 1)
        {
            try
            {
                return DbList.ElementAt(index - 1);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<I> GetDbList() => DbList.ToList(); // Список для работы с LINQ
        public void ToWriteList(List<I> toAddList)
        {
            DbList.Clear();
            DbList = toAddList.Select(c => c).ToList();
        }

        public abstract void CutOff(object parameter);



        /// <summary>
        /// Формирование списка из ClientList для создания меню 
        /// </summary>
        /// <returns> список из Client.ToString()</returns>
        public List<string> ToStringList()
        {
            List<string> output = new();
            foreach (I item in DbList)
                output.Add(item!.ToString()!);
            return output;
        }

        //Override
        public override string ToString()
        {
            string output = String.Empty;
            foreach (I item in DbList)
                output += item!.ToString()! + "\n";
            return output;
        }
    }
}
