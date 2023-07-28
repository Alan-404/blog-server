using Microsoft.AspNetCore.Mvc;
using server.SRC.Services;
using server.SRC.Models;
using server.SRC.Utils;
using server.SRC.DTOs.Requests;
using server.SRC.DTOs.Responses;

namespace server.SRC.Controllers
{
    [ApiController]
    [Route("server/room")]
    public class RoomController: ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public RoomController(IRoomService roomService, IUserService userService, IAccountService accountService)
        {
            this._roomService  = roomService;
            this._userService = userService;
            this._accountService = accountService;
        }

        [HttpGet("connect/{receiverId}")]
        public async Task<IActionResult> ConnectToRoom([FromRoute (Name = "receiverId")] string receiverId)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {   
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized();

                Room room = await this._roomService.GetByTwoUsers(account.UserId, receiverId);
                if (room == null)
                {
                    room = await this._roomService.GetByTwoUsers(receiverId, account.UserId);
                    if (room == null)
                    {
                        Room savedRoom = await this._roomService.Save(new Room(account.UserId, receiverId));
                        if (savedRoom == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                        return Ok(savedRoom);
                    }
                    return Ok(room);
                }
                return Ok(room);
            }
            else return Unauthorized();
        }

        [HttpGet("chat/{roomId}")]
        public async Task<IActionResult> GetRoomById([FromRoute(Name = "roomId")] string roomId)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if (account == null) return Unauthorized();

                User sender = await this._userService.GetById(account.UserId);
                Room room = await this._roomService.GetById(roomId);
                if (room == null) return BadRequest();
      
                string receiverId = "";

                
                if (sender.Id.Equals(room.User1) == false) receiverId = room.User1;
                else receiverId = room.User2;

                User receiver = await this._userService.GetById(receiverId);

                return Ok(new RoomInfo(roomId, sender, receiver));

            
                
            }
            else return Unauthorized();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
        {
            if (HttpContext.Request.Headers.TryGetValue(RequestHeader.AUTH_HEADER, out var accountId))
            {
                Account account = await this._accountService.GetById(accountId);
                if(account == null) return Unauthorized(Message.INVALID_TOKEN);

                User receiver = await this._userService.GetById(request.Receiver);
                if (receiver == null || account.UserId == receiver.Id) return BadRequest(Message.INVALID_REQUEST);

                Room room = new Room(account.UserId, receiver.Id);
                Room savedRoom = await this._roomService.Save(room);

                if (savedRoom == null) return StatusCode(500, Message.INTERNAL_ERROR_SERVER);

                return Ok(savedRoom);
            }
            else return Unauthorized(Message.INVALID_TOKEN);
        }
    }
}