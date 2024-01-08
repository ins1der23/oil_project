

namespace Models
{
    public class District : BaseElement<District>
    {
        private string name;
        private int cityId;
        private City city;

        internal string Name { get => name; set => name = value; }
        internal int CityId { get => cityId; set => cityId = value; }
        internal virtual City City { get => city; set => city = value; }


        public District() : base()
        {
            name = string.Empty;
            city = new();
            UpdateParameters();
        }

        public override Dictionary<string,object> UpdateParameters()
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
            if (city.Id != 0)
            {
                City = city;
                CityId = city.Id;
            }
            UpdateParameters();
        }

        public override string SearchString() => Name.PrepareToSearch();
        public override string ToString() => $"{Name}, {City.Name}";

        public override District SetEmpty()
        {
            {
                Id = 0;
                Name = string.Empty;
                CityId = 0;
                City = new();
                UpdateParameters();
                return this;
            }
        }

        public override District Clone()
        {
            District item = (District)MemberwiseClone();
            item.City = city;
            item.Parameters = new Dictionary<string, object>(Parameters);
            return item;
        }

        public override string Summary() => ToString();

        public override bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            District toCompare = (District)obj;
            if (Name.Equals(toCompare.Name) && City.Equals(toCompare.City)) return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + City.GetHashCode();
        }

       
    }
}
