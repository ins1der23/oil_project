namespace Models
{
    public class IssuedBy : IModels, ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IssuedBy()
        {
            Name = string.Empty;
        }
        public void Change(string name)
        {
            if (name != string.Empty) Name = name;
        }
        public override string ToString() => $"{Name}";

        public object Clone() => MemberwiseClone();
    }
}