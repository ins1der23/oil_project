using static InOut;
namespace Models

{
    public class Street
    {
        static int nextId;
        public int Id { get; private set; }
        public string? Name { get; set; }
        public int CityId { get; private set; }
        public virtual City City { get; set; }

        public Street()
        {
            Id = Interlocked.Increment(ref nextId);
            City = new();
        }
        public void Create() => Name = GetString(MenuText.streetName);
    }

    public class Streets
    {
        List<Street> StreetsList { get; set; }

        public Streets()
        {
            StreetsList = new();
        }
    }
}