using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace FDMS
{
    /*
     * Class: SqlDataAccess
     * Description: This class is responsible for interacting with a database to store and recieve Telemetry data
     */
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public string ConnectionString { get; set; } = "Default";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionString);
            //string connectionString = "Data Source = SASHAMALESEFDB0; Initial Catalog = GroundTerminal; Integrated Security = True";

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data.ToList();
            }
        }


        public async Task SaveData<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionString);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(sql, parameters);

            }
        }
    }
}
