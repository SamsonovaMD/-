using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planner_with_postgresql
{
    class DB
    {
        // Создаем подключение к базе данных
        NpgsqlConnection connection = new NpgsqlConnection("server=localhost;port = 5432;username = postgres;password = kam154t;database = postgres");
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public NpgsqlConnection getConnection()
        {
            return connection;
        }
    }
}
