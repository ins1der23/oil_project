
namespace Models
{
    public class Passport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Serial { get; set; }
        public double Number { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueAuthority { get; set; }
        public int RegistrationId { get; set; }
        public virtual Address Registration { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public string SearchString => $"{Serial}{Number}";
        public Passport()
        {
            Name = string.Empty;
            Surname = string.Empty;
            IssueAuthority = string.Empty;
            Client = new();
            Registration = new();
        }

        public override string ToString()
        {
            string passport = $"{this.Serial} {this.Number} выдан {this.IssueAuthority} {this.IssueDate.Date}";
            return $"{passport}\n Адрес регистрации: {this.RegistrationId}";

        }

    }
}
