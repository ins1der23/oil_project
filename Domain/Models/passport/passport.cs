using Interfaces;

namespace Models
{
    public class Passport : BaseElement<Passport>
    {
        private double number;
        private int issuedId;
        private DateTime issueDate;
        private IssuedBy? issuedBy;
        private int registrationId;
        private Registration? registration;
        private int clientId;
        private Client? client;

        public double Number { get => number; set => number = value; }
        public int IssuedId { get => issuedId; set => issuedId = value; }
        public DateTime IssueDate { get => issueDate; set => issueDate = value; }
        public virtual IssuedBy IssuedBy { get => issuedBy!; set => issuedBy = value; }
        public int RegistrationId { get => registrationId; set => registrationId = value; }
        public virtual Registration Registration { get => registration!; set => registration = value; }
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
        public override string SearchString() => $"{Number}".PrepareToSearch();

        public override Passport SetEmpty()
        {
            throw new NotImplementedException();
        }

        public override Passport Clone()
        {
            throw new NotImplementedException();
        }

        public override string Summary()
        {
            throw new NotImplementedException();
        }


        public override bool Equals(object? obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
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
