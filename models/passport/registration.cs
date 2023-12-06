namespace Models
{
    public class Registration : Address
    {
        public string FlatNum { get; set; }
        public new string LongString => $"{City.Name,-19}{Street.Name,-28}{HouseNum,-12}{FlatNum,-10}";
        public new string SearchString => $"{City.Name}{Street.Name}{HouseNum}{FlatNum}".PrepareToSearch();

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
        public override string ToString() => $"{LongString}";

        public new object Clone()
        {
            Registration registration = (Registration)MemberwiseClone();
            registration.City = City;
            registration.Street = Street;
            return registration;
        }
    }
}