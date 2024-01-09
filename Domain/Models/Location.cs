using Connection;

namespace Models
{
    public class Location : BaseElement<Location>
    {
        private string name;
        private int cityId;
        private City city;
        private int districtId;
        private District district;

        public string Name { get => name; set => name = value; }
        public int CityId { get => cityId; set => cityId = value; }
        public virtual City City { get => city; set => city = value; }
        public int DistrictId { get => districtId; set => districtId = value; }
        public virtual District District { get => district; set => district = value; }

        public Location() : base()
        {
            name = string.Empty;
            city = new();
            district = new();
            UpdateParameters();
        }

        public override Dictionary<string, object> UpdateParameters()
        {
            Parameters["Name"] = name;
            Parameters["City"] = city;
            Parameters["District"] = district;
            return Parameters;
        }

        public override void Change(Dictionary<string, object> parameters)
        {
            string name = parameters["Name"].Wrap<string>();
            City city = parameters["City"].Wrap<City>();
            District district = parameters["District"].Wrap<District>();
            if (name != string.Empty) Name = name;
            if (city.Id != 0)
            {
                City = city;
                CityId = city.Id;
            }
            if (district.Id != 0)
            {
                District = district;
                DistrictId = district.Id;
            }
            UpdateParameters();
        }
        public override Location SetEmpty()
        {
            Id = 0;
            Name = string.Empty;
            cityId = 0;
            City = new();
            districtId = 0;
            District = new();
            UpdateParameters();
            return this;
        }
        public override string SearchString() => Name.PrepareToSearch();
        public override string Summary() => ToString();
        public override string ToString()
        {
            if (City.Id == 1)
                return $"{City.Name}, {District.Name}, {Name}";
            else
                return $"{Name}";
        }
        public override Location Clone()
        {
            Location item = (Location)MemberwiseClone();
            item.District = district;
            item.City = city;
            item.Parameters = new Dictionary<string, object>(Parameters);
            return item;
        }
        public override bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Location toCompare = (Location)obj;
            if (Name.Equals(toCompare.Name) &&
                City.Equals(toCompare.City) &&
                District.Equals(toCompare.District)) return true;
            return false;
        }
        public override int GetHashCode() => Name.GetHashCode() + City.GetHashCode() + District.GetHashCode();


    }
}


