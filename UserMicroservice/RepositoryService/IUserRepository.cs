using Dtos;

namespace UserMicroservice.RepositoryService
{
    public interface IUserRepository
    {
        public CreateUserResponse CreateUser(Dtos.CreateUserRequest request);
        public UpdateUserBalanceResponse UpdateBalance(UpdateUserBalanceRequest request);
    }
}
