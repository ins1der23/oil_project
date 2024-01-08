using Interfaces;

namespace Models

{
    public class City : BaseElement<City>
    {
        private string name;
        private Districts districts;
        private Locations locations;
        private Streets streets;

        public string Name { get => name; set => name = value; }
        public virtual Districts Districts { get => districts; set => districts = value; }
        public virtual Locations Locations { get => locations; set => locations = value; }
        public virtual Streets Streets { get => streets; set => streets = value; }
        public City() : base()
        {
            name = string.Empty;
            districts = new();
            locations = new();
            streets = new();
            UpdateParameters();
        }
        public override Dictionary<string,object> UpdateParameters()
        {
            Parameters["Name"] = name;
            return Parameters;
        }
        public override void Change(Dictionary<string, object> parameters)
        {
            string name = parameters["Name"].Wrap<string>();
            if (name != string.Empty) Name = name;
            UpdateParameters();
        }

        public override string SearchString() => Name.PrepareToSearch();
        public override City SetEmpty()
        {
            Id = 0;
            Name = string.Empty;
            Districts = new();
            Locations = new();
            Streets = new();
            UpdateParameters();
            return this;

        }
        public override City Clone()
        {
            City city = (City)MemberwiseClone();
            city.Districts = Districts;
            city.Locations = Locations;
            city.Streets = Streets;
            city.Parameters = new Dictionary<string, object>(Parameters);
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
            City toCompare = (City)obj;
            if (Name.Equals(toCompare.Name)) return true;
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}