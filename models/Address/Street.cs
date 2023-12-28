namespace Models

{
    public class Street : ICloneable, IModels
    {
        private int id;
        private string name;
        private int cityId;
        private City city;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int CityId { get => cityId; set => cityId = value; }
        public virtual City City { get => city; set => city = value; }


        public Street()
        {
            city = new();
            name = string.Empty;
        }
        public void Change(string name)
        {
            if (name != string.Empty) Name = name;
        }
        public string SearchString() => Name.PrepareToSearch();
        public object Clone()
        {
            Street street = (Street)MemberwiseClone();
            street.City = City;
            return street;
        }
        
        //override ToString
        public override string ToString() => $"{City.Name}, {Name}";
        
        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Street street = (Street)obj;
            if (Name.Equals(street.Name) &&
                CityId == street.CityId)
                return true;
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode() + Name.GetHashCode() + CityId.GetHashCode();
        }
    }

}