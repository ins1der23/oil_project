
namespace Models
{
    public class IssuedBy : BaseElement<IssuedBy>
    {
        private string name;
        internal string Name { get => name!; set => name = value; }

        public IssuedBy() : base()
        {
            name = string.Empty;
            UpdateParameters();
        }

        public override Dictionary<string,object> UpdateParameters()
        {
            Parameters["Name"] = name;
            return Parameters;
        }
        public override void Change(Dictionary<string, object> parameters)
        {
            string name = parameters["Name"].Wrap<string>();
            if (name != string.Empty) Name = name;
            UpdateParameters();
        }


        public override string ToString() => $"{Name}";
        public override string SearchString() => Name.PrepareToSearch();
        public override IssuedBy Clone()
        {
            IssuedBy item = (IssuedBy)MemberwiseClone();
            item.Parameters = new Dictionary<string, object>(Parameters);
            return item;
        }
        public override IssuedBy SetEmpty()
        {
            Id = 0;
            name = string.Empty;
            UpdateParameters();
            return this;
        }

        public override string Summary() => ToString();

       // override object.Equals
        public override bool Equals(object? obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            IssuedBy toCompare = (IssuedBy)obj;
            if (Name.Equals(toCompare.Name)) return true;
            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}