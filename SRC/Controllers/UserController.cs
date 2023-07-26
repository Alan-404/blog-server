using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.Utils;
using server.SRC.DTOs.Requests;
using server.SRC.DTOs.Responses;
namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/user")]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly ISocialNetworkService _socialNetworkService;
        private readonly IUserSocialNetworkService _userNetworkService;

        public UserController(IUserService userService, IAccountService accountService, ISocialNetworkService socialNetworkService, IUserSocialNetworkService userNetworkService)
        {
            this._userService = userService;
            this._accountService = accountService;
            this._socialNetworkService = socialNetworkService;
            this._userNetworkService = userNetworkService;
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

        [HttpGet("avatar/{userId}")]
        public IActionResult GetAvatar([FromRoute(Name = "userId")] string userId)
        {
            string path = this._userService.GetAvatarPath(userId);
            if (path == null)
                return File(System.IO.File.OpenRead(DefaultPath.userPath), Constant.contentTypeImage);
            return File(System.IO.File.OpenRead(path), Constant.contentTypeImage);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditUserProfile([FromBody] EditUserRequest request)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                User user = await this._userService.GetById(account.UserId);
                if (user == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                foreach (var network in request.Networks)
                {
                    SocialNetwork checkNetwork = await this._socialNetworkService.GetById(network.NetworkId);
                    if (checkNetwork == null) continue;
                    await this._userNetworkService.Save(new UserSocialNetwork(user.Id, network.NetworkId, network.Link));
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Gender = request.Gender;
                user.PhoneNumber = request.PhoneNumber;
                user.BDate = request.BDate;

                User savedUser = await this._userService.Edit(user);
                if (savedUser == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);
                return Ok(savedUser);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetMyProfile()
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                User user = await this._userService.GetById(account.UserId);
                if (user == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                List<SocialNetworkInfo> networks = new List<SocialNetworkInfo>();
                List<UserSocialNetwork> items = await this._userNetworkService.GetByUserId(user.Id);
                foreach (var item in items)
                {
                    networks.Add(new SocialNetworkInfo(item.Id, item.Link));
                }
                return Ok(new UserInfo(
                    user.Id,
                    user.Email,
                    user.FirstName,
                    user.LastName,
                    user.PhoneNumber,
                    user.BDate,
                    user.Gender,
                    networks,
                    user.CreatedAt,
                    user.ModifiedAt
                ));
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }

        [HttpGet("info/{userId}")]
        public async Task<IActionResult> GetInfoByUserId([FromRoute(Name = "userId")] string id)
        {
            User user = await this._userService.GetById(id);
            return Ok(user);
        }

        [HttpPut("avatar")]
        public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarRequest request)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                if (accountId.ToString() == null) return Unauthorized(Message.INVALID_TOKEN);

                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                bool updated = await this._userService.SaveAvatar(account.UserId, request.Avatar);
                if (updated == false) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok();
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }

        
    }
}