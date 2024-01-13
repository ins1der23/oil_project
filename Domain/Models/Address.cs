

using MenusAndChoices;

namespace Models
{
    public class Address : BaseAddress
    {
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public Address() : base()
        {
            RoleId = 1;
            District = new();
            Location = new();
            UpdateParameters();
        }

        public override Dictionary<string, object> UpdateParameters()
        {
            Parameters["City"] = City;
            Parameters["District"] = City;
            Parameters["Location"] = City;
            Parameters["Street"] = Street;
            Parameters["HouseNum"] = HouseNum;
            return Parameters;
        }

        public override void Change(Dictionary<string, object> parameters)
        {

            City city = parameters["City"].Wrap<City>();
            District district = parameters["District"].Wrap<District>();
            Location location = parameters["Location"].Wrap<Location>();
            Street street = parameters["Street"].Wrap<Street>();
            string houseNum = parameters["HouseNum"].Wrap<string>();

            if (city.ChooseEmpty == true || city.Id != 0)
            {
                City = city;
                CityId = city.Id;
            }
            if (district.ChooseEmpty == true || district.Id != 0)
            {
                District = district;
                DistrictId = district.Id;
            }
            if (location.ChooseEmpty == true || location.Id != 0)
            {
                Location = location;
                LocationId = location.Id;
            }
            if (street.ChooseEmpty == true || street.Id != 0)
            {
                Street = street;
                StreetId = street.Id;
            }
            if (houseNum != string.Empty) HouseNum = houseNum;
            UpdateParameters();
        }

        public override Address Clone()
        {
            Address address = (Address)MemberwiseClone();
            address.City = City;
            address.District = District;
            address.Location = Location;
            address.Street = Street;
            address.Parameters = new Dictionary<string, object>(Parameters);
            return address;
        }
        public override string LongString()
        {
            if (CityId == 1) return $"{City.Name,-15}{Location.Name,-17}{Street.Name,-28}{HouseNum,-12}";
            else return $"{City.Name,-15}{Street.Name,-28}{HouseNum,-12}";
        }
        public override string ShortString()
        {

            if (CityId == 1) return $"{Street.Name}, {HouseNum}";
            else return $"{City.Name}, {Street.Name}, {HouseNum}";

        }
        public override string SearchString() => $"{City.Name}{Street.Name}{HouseNum}".PrepareToSearch();
        public override string ToString() => $"{LongString}";

        public override Address SetEmpty()
        {
            Id = 0;
            CityId = 0;
            City = new();
            LocationId = 0;
            Location = new();
            DistrictId = 0;
            District = new();
            StreetId = 0;
            Street = new();
            HouseNum = string.Empty;
            UpdateParameters();
            return this;
        }

        public override string Summary() => AddressText.Summary(this);


        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Address toCompare = (Address)obj;
            if (City.Equals(toCompare.City) &&
                District.Equals(toCompare.District) &&
                Location.Equals(toCompare.Location) &&
                Street.Equals(toCompare.Street) &&
                HouseNum.Equals(toCompare.HouseNum))
                return true;
            return false;
        }

        public override int GetHashCode() => City.GetHashCode() +
                                             District.GetHashCode() +
                                             Location.GetHashCode() +
                                             Street.GetHashCode() +
                                             HouseNum.GetHashCode();




        public void Change(City city, District district, Location location, Street street, string houseNum)
        {
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
            if (location.Id != 0)
            {
                Location = location;
                LocationId = location.Id;
            }
            if (street.Id != 0)
            {
                Street = street;
                StreetId = street.Id;
            }
            if (houseNum != string.Empty) HouseNum = houseNum;
        }

    }
}