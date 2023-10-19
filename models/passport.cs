
namespace Models
{
    public class Passport
    {
        static int nextId;
        public int Id { get; private set; }
        public int passportSerial { get; set; }
        public int passportNum { get; set; }
        public DateOnly issueDate { get; set; }
        public string? issueAuthority { get; set; }
        public string? address { get; set; }

        public Passport()
        {
            Id = Interlocked.Increment(ref nextId);

        }

        public override string ToString()
        {
            string passport = $"{this.passportSerial} {this.passportNum} выдан {this.issueAuthority} {this.issueDate}";
            return $"{passport}\n Адрес регистрации: {this.address}";

        }

    }
}
