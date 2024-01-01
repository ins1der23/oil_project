namespace Models
{
    public class Registration : BaseElement<Registration>
    {
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int StreetId { get; set; }
        public virtual Street Street { get; set; }
        public string HouseNum { get; set; }
        public string FlatNum { get; set; }
        public string LongString => $"{City.Name,-19}{Street.Name,-28}{HouseNum,-12}{FlatNum,-10}";


        public Registration()
        {
            City = new();
            Street = new();
            HouseNum = string.Empty;
            FlatNum = string.Empty;
        }

        public void Change(City city, Street street, string houseNum, string flatNum)
        {
            if (city.Id != 0)
            {
                City = city;
                CityId = city.Id;
            }
            if (street.Id != 0)
            {
                Street = street;
                StreetId = street.Id;
            }
            if (houseNum != string.Empty) HouseNum = houseNum;
            if (flatNum != string.Empty) FlatNum = flatNum;

        }
        public override Registration Clone()
        {
            Registration registration = (Registration)MemberwiseClone();
            registration.City = City;
            registration.Street = Street;
            return registration;
        }
        public override string SearchString() => $"{City.Name}{Street.Name}{HouseNum}{FlatNum}".PrepareToSearch();
        public override string ToString() => $"{LongString}";

        public override Registration SetEmpty()
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
    }
}