using Connection;
using MySql.Data.MySqlClient;
using Dapper;

namespace Models
{
    class Positions
    {
        List<Position> PositionsList {get; set;}

        public Positions()
        {
            PositionsList = new();
            
        }

        public async Task GetFromSqlAsync(DBConnection user)
        {
            await user.ConnectAsync();
            if (user.IsConnect)
            {
                string selectQuery = $@"select * from positions";
                var temp = await user.Connection.QueryAsync<Position>(selectQuery);
                PositionsList = temp.ToList();
                user.Close();
            }
        }

        public List<string> SelectAll()
        {

            List<string> output = new List<string>();
            foreach (var position in PositionsList)
            {
                output.Add(position.ToString());
            }
            return output;
        }




    }
}
