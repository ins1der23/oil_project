using Models;
using Connection;
using static InOut;

public interface ControlInterface
{
     public static Worker CreateWorker()
    {
        string name = GetString(MenuText.workerName);
        string surname = GetString(MenuText.workerSurname);
        DateTime birth = GetDate(MenuText.workerBirth);
        return new Worker(name, surname, birth);
    }
    public static void ChangeWorker(Worker worker)
    {
        string name = GetString(MenuText.workerName);
        string surname = GetString(MenuText.workerSurname);
        DateTime birth = GetDate(MenuText.workerBirth);
        worker.ChangeFields(worker, name, surname, birth);

    }
}