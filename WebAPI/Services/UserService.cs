using Dtos;
using UserMicroservice;
using static UserMicroservice.User;

namespace WebAPI.Services
{
    public class UserService:IUserService
    {
        private static UserClient _userClient;

        public UserService(UserClient userClient)
        {
            _userClient = userClient;
        }

        public CreateUserResponse CreateUser(Dtos.CreateUserRequest request)
        {
            CreateUserResponse response = new CreateUserResponse();

            UserMicroservice.CreateUserRequest grpcRequest = new UserMicroservice.CreateUserRequest();

            grpcRequest.Cash = request.cash;
            grpcRequest.Name = request.username;

           CreateUserReply reply= _userClient.CreateUser(grpcRequest);

            response.statusCode.message = reply.Message;
            response.statusCode.code = reply.Code;

            return response;
        }

        public UpdateUserBalanceResponse UpdateBalance(UpdateUserBalanceRequest request)
        {
            UpdateUserBalanceResponse response = new UpdateUserBalanceResponse();
            UpdateCashRequest updateCashRequest = new UpdateCashRequest();
            updateCashRequest.Cash = request.cash;
            updateCashRequest.Name = request.username;

            UpdateCashReply reply = _userClient.UpdateCash(updateCashRequest);

            response.statusCode.code = reply.Code;
            response.statusCode.message = reply.Message;


            return response;
        }
    }
}
