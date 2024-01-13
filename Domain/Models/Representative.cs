
using MenusAndChoices;

namespace Models
{
    public class Representative : Human
    {
        private int clientId;
        private Client client;
        public int ClientId { get => clientId; set => clientId = value; }
        public Client Client { get => client; set => client = value; }

        public Representative() : base()
        {
            RoleId = 2;
            client = new();
            UpdateParameters();
        }
        public override Dictionary<string, object> UpdateParameters()
        {
            Parameters["Name"] = Name;
            Parameters["Middlename"] = Middlename;
            Parameters["Surname"] = Surname;
            Parameters["Passport"] = Passport;
            Parameters["Client"] = Client;
            return Parameters;
        }

        public override void Change(Dictionary<string, object> parameters)
        {
            string name = parameters["Name"].Wrap<string>();
            string middlename = parameters["Middlename"].Wrap<string>();
            string surname = parameters["Surname"].Wrap<string>();
            Passport passport = parameters["Passport"].Wrap<Passport>();
            Client client = parameters["Client"].Wrap<Client>();
            if (name != string.Empty) Name = name;
            if (middlename != string.Empty) Middlename = middlename;
            if (surname != string.Empty) Surname = surname;
            if (passport.ChooseEmpty == true || passport.Id != 0)
            {
                Passport = passport;
                PassportId = passport.Id;
            }
            if (client.ChooseEmpty == true || client.Id != 0)
            {
                Client = client;
                ClientId = client.Id;
            }
            UpdateParameters();
        }
        public override Representative SetEmpty()
        {
            Id = 0;
            Name = string.Empty;
            Middlename = string.Empty;
            Surname = string.Empty;
            PassportId = 0;
            Passport = new();
            ClientId = 0;
            Client = new();
            UpdateParameters();
            return this;
        }

        public override string SearchString() => ToString().PrepareToSearch();
        public override string Summary() => RepresentativeText.Summary(this);
        public override string ToString() => $"{Name} {Middlename} {Surname}";



        public override Representative Clone()
        {
            Representative item = (Representative)MemberwiseClone();
            item.Passport = Passport;
            item.Client = Client;
            item.Parameters = new Dictionary<string, object>(Parameters);
            return item;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Representative toCompare = (Representative)obj;
            if (Name.Equals(toCompare.Name) &&
                Middlename.Equals(toCompare.Middlename) &&
                Surname.Equals(toCompare.Surname) &&
                Passport.Equals(toCompare.Passport))
                return true;
            return false;
        }

        public override int GetHashCode() => Name.GetHashCode() +
                                             Middlename.GetHashCode() +
                                             Surname.GetHashCode();
    }
}