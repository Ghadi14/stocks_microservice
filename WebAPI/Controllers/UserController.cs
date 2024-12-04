using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public CreateUserResponse Create(CreateUserRequest request)
        {

            return _userService.CreateUser(request);
        }

        [HttpPost("UpdateBalance")]
        public UpdateUserBalanceResponse UpdateBalance(UpdateUserBalanceRequest request)
        {

            return _userService.UpdateBalance(request);
        }
    }
}
