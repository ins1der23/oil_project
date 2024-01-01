using System.Reflection.Metadata.Ecma335;
using Interfaces;

namespace Models
{
    public abstract class BaseElement<T> : ICloneable<T>, IModels
    {
        private int id;
        public int Id { get => id; set => id = value; }
        public BaseElement()
        {
            id = 0;
        }

        public abstract T SetEmpty();
        public abstract T Clone();
        public abstract string Summary();
        public abstract override string ToString();
        public abstract string SearchString();
        public abstract override bool Equals(object? obj);
        public abstract override int GetHashCode();

    }
}
