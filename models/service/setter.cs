namespace Models
{
    class Setter
    {

        public string? Name { get; set; }
        public bool Status { get; set; }
        public Setter(string name)
        {
            Name = name;
        }
        public void ResetByDate()
        {
            DateTime today = DateTime.Today;
            if (today.Day > 25) Status = true;
        }
        public override string ToString() => $"{Name} = {Status}";
    }
}

