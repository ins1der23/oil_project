using Controller;

namespace Models
{
    public class Agreement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string ScanPath { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Passport Passport { get; set; }
        public bool ScanCheck
        {
            get => ScanPath != String.Empty ? true : false;
        }
        static int nextNum = ResetNum(nextNum);

        public Agreement()
        {
            Name = $"{Date.Month} - {Interlocked.Increment(ref nextNum)}";
            ScanPath = String.Empty;
            Client = new();
            Passport = new();
            Date = DateTime.Today;
        }

        // Сброс автоинкремнтного счетчика до нуля
        private static int ResetNum(int num)
        {   
            DateTime date = DateTime.Today;
            if (date.Day < 10 && Settings.numResetter.Status)
            {
                nextNum = 0;
                Settings.numResetter.Status = false;
            }
            return nextNum;
        }

        public override string ToString()
        {
            return $"{Name} {Client.FullName}";
        }
    }
}

