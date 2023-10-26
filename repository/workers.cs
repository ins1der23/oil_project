using Connection;

namespace Models
{
    public class Workers : IRepository
    {
        List<Worker> WorkersList { get; set; }
        public Workers()
        {
            WorkersList = new();
        }
        public void Clear() => WorkersList = new();
        public void Append(Worker worker) => WorkersList.Add(worker);
        public Worker GetFromList(int index) => WorkersList[index - 1];
        public async Task GetFromSqlAsync(DBConnection user, string search = "")
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = @$"select *
                                    from workers as w, positions as p
                                    where w.positionId=p.Id
                                    and (w.surname like ""%{search}%"" or w.name like ""%{search}%"")
                                    order by positionId";
                var temp = await user.Connection!.QueryAsync<Worker, Position, Worker>(selectQuery, (w, p) =>
                {
                    w.Position = p;
                    return w;
                });
                WorkersList = temp.ToList();
                user.Close();
            }
        }
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
                await user.Connection!.ExecuteAsync(selectQuery, WorkersList);
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
                await user.Connection!.ExecuteAsync(selectQuery, WorkersList);
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
                await user.Connection!.ExecuteAsync(selectQuery, WorkersList);
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
