using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.DTOs.Requests;
using server.SRC.Utils;

namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/view")]
    public class BlogViewController: ControllerBase
    {
        private readonly IBlogViewSerivce _blogViewService;
        private readonly IAccountService _accountService;

        public BlogViewController(IBlogViewSerivce blogViewSerivce, IAccountService accountService)
        {
            this._blogViewService = blogViewSerivce;
            this._accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> AddView([FromBody] AddViewRequest request)
        {
            if (request.BlogId == null) return BadRequest(Message.INVALID_REQUEST);

            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Console.WriteLine(accountId);
                Account account = await this._accountService.GetById(accountId.ToString());
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                BlogView savedItem = await this._blogViewService.Save(account.UserId, request.BlogId);
                if (savedItem == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);
                return Ok();
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }
    }
}