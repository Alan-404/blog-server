using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.DTOs.Requests;
using server.SRC.Middlewares;
using server.SRC.Utils;
using server.SRC.Enums;

namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/blog")]
    public class BlogController: ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly JWTMiddleware _middleware;
        private readonly IAccountService _accountService;

        public BlogController(IBlogService blogService, IAccountService accountService)
        {
            this._blogService = blogService;
            this._accountService = accountService;
            this._middleware = new JWTMiddleware();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBlog([FromBody] AddBlogRequest request)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTHORIZATION_HEADER, out var authorizationHeader))
            {
                string accountId = this._middleware.ExtractAccountId(authorizationHeader.ToString());
                if (accountId == null) return Unauthorized(Message.INVALID_TOKEN);

                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);
                else if (account.Role.Equals(RoleEnum.ADMIN.ToString().ToLower()) == false) return Forbid(Message.FORBIDDEN_CLIENT); 

                Blog blog = new Blog(request.Title, request.Introduction, request.Content);
                Blog savedBlog = await this._blogService.Save(blog);
                if (savedBlog == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);
                
                return Ok(savedBlog);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }
    }
}