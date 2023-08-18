using Connection;
using MySql.Data.MySqlClient;
using Dapper;

namespace Models
{
    class Workers
    {
        List<Worker> WorkersList { get; set; }

        public Workers()
        {
            WorkersList = new();
        }
        public void AppendWorker(Worker worker) => WorkersList.Add(worker);

        public async Task GetFromSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select * from workers";
                var temp = await user.Connection.QueryAsync<Worker>(selectQuery);
                WorkersList = temp.ToList();
                user.Close();
            }
        }

        public async Task GetFromSqlPos(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select * 
                                    from workers as w, positions as p
                                    where w.positionId=p.Id";
                var temp = await user.Connection.QueryAsync<Worker, Position, Worker>(selectQuery, (w, p) =>
                {
                    w.Position = p;
                    return w;
                });
                WorkersList = temp.ToList();
                user.Close();
            }
        }

        public async Task AddToSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"insert workers
                (name, surname, birthday, positionId)
                values (
                @{nameof(Worker.Name)},
                @{nameof(Worker.Surname)},
                @{nameof(Worker.Birthday)},
                @{nameof(Worker.PositionId)})";
                await user.Connection.ExecuteAsync(selectQuery, WorkersList);
                user.Close();
            }
        }

        public List<string> ListWorkers()
        {
            List<string> output = new List<string>();
            foreach (var worker in WorkersList)
                output.Add($"{worker.FullName} {worker.Age} {worker.Position.Name}");
            return output;
        }

    }
}