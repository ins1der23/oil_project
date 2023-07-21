using System;
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
    public string SelectAllPositions()
    {
        string output = String.Empty;
        foreach (var position in positionsTable)
        {
            output += $"{position.ToString()}\n";
        }
        return output;
    }
    public void AppendWorker(Worker worker) => workersTable.Add(worker);
    public string SelectAllWorkers()
    {
        string output = String.Empty;

        foreach (var item in workersTable)
        {
            output += $"{item.fullName} {item.age} {positionsTable[item.positionId].positionName}\n";
        }
        return output;
    }

    public void AppendClient(Client client) => clientsTable.Add(client);
    public void AppendPassprot(Passport passport) => passportTable.Add(passport);

}