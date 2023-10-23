
namespace Models
{
    public class Passport
    {
        public int Id { get; set; }
        public int PassportSerial { get; set; }
        public double PassportNum { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueAuthority { get; set; }
        public string Registration { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public Passport()
        {
            IssueAuthority = String.Empty;
            Registration = String.Empty;
            Client = new();
        }

        public override string ToString()
        {
            string passport = $"{this.PassportSerial} {this.PassportNum} выдан {this.IssueAuthority} {this.IssueDate.Date}";
            return $"{passport}\n Адрес регистрации: {this.Registration}";

        }

    }
}
