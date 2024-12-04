using Dapper;
using Dtos;
using PostgresqlHelper;
using static UserMicroservice.User;
using System.Data;

namespace UserMicroservice.RepositoryService
{
    public class UserRepository:IUserRepository
    {
        private static IPostgresService _postgresService;

        public UserRepository(IPostgresService postgresService)
        {
            _postgresService = postgresService;
        }

        public CreateUserResponse CreateUser(Dtos.CreateUserRequest request)
        {
            CreateUserResponse response = new CreateUserResponse();

            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("p_username", request.username, DbType.String);
            parameters.Add("p_cash", request.cash, DbType.Decimal);

            parameters = _postgresService.ExecuteStoredProcedure("public.create_user", parameters).Result;

            int statuscode = parameters.Get<int>("status_code");
            string message = parameters.Get<string>("message");

            response.statusCode.code = statuscode;
            response.statusCode.message = message;

            return response;
        }
        public UpdateUserBalanceResponse UpdateBalance(UpdateUserBalanceRequest request)
        {
            UpdateUserBalanceResponse response = new UpdateUserBalanceResponse();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("p_username", request.username, DbType.String);
            parameters.Add("new_cash", request.cash, DbType.Decimal);

            parameters = _postgresService.ExecuteStoredProcedure("public.update_user_cash", parameters).Result;

            int statuscode = parameters.Get<int>("status_code");
            string message = parameters.Get<string>("message");

            response.statusCode.code = statuscode;
            response.statusCode.message = message;
            return response;
        }
    }
}
