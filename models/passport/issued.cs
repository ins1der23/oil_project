namespace Models
{
    public class IssuedBy : IModels
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IssuedBy()
        {
            Name = string.Empty;
        }
        public override string ToString() => $"{Name}";
    }
}