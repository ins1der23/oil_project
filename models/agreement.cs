using Controller;

namespace Models
{
    public class Agreement
    {
        public int Id { get; set; }
        public static DateTime Date { get; set; }
        public string Name { get; set; }
        public string ScanPath { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int PassportId { get; set; }
        public virtual Passport Passport { get; set; }
        static int nextNum = ResetNum(nextNum);

        public Agreement()
        {
            Name = $"{Date.Month} - {Interlocked.Increment(ref nextNum)}";
            ScanPath = String.Empty;
            Client = new();
            Passport = new();
            Date = DateTime.Today;
        }
        private static int ResetNum(int num)
        {
            if (Date.Day < 10 && Settings.numResetter.Status)
            {
                nextNum = 0;
                Settings.numResetter.Status = false;
            }
            return nextNum;
        }
    }
}

