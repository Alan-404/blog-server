using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.DTOs.Requests;
using server.SRC.DTOs.Responses;
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

        [HttpGet("auth")]
        public async Task<IActionResult> GetInfoByToken()
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTHORIZATION_HEADER, out var authorizationHeader))
            {
                string accountId = this._middleware.ExtractAccountId(authorizationHeader.ToString());
                if (accountId == null) return Unauthorized(Message.INVALID_TOKEN);

                Account account = await this._accountService.GetById(accountId);
                if (account == null) return NotFound("Not Found User");

                User user = await this._userService.GetById(account.UserId);
                if (user == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok(new GetInfoByTokenResponse(user.Id, user.Email, user.FirstName, user.LastName, account.Role));
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }
    }
}