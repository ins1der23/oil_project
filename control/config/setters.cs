namespace Controller
{
    static class Setters
    {
        public static bool numResetter;
        private static void ResetByDate()
        {
            DateTime today = DateTime.Today;
            if (today.Day > 25) numResetter = true;
        }
    }
}

