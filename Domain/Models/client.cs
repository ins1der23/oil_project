using Controller;
using Interfaces;

namespace Models
{
    public class Client : BaseElement<Client>
    {
        private string name;
        private int addressId;
        private Address address;
        private double phone;
        private string comment;
        private int ownerId;
        private Worker owner;
        private ICollection<Agreement> agreements;
        private ICollection<Passport> passports;
        private bool toDelete;

        internal string Name { get => name; set => name = value; }
        internal int AddressId { get => addressId; set => addressId = value; }
        internal virtual Address Address { get => address; set => address = value; }
        internal double Phone { get => phone; set => phone = value; }
        internal string Comment { get => comment; set => comment = value; }
        internal int OwnerId { get => ownerId; set => ownerId = value; }
        internal virtual Worker Owner { get => owner; set => owner = value; }
        internal virtual ICollection<Agreement> Agreements { get => agreements; set => agreements = value; }
        internal virtual ICollection<Passport> Passports { private get => passports; set => passports = value; }
        internal bool ToDelete { get => toDelete; set => toDelete = value; }
        internal bool AgreementCheck => Agreements.Any();
        internal string FullName => $"{Name,-35}{Address.LongString}";
        internal string ShortName => $"{Name}, {Address.ShortString}";

        public Client()
        {
            name = string.Empty;
            address = new Address();
            comment = string.Empty;
            ownerId = Settings.UserId;
            owner = new Worker();
            agreements = new List<Agreement>();
            passports = new List<Passport>();
            toDelete = false;
        }
        public void Change(string name, Address address, double phone, string comment)
        {
            if (name != String.Empty) Name = name;
            if (address.Id != 0)
            {
                Address = address;
                AddressId = address.Id;
            }
            if (phone != 0) Phone = phone;
            if (comment != String.Empty) Comment = comment;
        }

        public override string ToString()
        {
            return $"{FullName}{Phone,-10}";
        }

        public override Client Clone()
        {
            Client client = (Client)MemberwiseClone();
            client.Address = Address;
            client.Owner = Owner;
            client.Agreements = Agreements;
            client.Passports = Passports;
            return client;
        }

        public override string SearchString() => $"{Name}{Address.SearchString}{Phone}".PrepareToSearch();

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Client client = (Client)obj;
            if (this.Name == client.Name &&
                this.AddressId == client.AddressId &&
                this.Phone == client.Phone) return true;
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode() + name.GetHashCode() + address.GetHashCode();
        }

        public override Client SetEmpty()
        {
            throw new NotImplementedException();
        }


        public override string Summary()
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












