using MenusAndChoices;

namespace Models
{
    public class Registration : BaseElement<Registration>
    {
        private int cityId;
        private City city;
        private int streetId;
        private Street street;
        private string houseNum;
        private string flatNum;

        public int CityId { get => cityId; set => cityId = value; }
        public virtual City City { get => city; set => city = value; }
        public int StreetId { get => streetId; set => streetId = value; }
        public virtual Street Street { get => street!; set => street = value; }
        public string HouseNum { get => houseNum; set => houseNum = value; }
        public string FlatNum { get => flatNum; set => flatNum = value; }
        public string LongString => $"{City.Name,-19}{Street.Name,-28}{HouseNum,-12}{FlatNum,-10}";


        public Registration()
        {
            city = new();
            street = new();
            houseNum = string.Empty;
            flatNum = string.Empty;
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
            City = new();
            Street = new();
            HouseNum = string.Empty;
            FlatNum = string.Empty;
            return this;
        }

        public override string Summary() => RegistrationText.Summary(this);

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Registration item = (Registration)obj;
            if (HouseNum.Equals(item.HouseNum) &&
                FlatNum.Equals(item.FlatNum) &&
                CityId.Equals(item.CityId) &&
                StreetId.Equals(item.StreetId)
                ) return true;
            return false;
        }

        public override int GetHashCode() => Id.GetHashCode() + City.GetHashCode() + Street.GetHashCode() +
                                             HouseNum.GetHashCode() + FlatNum.GetHashCode();
        
    }
}