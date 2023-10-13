using System.Diagnostics;
using System;
using System.Globalization;
using System.Threading;


namespace Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public double Phone { get; set; }
        public virtual ICollection<Passport> Passports { private get; set; }
        public virtual ICollection<Mailing> Mailings { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public virtual ICollection<Agreement> Agreements { get; set; }
        public string? Comment { get; set; }
        public int OwnerId { get; set; }
        public virtual Worker Owner { get; set; }
        public bool ToDelete { get; set; }
        public bool AgreementCheck
        {
            get => Agreements.Any() ? true : false;
        }
        public string FullName
        {
            get
            {
                return $"{Name,-35}{Address.FullAddress}";
            }
        }
        public string ShortName
        {
            get
            {
                return $"{Name}, {Address.ShortAddress}";
            }
        }


        public Client()
        {
            Name = String.Empty;
            Address = new Address();
            Passports = new List<Passport>();
            Agreements = new List<Agreement>();
            // Mailings = new Mailing();
            // Claims = new Claim();
            Owner = new Worker();
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

    }
}









// public static void ChangeFields(Client client, string address, string phone, string name, string contact)
// {
//     if (address != String.Empty)
//         client.address = address;
//     if (phone != String.Empty)
//         client.phone = phone;
//     if (name != String.Empty)
//         client.name = name;
//     if (contact != String.Empty)
//         client.contact = contact;
// }
// public static void AddAgreement(Client client, string path) => client.agreement = path;
// public static void ChangeAgreement(Client client, string path)
// {
//     if (path != String.Empty) client.agreement = path;
// }
// public static void DelAgreement(Client client) => client.agreement = String.Empty;
// public static void SetPassportId(Client client, int passportId) => client.passportId = passportId;




