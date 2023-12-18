using System.Linq;
using System.Linq.Expressions;
using Connection;
using MySqlX.XDevAPI;
namespace Models
{
    public class BaseList : IQueryable
    {


        protected List<object> DbList { get; set; }
        public bool IsEmpty => !DbList.Any();

        protected BaseList()
        {
            DbList = new List<object>();
        }
        public IEnumerator GetEnumerator() => DbList.GetEnumerator();
        public Type ElementType => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();


        public void Clear() => DbList.Clear();
        public void Append(Object obj) => DbList.Add(obj);
        public object GetFromList() => DbList.SingleOrDefault()!;
        public List<object> ToWorkingList() => DbList.ToList();
        public void ToWriteList(List<object> toAddList)
        {
            DbList.Clear();
            DbList = toAddList.Select(c => c).ToList();
        }

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
