using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.DTOs.Responses;
using server.SRC.Utils;
namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/comment")]
    public class CommentController: ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService, IAccountService accountService, IUserService userService)
        {
            this._commentService = commentService;
            this._accountService = accountService;
            this._userService  =  userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                if (accountId.ToString() == null) return Unauthorized(Message.INVALID_TOKEN);

                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                comment.UserId = account.UserId;
                Comment savedComment = await this._commentService.Save(comment);
                if (savedComment == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok(savedComment);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }

        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetRootCommentsOfBlog([FromRoute(Name ="blogId")] string id)
        {
            List<Comment> comments = await this._commentService.GetAllRootByBlogId(id);
            List<CommentInfo> items = new List<CommentInfo>();
            foreach (var comment in comments)
            {
                User user = await this._userService.GetById(comment.UserId);
                items.Add(new CommentInfo(comment.Id, comment.UserId, user.FirstName + " " + user.LastName, comment.BlogId, comment.Reply, comment.Content, comment.CreatedAt, comment.ModifiedAt));
            }
            return Ok(items);
        }
    }
}