using System.Reflection.Metadata.Ecma335;
using Interfaces;

namespace Models
{
    public abstract class BaseElement<T> : ICloneable<T>, IModels<T> where T : BaseElement<T>
    {
        private int id;
        private bool chooseEmpty;
        private Dictionary<string, object> parameters;
        public int Id { get => id; set => id = value; }
        public Dictionary<string, object> Parameters { get => parameters; set => parameters = value; }
        public bool ChooseEmpty { get => chooseEmpty; set => chooseEmpty = value; }

        protected BaseElement()
        {
            id = 0;
            parameters = new();
            chooseEmpty = false;
        }

        public Dictionary<string, object> GetEmptyParameters()
        {
            BaseElement<T> street = Clone().SetEmpty();
            return street.Parameters;
        }
        public abstract Dictionary<string, object> UpdateParameters();
        public abstract T SetEmpty();
        public abstract T Clone();
        public abstract string Summary();
        public abstract override string ToString();
        public abstract string SearchString();
        public abstract void Change(Dictionary<string, object> parameters);
        //    override object.Equals
        public override abstract bool Equals(object? obj);
        //    override object.GetHashCode
        public override abstract int GetHashCode();
    }
}

