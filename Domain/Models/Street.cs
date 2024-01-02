using Interfaces;

namespace Models

{
    public class Street : BaseElement<Street>
    {

        private string name;
        private int cityId;
        private City city;

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
        public override string SearchString() => Name.PrepareToSearch();

        public override Street SetEmpty()
        {
            City = new();
            Name = string.Empty;
            return this;
        }

        public override string Summary() => ToString();

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
        public override int GetHashCode() => Id.GetHashCode() + Name.GetHashCode() + CityId.GetHashCode();

        // override Clone
        public override Street Clone()
        {
            Street street = (Street)MemberwiseClone();
            street.City = City;
            return street;
        }
    }
}