using MySql.Data.MySqlClient;
using Dapper;
using Connection;
using static InOut;

namespace Models
{
    public class District
    {
        static int nextId;
        public int Id { get; private set; }
        public string? Name { get; set; }
        public int CityId { get; private set; }
        public virtual City City { get; set; }

        public District()
        {
            Id = Interlocked.Increment(ref nextId);
            City = new();
        }
        public void Create() => Name = GetString(MenuText.districtName);
    }

    public class Districts
    {
        List<District> DistrictsList { get; set; }
        public Districts()
        {
            DistrictsList = new();
        }
    }
}