using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.Utils;
using server.SRC.DTOs.Requests;

namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/like")]
    public class CommentLikeController: ControllerBase
    {
        private readonly ICommentLikeService _commentLikeService;
        private readonly IAccountService _accountService;

        public CommentLikeController(ICommentLikeService commentLikeService, IAccountService accountService)
        {
            this._commentLikeService = commentLikeService;
            this._accountService = accountService;
        }

        [HttpDelete("remove/{commentId}")]
        public async Task<IActionResult> RemoveCommentLike([FromRoute(Name ="commentId")] string commentId)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                CommentLike like = await this._commentLikeService.GetByCommentIdAndUserId(commentId, account.UserId);
                if (like == null) return BadRequest(Message.INVALID_REQUEST);

                bool removed = await this._commentLikeService.Remove(like);
                if (removed == false) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok();
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }   

        [HttpPost("add")]
        public async Task<IActionResult> AddCommentLike([FromBody] AddCommentLikeRequest request)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                CommentLike savedLike = await this._commentLikeService.Save(new CommentLike(request.CommentId, account.UserId));
                if (savedLike == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok(savedLike);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }   
    }
}