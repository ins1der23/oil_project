using Interfaces;

namespace Models

{
    public class City : BaseElement<City>
    {
        public string Name { get; set; }
        public virtual Districts Districts { get; private set; }
        public virtual Locations Locations { get; private set; }
        public virtual Streets Streets { get; private set; }
        public City()
        {
            Name = string.Empty;
            Districts = new();
            Locations = new();
            Streets = new();
        }
        public void Change(string name)
        {
            if (name != string.Empty) Name = name;
        }
        public override string SearchString() => Name.PrepareToSearch();
        public override City SetEmpty()
        {
            Id = 0;
            Name = string.Empty;
            Districts = new();
            Locations = new();
            Streets = new();
            return this;

        }
        public override City Clone()
        {
            City city = (City)MemberwiseClone();
            city.Districts = Districts;
            city.Locations = Locations;
            city.Streets = Streets;
            return city;
        }

        public override string Summary() => ToString();
        
        public override string ToString() => $"{Name}";

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            City city = (City)obj;
            if (Name.Equals(city.Name)) return true;
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode() + Name.GetHashCode();
        }


    }
}