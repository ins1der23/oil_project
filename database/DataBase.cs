using System;
using System.Collections.Generic;
class DataBase
{
    List<Position> positionsTable;
    List<Worker> workersTable;
    List<Client> clientsTable;
    List<Passport> passportTable;

    public DataBase()
    {
        positionsTable = new();
        workersTable = new();
        clientsTable = new();
        passportTable = new();

    }

    public void AppendPosition(Position position) => positionsTable.Add(position);
    public List<string> SelectAllPositions()
    {
        List<string> output = new List<string>();
        foreach (var position in positionsTable)
        {
            output.Add(position.ToString());
        }
        return output;
    }
    public void AppendWorker(Worker worker) => workersTable.Add(worker);
    
    public string SelectWorker(int workerId)
    {
       return $"{workersTable[workerId].fullName} {workersTable[workerId].age} {positionsTable[workersTable[workerId].positionId].positionName}";
    }
    public List<string> SelectAllWorkers()
    {
        List<string> output = new List<string>();

        foreach (var item in workersTable)
        {
            output.Add($"{item.fullName} {item.age} {positionsTable[item.positionId].positionName}");
        }
        return output;
    }

    public void AppendClient(Client client) => clientsTable.Add(client);
    public void AppendPassprot(Passport passport) => passportTable.Add(passport);

}