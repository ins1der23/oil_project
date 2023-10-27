namespace Models
{
    public class Client : IModels, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public double Phone { get; set; }
        public string? Comment { get; set; }
        public int OwnerId { get; set; }
        public virtual Worker Owner { get; set; }
        public virtual ICollection<Agreement> Agreements { get; set; }
        public virtual ICollection<Passport> Passports { private get; set; }
        public bool ToDelete { get; set; }
        public bool AgreementCheck
        {
            get => Agreements.Any();
        }
        public string FullName
        {
            get
            {
                return $"{Name,-35}{Address.LongString}";
            }
        }
        public string ShortName
        {
            get
            {
                return $"{Name}, {Address.ShortString}";
            }
        }


        public Client()
        {
            Name = string.Empty;
            Address = new Address();
            Owner = new Worker();
            Agreements = new List<Agreement>();
            Passports = new List<Passport>();
            ToDelete = false;
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

        public object Clone() 
        {
            Client client = (Client)MemberwiseClone();
            client.Address = Address;
            client.Owner = Owner;
            client.Agreements = Agreements;
            client.Passports = Passports;
            return client;
        } 
    }
}












