namespace Models
{
    public class Passport : IModels
    {
        public int Id { get; set; }
        public double Number { get; set; }
        public int IssuedId { get; set; }
        public DateTime IssueDate { get; set; }
        public virtual IssuedBy IssuedBy { get; set; }
        public int RegistrationId { get; set; }
        public virtual Address Registration { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        // public string SearchString => $"{Number}".PrepareToSearch();

        public Passport()
        {
            Client = new();
            Registration = new();
            IssuedBy = new();
        }

        public override string ToString()
        {
            string passport = $"{Number} выдан {IssuedId} {IssueDate.Date}";
            return $"{passport}\n Адрес регистрации: {RegistrationId}";

        }
        public string SearchString() => $"{Number}".PrepareToSearch();

    }
}
