using Connection;
using MenusAndChoices;
using static InOut;

namespace Models
{
    public class Worker
    {
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

        public int PositionId { get; private set; }
        public virtual Position Position { get; set; }
        public virtual Clients Clients { get; set; }
        public Worker()
        {
            PositionId = 1;
            Position = new();
            Clients = new();
        }
        public static Worker Create()
        {
            Worker worker = new();
            worker.Name = GetString(Text.workerName);
            worker.Surname = GetString(Text.workerSurname);
            worker.Birthday = GetDate(Text.workerBirth);
            return worker;
        }
        public void Change()
        {
            string name = GetString(Text.workerName);
            string surname = GetString(Text.workerSurname);
            DateTime birth = GetDate(Text.workerBirth);
            if (name != String.Empty) Name = name;
            if (surname != String.Empty) Surname = surname;
            Birthday = birth;
        }
        public void SetPosition(int positionId) => PositionId = positionId;

        public override string ToString() => $"ID:{Id}, {FullName}, {Age}, {Position.Name}";
    }
}
