using graduationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Security.Claims;

namespace graduationProject.Controllers
{
    [Authorize(Roles ="User,Investor")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService connectionService;

        public ConnectionController(IConnectionService connectionService)
        {
            this.connectionService = connectionService;
        }
        [HttpPost("Connect/{username}")]
        public async Task<IActionResult> Connect(string username)
        {
            var senderId = User.FindFirstValue("UserId");
            var result = await connectionService.Connect(senderId,username);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AcceptConnection/{id}")]
        public async Task<IActionResult> AcceptConnection(int id)
        {
            var senderId = User.FindFirstValue("UserId");
            var result = await connectionService.AcceptConnection(id, senderId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("RefuseConnection/{id}")]
        public async Task<IActionResult> RefuseConnection(int id)
        {
            var senderId = User.FindFirstValue("UserId");
            var result = await connectionService.refuseConnection(id, senderId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("Connections")]
        public async Task<IActionResult> Connections()
        {
            var userId = User.FindFirstValue("UserId");
            var result = await connectionService.Connections(userId);
            return Ok(result);
        }
        [HttpGet("ReceivedConnections")]
        public async Task<IActionResult> ReceivedConnections()
        {
            var userId = User.FindFirstValue("UserId");
            var result = await connectionService.ReceivedConnections(userId);
            return Ok(result);
        }
        [HttpGet("RequestedConnections")]
        public async Task<IActionResult> RequestedConnections()
        {
            var userId = User.FindFirstValue("UserId");
            var result = await connectionService.RequestedConnections(userId);
            return Ok(result);
        }
    }
}
