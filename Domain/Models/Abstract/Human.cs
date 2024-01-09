namespace Models
{
    public abstract class Human : BaseElement<Human>
    {
        private string name;
        private string middlename;
        private string surname;
        private int passportId;
        private Passport passport;

        public string Name { get => name; set => name = value; }
        public string Middlename { get => middlename; set => middlename = value; }
        public string Surname { get => surname; set => surname = value; }
        public int PassportId { get => passportId; set => passportId = value; }
        public virtual Passport Passport { get => passport; set => passport = value; }

        protected Human() : base()
        {
            name = string.Empty;
            middlename = string.Empty;
            surname = string.Empty;
            passport = new();
        }
    }
}
