using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.DTOs.Requests;
using server.SRC.Utils;
using server.SRC.Enums;

namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/network")]
    public class SocialNetworkController: ControllerBase
    {
        private readonly ISocialNetworkService _socialNetworkService;
        private readonly IAccountService _accountService;

        public SocialNetworkController(ISocialNetworkService socialNetworkService, IAccountService accountService)
        {
            this._socialNetworkService = socialNetworkService;
            this._accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSocialNetworks()
        {
            List<SocialNetwork> networks = await this._socialNetworkService.GetAll();
            return Ok(networks);
        }

        [HttpPost]
        public async Task<IActionResult> AddSocialNetwork([FromForm] AddSocialNetworkRequest request)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);
                if (account.Role.Equals(RoleEnum.ADMIN.ToString().ToLower()) == false) return BadRequest(Message.FORBIDDEN_CLIENT);
                
                SocialNetwork network = new SocialNetwork(request.Name);

                SocialNetwork savedNetwork = await this._socialNetworkService.Save(network);

                if (savedNetwork == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                bool savedImage = await this._socialNetworkService.SaveImage(savedNetwork.Id, request.File);
                if (savedImage == false) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok(savedNetwork);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }

        [HttpGet("media/{id}")]
        public IActionResult GetMedia(int id)
        {
            string filePath = this._socialNetworkService.GetFilePath(id);
            if (filePath == null)
                filePath = DefaultPath.socialNetworkPath;

            return File(System.IO.File.OpenRead(filePath), Constant.contentTypeImage);
        }
    }
}