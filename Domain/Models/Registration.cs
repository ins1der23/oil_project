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

        internal int CityId { get => cityId; set => cityId = value; }
        internal virtual City City { get => city; set => city = value; }
        internal int StreetId { get => streetId; set => streetId = value; }
        internal virtual Street Street { get => street!; set => street = value; }
        internal string HouseNum { get => houseNum; set => houseNum = value; }
        internal string FlatNum { get => flatNum; set => flatNum = value; }
        internal string LongString => $"{City.Name,-19}{Street.Name,-28}{HouseNum,-12}{FlatNum,-10}";


        public Registration() : base()
        {
            city = new();
            street = new();
            houseNum = string.Empty;
            flatNum = string.Empty;
            UpdateParameters();
        }

        public override void Change(Dictionary<string, object> parameters)
        {

            City city = parameters["City"].Wrap<City>();
            Street street = parameters["Street"].Wrap<Street>();
            string houseNum = parameters["HouseNum"].Wrap<string>();
            string flatNum = parameters["FlatNum"].Wrap<string>();
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
            UpdateParameters();
        }


        public override Registration Clone()
        {
            Registration registration = (Registration)MemberwiseClone();
            registration.City = city;
            registration.Street = street;
            registration.Parameters = new Dictionary<string, object>(Parameters);
            return registration;
        }
        public override string SearchString() => $"{City.Name}{Street.Name}{HouseNum}{FlatNum}".PrepareToSearch();
        public override string ToString() => $"{LongString}";

        public override Registration SetEmpty()
        {
            Id = 0;
            cityId = 0;
            City = new();
            streetId = 0;
            Street = new();
            HouseNum = string.Empty;
            FlatNum = string.Empty;
            UpdateParameters();
            return this;
        }

        public override void UpdateParameters()
        {
            Parameters["City"] = city;
            Parameters["Street"] = street;
            Parameters["HouseNum"] = houseNum;
            Parameters["FlatNum"] = flatNum;
        }

        public override string Summary() => RegistrationText.Summary(this);

    }
}