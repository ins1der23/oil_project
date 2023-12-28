namespace Models

{
    public class Street : ICloneable, IModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }


        public Street()
        {
            City = new();
            Name = string.Empty;
        }
        public void Change(string name)
        {
            if (name != string.Empty) Name = name;
        }
        public override string ToString() => $"{City.Name}, {Name}";

        public object Clone()
        {
            Street street = (Street)MemberwiseClone();
            street.City = City;
            return street;
        }
        public string SearchString() => Name.PrepareToSearch();
    }
}