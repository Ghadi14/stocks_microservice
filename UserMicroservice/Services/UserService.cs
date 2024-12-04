using Dtos;
using Grpc.Core;
using UserMicroservice;
using UserMicroservice.RepositoryService;

namespace UserMicroservice.Services
{
    public class UserService : User.UserBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IUserRepository _userRepository;
        public UserService(ILogger<GreeterService> logger,IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public override Task<CreateUserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            Dtos.CreateUserRequest createUserRequest = new Dtos.CreateUserRequest();
            createUserRequest.cash = request.Cash;
            createUserRequest.username = request.Name;

            CreateUserResponse createUserResponse = _userRepository.CreateUser(createUserRequest);

            CreateUserReply createUserReply = new CreateUserReply();
            createUserReply.Code = createUserResponse.statusCode.code;
            createUserReply.Message = createUserResponse.statusCode.message;

            return Task.FromResult(createUserReply);
        }

        public override Task<UpdateCashReply> UpdateCash(UpdateCashRequest request, ServerCallContext context)
        {
            UpdateUserBalanceRequest updateUserBalanceRequest = new UpdateUserBalanceRequest();
            updateUserBalanceRequest.cash = request.Cash;
            updateUserBalanceRequest.username = request.Name;

           UpdateUserBalanceResponse response= _userRepository.UpdateBalance(updateUserBalanceRequest);

            UpdateCashReply reply = new UpdateCashReply();
            reply.Message = response.statusCode.message;
            reply.Code = response.statusCode.code;


            return Task.FromResult(reply);
        }
    }
}
