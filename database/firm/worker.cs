using System.Threading;
using System;
public class Worker
{
    static int nextId;
    public int workerId { get; private set; }
    private string name { get; set; }
    private string surname { get; set; }
    public string fullName
    {
        get
        {
            return $"{name} {surname}";
        }
    }
    private DateTime birthday { get; set; }
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

    public int positionId;


    public Worker(string name, string surname, DateTime date)
    {
        this.workerId = Interlocked.Increment(ref nextId);
        this.name = name;
        this.surname = surname;
        this.birthday = date;
        this.positionId = 0;
    }

    public void SetPosition(Worker worker, int positionId) => worker.positionId = positionId;

    public override string ToString()
    {
        return $"{this.workerId}. {this.fullName}, {this.age}, {this.positionId}";
    }


}