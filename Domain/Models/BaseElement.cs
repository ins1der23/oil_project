using System.Reflection.Metadata.Ecma335;
using Interfaces;

namespace Models
{
    public abstract class BaseElement<T> : ICloneable<T>, IModels<T> where T : BaseElement<T>
    {
        private int id;
        private Dictionary<string, object> parameters;
        internal int Id { get => id; set => id = value; }
        internal Dictionary<string, object> Parameters { get => parameters; set => parameters = value; }

        public BaseElement()
        {
            id = 0;
            parameters = new();
        }
        public abstract void UpdateParameters();
        public abstract T SetEmpty();
        public abstract T Clone();
        public abstract string Summary();
        public abstract override string ToString();
        public abstract string SearchString();
        public abstract void Change(Dictionary<string, object> parameters);

        public bool Equals(T toCompare)
        {
            if (toCompare == null || GetType() != toCompare.GetType())
            {
                return false;
            }
            foreach (var item in Parameters)
            {
                string key = item.Key;
                if (!Parameters[key].Equals(toCompare.parameters[key])) return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            int output = 0;
            foreach (var item in Parameters)
            {
                output += item.Value.GetHashCode();
            }
            return output;
        }


    }
}
