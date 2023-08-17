using System.Threading;
using System;

namespace Models
{
    public class Worker
    {
        static int nextId;
        public int Id { get; private set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
        public DateTime Birthday { get; set; }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - Birthday.Year;
                if (Birthday > today.AddYears(-age)) age--;
                return age;
            }
        }

        public int PositionId { get; set; }

        public Worker()
        {
            this.Id = Interlocked.Increment(ref nextId);
            this.PositionId = 1;
        }

        public void SetPosition(int positionId) => PositionId = positionId;

        public void ChangeFields(string name, string surname, DateTime date)
        {
            if (name != String.Empty) Name = name;
            if (surname != String.Empty) Surname = surname;
            Birthday = date;
        }
        public override string ToString()
        {
            return $"{Id}. {FullName}, {Age}, {PositionId}";
        }

    }
}
