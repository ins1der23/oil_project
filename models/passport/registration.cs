using Connection;

namespace Models
{
    public class Registration : Address
    {
        public string FlatNum { get; set; }
        public new string LongString => $"{City.Name,-15}, {Street.Name,-28}, {HouseNum,-12}-{FlatNum,-10}";

        public Registration()
        {
            City = new();
            Street = new();
            HouseNum = string.Empty;
            FlatNum = string.Empty;
        }
        public override string ToString() => $"{LongString}";
    }
}