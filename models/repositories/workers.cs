using Connection;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq;

namespace Models
{
    public class Workers
    {
        List<Worker> WorkersList { get; set; }

        public Workers()
        {
            WorkersList = new();
        }
        public void AppendWorker(Worker worker) => WorkersList.Add(worker);
        public void DeleteWorker(int id) => WorkersList.Remove(WorkersList.Where(w => w.Id == id).Single());
        public async Task GetFromSqlAsync(DBConnection user, int id = 0, string? search = null)
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

        public Worker? GetById(int id) => WorkersList.Where(w => w.Id == id).SingleOrDefault();
        public Worker GetFromList(int index) => WorkersList[index - 1];

        public async Task AddSqlAsync(DBConnection user)
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
        public async Task ChangeSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"update workers set 
                    name = @{nameof(Worker.Name)}, 
                    surname = @{nameof(Worker.Surname)},
                    birthday = @{nameof(Worker.Birthday)},  
                    positionId = @{nameof(Worker.PositionId)}
                    where Id = @{nameof(Worker.Id)};";
                await user.Connection.ExecuteAsync(selectQuery, WorkersList);
                user.Close();
            }
        }

        public async Task DeleteSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"delete from workers 
                                        where Id = @{nameof(Worker.Id)};";
                await user.Connection.ExecuteAsync(selectQuery, WorkersList);
                user.Close();
            }
        }

        public List<string> ToStringList()
        {
            List<string> output = new List<string>();
            foreach (var worker in WorkersList)
                output.Add(worker.ToString());
            return output;
        }
    }
}
