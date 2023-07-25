using Microsoft.AspNetCore.Mvc;
using server.SRC.Services;
using server.SRC.Models;
using server.SRC.Utils;
using server.SRC.Enums;

namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/category")]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;

        public CategoryController(ICategoryService categoryService, IAccountService accountService)
        {
            this._categoryService = categoryService;
            this._accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized(Message.INVALID_TOKEN);
                if (account.Role.Equals(RoleEnum.ADMIN.ToString().ToLower()) == false) return BadRequest(Message.FORBIDDEN_CLIENT);

                Category savedCategory = await this._categoryService.Save(category);
                if (savedCategory == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok(category);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }
    }
}