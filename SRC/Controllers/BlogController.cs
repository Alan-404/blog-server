using Microsoft.AspNetCore.Mvc;
using server.SRC.Models;
using server.SRC.Services;
using server.SRC.DTOs.Requests;
using server.SRC.Utils;
using server.SRC.DTOs.Responses;
using server.SRC.Enums;

namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/blog")]
    public class BlogController: ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IBlogViewSerivce _blogViewService;

        public BlogController(IBlogService blogService, IAccountService accountService, IUserService userService, ICommentService commentService, IBlogViewSerivce blogViewService)
        {
            this._blogService = blogService;
            this._accountService = accountService;
            this._userService = userService;
            this._blogViewService = blogViewService;
            this._commentService = commentService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBlog([FromForm] AddBlogRequest request)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                if (accountId.ToString() == null) return Unauthorized(Message.INVALID_TOKEN);

                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);
                else if (account.Role.Equals(RoleEnum.ADMIN.ToString().ToLower()) == false) return Forbid(Message.FORBIDDEN_CLIENT); 

                
                
                Blog blog = new Blog(account.UserId ,request.Title, request.Introduction, request.Content);
                Blog savedBlog = await this._blogService.Save(blog);
                if (savedBlog == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);
                
                bool savedThumnail = await this._blogService.SaveThumnail(request.Thumnail, savedBlog.Id);
                if (savedThumnail == false) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok(savedBlog);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }

        [HttpPost("media")]
        public async Task<IActionResult> AddMedia([FromForm] AddMediaRequest request)
        {
            if (request.File == null || request.File.Length == 0) return BadRequest();

            string mediaId = await this._blogService.SaveMedia(request.File);
            if (mediaId == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);
            return Ok(new AddMediaResponse(mediaId));
        }

        [HttpGet("media/{id}")]
        public IActionResult GetMedia([FromRoute] string id)
        {
            string path = this._blogService.GetMediaLink(id);
            return File(System.IO.File.OpenRead(path), Constant.contentTypeImage);
        }

        [HttpGet("view/{blogId}")]
        public async Task<IActionResult> AddView([FromRoute(Name = "blogId")] string id)
        {
            Blog blog = await this._blogService.GetById(id);
            if (blog == null) return NotFound("Not found blog");

            Blog savedBlog = await this._blogService.Edit(blog);
            if (savedBlog == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);
            return Ok();
        }

        [HttpGet("show")]
        public async Task<IActionResult> GetBlogs([FromQuery (Name = "page")] int page, [FromQuery(Name = "num")] int num){
            if (page == 0) page = 1;

            int totalItems = (await this._blogService.GetAll()).Count;

            if (num == 0) num = totalItems;

            int totalPages = totalItems / num;
            if (totalItems % num != 0) totalPages++;

            List<Blog> blogs = await this._blogService.Paginate(page, num);
            ShowBlogsResponse response = new ShowBlogsResponse();
            List<BlogInfo> items = new List<BlogInfo>();
            foreach (var blog in blogs){
                User user = await this._userService.GetById(blog.UserId);
                BlogInfo item = new BlogInfo();
                item.BlogId = blog.Id;
                item.Author = user.FirstName + " " + user.LastName;
                item.Title = blog.Title;
                item.NumComments = (await this._commentService.GetAllByBlogId(blog.Id)).Count;
                item.numViews = (await this._blogViewService.GetNumViewsByBlogId(blog.Id));
                item.Introduction = blog.Introduction;
                item.CreatedAt = blog.CreatedAt;
                item.ModifiedAt = blog.ModifiedAt;
                items.Add(item);
            }
            response.blogs = items;
            response.TotalItems = totalItems;
            response.TotalPages = totalPages;
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllBlogs()
        {
            List<Blog> blogs = await this._blogService.GetAll();
            return Ok(blogs);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetBlogById([FromRoute(Name ="id")] string id)
        {
            if (id == null) return BadRequest(Message.INVALID_PATH);

            Blog blog = await this._blogService.GetById(id);
            if (blog == null) return NotFound("Not Found Blog");

            BlogDetail item = new BlogDetail();
            
            User user = await this._userService.GetById(blog.UserId);
            item.Author = user.FirstName + " " + user.LastName;
            item.BlogId = blog.Id;
            item.Title = blog.Title;
            item.Introduction = blog.Introduction;
            item.Content = blog.Content;
            item.NumComments = (await this._commentService.GetAllByBlogId(blog.Id)).Count;
            item.numViews = (await this._blogViewService.GetNumViewsByBlogId(blog.Id));
            item.CreatedAt = blog.CreatedAt;
            item.ModifiedAt = blog.ModifiedAt;

            return Ok(item);
        }

        [HttpGet("thumnail/{id}")]
        public IActionResult GetThumnail([FromRoute] string id)
        {
            string path = this._blogService.GetThumnailLink(id);
            return File(System.IO.File.OpenRead(path), Constant.contentTypeImage);
        }
    }
}