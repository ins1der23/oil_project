

namespace Models
{
    public class Address : BaseElement<Address>
    {
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int DistrictId { get; set; }
        public virtual District District { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public int StreetId { get; set; }
        public virtual Street Street { get; set; }
        public string HouseNum { get; set; }
        public string LongString
        {
            get
            {
                if (CityId == 1) return $"{City.Name,-15}{Location.Name,-17}{Street.Name,-28}{HouseNum,-12}";
                else return $"{City.Name,-15}{Street.Name,-28}{HouseNum,-12}";
            }

        }
        public string ShortString
        {
            get
            {
                if (CityId == 1) return $"{Street.Name}, {HouseNum}";
                else return $"{City.Name}, {Street.Name}, {HouseNum}";
            }
        }

        public Address()
        {
            City = new();
            District = new();
            Location = new();
            Street = new();
            HouseNum = string.Empty;
        }

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

        public override string SearchString() => $"{City.Name}{Street.Name}{HouseNum}".PrepareToSearch();
        public override Address Clone()
        {
            Address address = (Address)MemberwiseClone();
            address.City = City;
            address.District = District;
            address.Location = Location;
            address.Street = Street;
            return address;
        }

        public override string ToString() => $"{LongString}";

        public override Address SetEmpty()
        {
            throw new NotImplementedException();
        }

        public override string Summary()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override void Change(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override void UpdateParameters()
        {
            throw new NotImplementedException();
        }
    }
}