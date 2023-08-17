using System.Data.Common;
using System.Xml;
using System;
using System.Collections.Generic;
using Models;
using MySql.Data.MySqlClient;
using Connection;
class DataBase
{
    List<Position> positionsTable;

    List<Client> clientsTable;
    List<Passport> passportTable;

    public DataBase()
    {
        positionsTable = new();

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
        // }
        // public Worker ReturnWorker(int workerId) => workersTable[workerId - 1];
        // public void DeleteWorker(int workerId) => workersTable.RemoveAt(workerId - 1);
        // public List<Worker> WorkersSearch(string name)
        // {
        //     List<Worker> output = new List<Worker>();
        //     foreach (var worker in workersTable)
        //     {
        //         if (worker.fullName.ToLower().Contains(name.ToLower()))
        //             output.Add(worker);
        //     }
        //     return output;
        // }
        // public List<string> ListWorkerSearch(List<Worker> searchTable)
        // {
        //     List<string> output = new List<string>();
        //     foreach (var worker in searchTable)
        //         output.Add($"{worker.fullName} {worker.age} {positionsTable[worker.positionId - 1].positionName}");
        //     return output;
        // }
        // public Worker WorkerSearchReturn(int index, List<Worker> searchTable) => searchTable[index - 1];

        // public string StringWorker(int workerId)
        // {
        //     int workerIndex = workerId - 1;
        //     return $"{workersTable[workerIndex].fullName} {workersTable[workerIndex].age} {positionsTable[workersTable[workerIndex].positionId - 1].positionName}";
        // }
        
    }

    // public List<string> ListWorkers(DbConnect dbCon)
    // {
    //     string query = "SELECT * FROM workers;";
    //     var cmd = new MySqlCommand(query, dbCon.Connection);
    //     var reader = cmd.ExecuteReader();
    //     List<string> output = new List<string>();
    //     while (reader.Read())
    //     {
    //         output.Add($"{reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
    //     }
    //     return output;
    // }

    public void AppendClient(Client client) => clientsTable.Add(client);
    public void AppendPassprot(Passport passport) => passportTable.Add(passport);

}
