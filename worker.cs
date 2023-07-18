using System.Threading;
using System;
class Worker
{
    static int nextId;
    public int workerId { get; private set; }
    public string name { get; set; }
    public string surname { get; set; }
    public DateTime birthday { get; set; }
    public int age
    {
        get
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;
            return age;
        }
    }

    
    public Worker(string name, string surname, (int, int, int) date)
    {
        this.workerId = Interlocked.Increment(ref nextId);
        this.name = name;
        this.surname = surname;
        this.birthday = new DateTime(date.Item1, date.Item2, date.Item3);
    }

    public override string ToString() 
    {
        return $"{this.workerId}. {this.name} {this.surname}, {this.birthday.ToShortDateString()}, {this.age}";
    } 


}