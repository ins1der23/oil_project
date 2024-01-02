using Interfaces;

namespace Models

{
    public class City : BaseElement<City>
    {
        private string name;
        private Districts districts;
        private Locations locations;
        private Streets<Street> streets;

        internal string Name { get => name; set => name = value; }
        internal virtual Districts Districts { get => districts; set => districts = value; }
        internal virtual Locations Locations { get => locations; set => locations = value; }
        internal virtual Streets<Street> Streets { get => streets; set => streets = value; }
        public City()
        {
            name = string.Empty;
            districts = new();
            locations = new();
            streets = new();
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
        public override int GetHashCode() => Id.GetHashCode() + Name.GetHashCode();
        
    }
}