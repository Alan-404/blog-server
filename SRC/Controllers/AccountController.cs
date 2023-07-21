using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.DTOs.Requests;
using server.SRC.Utils;
using server.SRC.Middlewares;

namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/account")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly JWTMiddleware _middleware;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            this._accountService = accountService;
            this._userService = userService;
            this._middleware = new JWTMiddleware();
        }

        [HttpPost("auth")]
        public async Task<IActionResult> LoginAccount([FromBody] AccountLoginRequest request)
        {
            User user = await this._userService.GetByEmail(request.Email);
            if (user == null) return NotFound("User not Found");

            Account account = await this._accountService.GetByUserId(user.Id);
            if (account == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

            if (this._accountService.CheckPassword(account.Password, request.Password) == false) return BadRequest("Unmatched Password");

            string accessToken = this._middleware.GenerateToken(account.Id);

            return Ok(accessToken);

        }
    }
}