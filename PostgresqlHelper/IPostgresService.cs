using Dapper;

namespace PostgresqlHelper
{
    public interface IPostgresService
    {
        public Task<DynamicParameters> ExecuteStoredProcedure(string storedProcedureName, DynamicParameters parameters);

    }
}
