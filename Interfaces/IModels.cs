using System.Configuration;

namespace Models
{
    public interface IModels
    {
        string SearchString();
        string ToString();
        bool Equals(object obj);
        
        
        
    }
}