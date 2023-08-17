using Models;
using Connection;
using static InOut;

public interface ControlInterface
{
     public static Worker CreateWorker()
    {
        Worker worker = new();
        worker.Name = GetString(MenuText.workerName);
        worker.Surname = GetString(MenuText.workerSurname);
        worker.Birthday = GetDate(MenuText.workerBirth);
        return worker;
    }
//     public static void ChangeWorker(Worker worker)
//     {
//         string name = GetString(MenuText.workerName);
//         string surname = GetString(MenuText.workerSurname);
//         DateTime birth = GetDate(MenuText.workerBirth);
//         worker.ChangeFields(worker, name, surname, birth);

//     }
}