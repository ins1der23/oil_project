using System.Reflection.Metadata;
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


        public Street() : base()
        {
            name = string.Empty;
            city = new();
            UpdateParameters();
        }
        public override Dictionary<string, object> UpdateParameters()
        {
            Parameters["Name"] = name;
            Parameters["City"] = city;
            return Parameters;
        }
        public override void Change(Dictionary<string, object> parameters)
        {
            string name = parameters["Name"].Wrap<string>();
            City city = parameters["City"].Wrap<City>();
            if (name != string.Empty) Name = name;
            if (city.ChooseEmpty == true || city.Id != 0)
            {
                City = city;
                CityId = city.Id;
            }
            UpdateParameters();
        }
        public override string SearchString() => Name.PrepareToSearch();

        public override Street SetEmpty()
        {
            Id = 0;
            Name = string.Empty;
            CityId = 0;
            City = new();
            UpdateParameters();
            return this;
        }
        public override string Summary() => ToString();

        //override ToString
        public override string ToString() => $"{City.Name}, {Name}";


        // override Clone
        public override Street Clone()
        {
            Street street = (Street)MemberwiseClone();
            street.City = city;
            street.Parameters = new Dictionary<string, object>(Parameters);
            return street;
        }
        public override bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Street toCompare = (Street)obj;
            if (Name.Equals(toCompare.Name) && City.Equals(toCompare.City)) return true;
            return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode() + City.GetHashCode();
        }
    }
}