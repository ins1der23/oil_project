using Interfaces;

namespace Models
{
    public class IssuedBy : BaseElement<IssuedBy>, ICloneable<IssuedBy>
    {
        private string? name;

        public string Name { get => name!; set => name = value; }

        public IssuedBy()
        : base()
        {
            Name = string.Empty;
        }
        public void Change(string name)
        {
            if (name != string.Empty) Name = name;
        }
        public override string ToString() => $"{Name}";
        public override string SearchString() => Name.PrepareToSearch();
        public override IssuedBy Clone()
        {
            return (IssuedBy)MemberwiseClone();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            IssuedBy issuedBy = (IssuedBy)obj;
            if (Name.Equals(issuedBy.Name)) return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + Name.GetHashCode();
        }

        public override IssuedBy SetEmpty()
        {
            Id = 0;
            Name = string.Empty;
            return this;
        }

        public override string Summary() => ToString();
        
    }
}