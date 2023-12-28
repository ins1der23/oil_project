namespace Models
{
    public class Passport : IModels
    {
        private int id;
        private double number;
        private int issuedId;
        private DateTime issueDate;
        private IssuedBy? issuedBy;
        private int registrationId;
        private Address? registration;
        private int clientId;
        private Client? client;

        public int Id { get => id; set => id = value; }
        public double Number { get => number; set => number = value; }
        public int IssuedId { get => issuedId; set => issuedId = value; }
        public DateTime IssueDate { get => issueDate; set => issueDate = value; }
        public virtual IssuedBy IssuedBy { get => issuedBy!; set => issuedBy = value; }
        public int RegistrationId { get => registrationId; set => registrationId = value; }
        public virtual Address Registration { get => registration!; set => registration = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public virtual Client Client { get => client!; set => client = value; }

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
