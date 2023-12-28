namespace Models

{
    public class City : IModels, ICloneable
    {
        public int Id { get; set; }
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
        public override string ToString() => $"{Name}";
        public object Clone()
        {
            City city = (City)MemberwiseClone();
            city.Districts = Districts;
            city.Locations = Locations;
            city.Streets = Streets;
            return city;
        }
        public string SearchString() => Name.PrepareToSearch();
    }
}