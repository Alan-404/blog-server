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
        private readonly IBlogService _blogService;
        private readonly ICommentLikeService _commentLikeService;

        public CommentController(ICommentService commentService, IAccountService accountService, IUserService userService, IBlogService blogService, ICommentLikeService commentLikeService)
        {
            this._commentService = commentService;
            this._accountService = accountService;
            this._userService  =  userService;
            this._blogService = blogService;
            this._commentLikeService = commentLikeService;
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

        [HttpGet("num/{blogId}")]
        public async Task<IActionResult> GetNumCommentsByBlogId ([FromRoute(Name ="blogId")] string id)
        {
            Blog blog = await this._blogService.GetById(id);
            if (blog == null) return NotFound();

            int numComments = (await this._commentService.GetAllByBlogId(id)).Count;
            return Ok(numComments);
        }

        [HttpGet("reply/{commentId}")]
        public async Task<IActionResult> GetReplies([FromRoute (Name ="commentId")] string commentId)
        {
            List<Comment> comments = await this._commentService.GetAllRepliesById(commentId);
            List<CommentInfo> items = new List<CommentInfo>();
            foreach(var comment in comments)
            {
                User user = await this._userService.GetById(comment.UserId);
                items.Add(new CommentInfo(
                    comment.Id, user.Id, user.FirstName + " " + user.LastName, comment.BlogId, comment.Reply, comment.Content, 0,0, comment.CreatedAt, comment.ModifiedAt
                ));
            }
            return Ok(items);
        }

        [HttpGet("blog/{blogId}")]
        public async Task<IActionResult> GetRootCommentsOfBlog([FromRoute(Name ="blogId")] string id)
        {
            List<Comment> comments = await this._commentService.GetAllRootByBlogId(id);
            List<CommentInfo> items = new List<CommentInfo>();
            foreach (var comment in comments)
            {
                User user = await this._userService.GetById(comment.UserId);
                int numReplies = (await this._commentService.GetAllRepliesById(comment.Id)).Count;
                int numLikes = (await this._commentLikeService.GetByCommentId(comment.Id)).Count;
                items.Add(new CommentInfo(comment.Id, comment.UserId, user.FirstName + " " + user.LastName, comment.BlogId, comment.Reply, comment.Content, numReplies, numLikes, comment.CreatedAt, comment.ModifiedAt));
            }
            return Ok(items);
        }

        [HttpDelete("remove/{commentId}")]
        public async Task<IActionResult> RemoveComment([FromRoute (Name ="commentId")] string commentId)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);

                Comment comment = await this._commentService.GetById(commentId);
                if (comment == null || comment.UserId.Equals(account.UserId) == false) return BadRequest();

                bool removed = await this._commentService.Remove(comment);
                if (removed == false) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);
                return Ok();

            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }
    }
}