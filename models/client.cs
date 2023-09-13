using System.Diagnostics;
using System;
using System.Globalization;
using System.Threading;


namespace Models
{
    public class Client
    {
        static int nextId;
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public double Phone { get; set; }
        public string Agreement { get; set; }
        public virtual ICollection<Passport> Passports { private get; set; }
        public virtual ICollection<Mailing> Mailings { get; set; }
        public virtual ICollection<Claim> Claims { get; set; }
        public string? Comment { get; set; }
        public int OwnerId { get; set; }
        public virtual Worker Owner { get; set; }
        public bool ToDelete { get; set; }
        public bool AgreementCheck
        {
            get => Agreement != String.Empty ? true : false;
        }
        public string FullName
        {
            get
            {
                return $"{Name,-35}{Address.FullAddress}";
            }
        }

        public Client()
        {
            Id = Interlocked.Increment(ref nextId);
            Address = new Address();
            Agreement = String.Empty;
            Passports = new List<Passport>();
            // Mailings = new Mailing();
            // Claims = new Claim();
            Owner = new Worker();
            ToDelete = false;
        }
        public override string ToString()
        {
            return $"{FullName}{Phone,-10}";
        }
        public string InfoToString()
        {
            string yesOrNo = AgreementCheck == true ? "Есть" : "Нет";
            return @$"
            Название клиента:         {Name}
            Адрес клиента:            {Address.FullAddress}
            Телефон клиента:          {Phone}
            Комментарий:              {Comment}
            Договор:                  {yesOrNo}
            Ответственный сотрудник:  {Owner.FullName}
            ";
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




