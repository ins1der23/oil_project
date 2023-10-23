using static InOut;
using Controller;

namespace Models
{
    public class Agreement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string Name { get; set; }
        public string ScanPath { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int PassportId { get; set; }
        public virtual Passport Passport { get; set; }
        public string FileName
        {
            get => $"{Name} от {Date.ToShortDateString()}_{Client.Name},{Client.Address.ShortAddress.PrepareToFileName()}";
        }
        public string SearchString
        {
            get => $"and agr.name like \"%{Name}%\" and date = \"{Date.ToString("yyyy-MM.dd")}\"";
        }

        public bool ScanCheck
        {
            get => ScanPath != String.Empty;
        }
        static int nextNum = ResetNum(nextNum);

        public Agreement()
        {
            Name = $"{Interlocked.Increment(ref nextNum)}-{Date.Month}";
            ScanPath = String.Empty;
            Client = new();
            Passport = new();
        }

        // Сброс автоинкрементного счетчика до нуля
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

        public void Change(string name, DateTime date)
        {
            if (name != String.Empty) Name = name;
            Date = date;
        }

        public override string ToString()
        {
            return $"{Name} {Client.FullName}";
        }
    }
}

