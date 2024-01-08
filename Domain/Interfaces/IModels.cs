

using Handbooks;

namespace Interfaces

{
    public interface IModels<T>
    {
        string SearchString();
        T SetEmpty();
        T Clone();
        string Summary();
        string ToString();
        bool Equals(object obj);
        int GetHashCode();
        void Change(Dictionary<string, object> parameters);
        Dictionary<string,object> UpdateParameters();
    }
}