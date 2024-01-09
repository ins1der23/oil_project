using Interfaces;
using MenusAndChoices;

namespace Models
{
    public class Passport : BaseElement<Passport>
    {
        private double number;
        private int issuedId;
        private DateTime issueDate;
        private IssuedBy issuedBy;
        private int registrationId;
        private Registration registration;


        public double Number { get => number; set => number = value; }
        public int IssuedId { get => issuedId; set => issuedId = value; }
        public DateTime IssueDate { get => issueDate; set => issueDate = value; }
        public virtual IssuedBy IssuedBy { get => issuedBy!; set => issuedBy = value; }
        public int RegistrationId { get => registrationId; set => registrationId = value; }
        public virtual Registration Registration { get => registration!; set => registration = value; }
        public Passport()
        {
            registration = new();
            issuedBy = new();
            UpdateParameters();
        }
        public override Dictionary<string, object> UpdateParameters()
        {
            Parameters["Number"] = number;
            Parameters["IssuedBy"] = issuedBy;
            Parameters["IssueDate"] = issueDate;
            Parameters["Registration"] = registration;
            return Parameters;
        }
        public override void Change(Dictionary<string, object> parameters)
        {
            double number = parameters["Number"].Wrap<double>();
            IssuedBy issuedBy = parameters["IssuedBy"].Wrap<IssuedBy>();
            DateTime issueDate = parameters["IssueDate"].Wrap<DateTime>();
            Registration registration = parameters["Registration"].Wrap<Registration>();
            if (number != 0) Number = number;
            if (issuedBy.Id != 0)
            {
                IssuedBy = issuedBy;
                IssuedId = issuedBy.Id;
                IssueDate = issueDate;
            }
            if (registration.Id != 0)
            {
                Registration = registration;
                registrationId = registration.Id;
            }
            UpdateParameters();
        }
        public override Passport SetEmpty()
        {
            Id = 0;
            Number = 0;
            IssuedId = 0;
            IssuedBy = new();
            issueDate = new();
            RegistrationId = 0;
            Registration = new();
            UpdateParameters();
            return this;
        }
        public override string SearchString() => $"{Number}".PrepareToSearch();
        public override string Summary() => PassportText.Summary(this);

        public override string ToString()
        {
            string passport = $"{Number} выдан {IssuedId} {IssueDate.Date}";
            return $"{passport}\n Адрес регистрации: {RegistrationId}";
        }

        public override Passport Clone()
        {
            Passport item = (Passport)MemberwiseClone();
            item.IssuedBy = issuedBy;
            item.Registration = registration;
            item.Parameters = new Dictionary<string, object>(Parameters);
            return item;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Passport toCompare = (Passport)obj;
            if (Number.Equals(toCompare.Number) &&
                IssueDate.Equals(toCompare.IssueDate) &&
                IssuedBy.Equals(toCompare.IssuedBy)) return true;
            return false;
        }
        public override int GetHashCode() => Number.GetHashCode() +
                                             IssuedBy.GetHashCode() +
                                             IssueDate.GetHashCode();
    }
}
