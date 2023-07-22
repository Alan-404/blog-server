using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.Middlewares;
using server.SRC.Utils;
namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/comment")]
    public class CommentController: ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly JWTMiddleware _middleware;
        private readonly IAccountService _accountService;

        public CommentController(ICommentService commentService, IAccountService accountService)
        {
            this._commentService = commentService;
            this._accountService = accountService;
            this._middleware = new JWTMiddleware();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTHORIZATION_HEADER, out var header))
            {
                string accountId = this._middleware.ExtractAccountId(header.ToString());
                if (accountId == null) return Unauthorized(Message.INVALID_TOKEN);

                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                comment.UserId = account.UserId;
                Comment savedComment = await this._commentService.Save(comment);
                if (savedComment == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok(savedComment);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }
    }
}