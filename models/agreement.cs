using Config;

namespace Models
{
    class Agreement
    {
        public static DateTime Date { get; set; } = DateTime.Today;
        public string AgreemNum { get; set; }
        static int nextNum = ResetNum(nextNum);

        public Agreement()
        {
            AgreemNum = $"{Date.Month} - {Interlocked.Increment(ref nextNum)}";
        }
        private static int ResetNum(int num)
        {
            if (Date.Day < 10 && Setters.numResetter)
            {
                nextNum = 0;
                Setters.numResetter = false;
            }
            return nextNum;
        }
    }
}

