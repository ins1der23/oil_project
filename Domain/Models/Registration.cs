using MenusAndChoices;

namespace Models
{
    public class Registration : BaseAddress
    {
        private string flatNum;
        public string FlatNum { get => flatNum; set => flatNum = value; }
        public string LongString => $"{City.Name,-19}{Street.Name,-28}{HouseNum,-12}{FlatNum,-10}";


        public Registration() : base()
        {
            RoleId = 2;
            flatNum = string.Empty;
            UpdateParameters();
        }
        public override Dictionary<string, object> UpdateParameters()
        {
            Parameters["City"] = City;
            Parameters["Street"] = Street;
            Parameters["HouseNum"] = HouseNum;
            Parameters["FlatNum"] = flatNum;
            return Parameters;
        }

        public override void Change(Dictionary<string, object> parameters)
        {

            City city = parameters["City"].Wrap<City>();
            Street street = parameters["Street"].Wrap<Street>();
            string houseNum = parameters["HouseNum"].Wrap<string>();
            string flatNum = parameters["FlatNum"].Wrap<string>();
            if (city.ChooseEmpty == true || city.Id != 0)
            {
                City = city;
                CityId = city.Id;
            }
            if (street.ChooseEmpty == true || street.Id != 0)
            {
                Street = street;
                StreetId = street.Id;
            }
            if (houseNum != string.Empty) HouseNum = houseNum;
            if (flatNum != string.Empty) FlatNum = flatNum;
            UpdateParameters();
        }


        public override Registration Clone()
        {
            Registration registration = (Registration)MemberwiseClone();
            registration.City = City;
            registration.Street = Street;
            registration.Parameters = new Dictionary<string, object>(Parameters);
            return registration;
        }
        public override string ShortString() => $"{City.Name}, {Street.Name}, {HouseNum}";
        public override string SearchString() => $"{City.Name}{Street.Name}{HouseNum}{FlatNum}".PrepareToSearch();
        public override string ToString() => $"{LongString}";

        public override Registration SetEmpty()
        {
            Id = 0;
            CityId = 0;
            City = new();
            StreetId = 0;
            Street = new();
            HouseNum = string.Empty;
            FlatNum = string.Empty;
            UpdateParameters();
            return this;
        }

        public override string Summary() => RegistrationText.Summary(this);

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Registration toCompare = (Registration)obj;
            if (City.Equals(toCompare.City) &&
                Street.Equals(toCompare.Street) &&
                HouseNum.Equals(toCompare.HouseNum) &&
                FlatNum.Equals(toCompare.FlatNum)) return true;
            return false;
        }

        public override int GetHashCode() => City.GetHashCode() +
                                             Street.GetHashCode() +
                                             HouseNum.GetHashCode() +
                                             flatNum.GetHashCode();


    }
}