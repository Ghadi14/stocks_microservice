using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PostgresqlHelper
{
    public class PostgresqlService:IPostgresService
    {
        private static IConfiguration _configuration;

        public PostgresqlService(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        //public async Task<DynamicParameters> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters)
        //{
        //    string cnxstring = _configuration.GetSection("ConnectionStrings").GetSection("Postgresql").Value;
        //    using (var conn = new NpgsqlConnection(cnxstring))
        //    {

        //        conn.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        //    }
        //    return parameters;
        //}

        //public async Task<DynamicParameters> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters)
        //{
        //    string cnxstring = _configuration.GetSection("ConnectionStrings").GetSection("Postgresql").Value;
        //    using (var conn = new NpgsqlConnection(cnxstring))
        //    {
        //        var result = await conn.QueryFirstOrDefaultAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        //        foreach (var property in result)
        //        {
        //            parameters.Add(property.Key, property.Value);
        //        }
        //    }
        //    return parameters;
        //}

        public async Task<DynamicParameters> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters)
        {
            string cnxstring = _configuration.GetSection("ConnectionStrings").GetSection("Postgresql").Value;
            using (var conn = new NpgsqlConnection(cnxstring))
            {
                var result = await conn.QueryAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                var firstResult = result.FirstOrDefault(); // Get the first row of the result set
                if (firstResult != null)
                {
                    foreach (var property in (IDictionary<string, object>)firstResult)
                    {
                        parameters.Add(property.Key, property.Value);
                    }
                }
            }
            return parameters;
        }


    }
}
