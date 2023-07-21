using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.Utils;
using server.SRC.DTOs.Requests;
using server.SRC.Middlewares;
namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/user")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly JWTMiddleware _middleware;

        public UserController(IUserService userService, IAccountService accountService)
        {
            this._userService = userService;
            this._accountService = accountService;
            this._middleware = new JWTMiddleware();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest request)
        {
            User checkedUser = await this._userService.GetByEmail(request.Email);
            if (checkedUser != null) return BadRequest("Email has been taken");

            User user = new User(request.Email, request.FirstName, request.LastName);
            User savedUser = await this._userService.Save(user);
            if(savedUser == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

            Account account = new Account(savedUser.Id, request.Password, request.Role);
            Account savedAccount = await this._accountService.Save(account);
            if (savedAccount == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);
                
            return Ok(savedUser);
        }


    }
}