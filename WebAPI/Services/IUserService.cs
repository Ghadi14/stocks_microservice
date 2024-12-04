using Dtos;

namespace WebAPI.Services
{
    public interface IUserService
    {
        public CreateUserResponse CreateUser(Dtos.CreateUserRequest request);
        public UpdateUserBalanceResponse UpdateBalance(UpdateUserBalanceRequest request);


    }
}
