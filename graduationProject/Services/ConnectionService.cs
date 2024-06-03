using graduationProject.core.DbContext;
using graduationProject.Dtos;
using graduationProject.DTOs;
using graduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Connection = graduationProject.Models.Connection;

namespace graduationProject.Services
{
    public class ConnectionService: IConnectionService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConnectionService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<ConnectionsDto> RequestedConnections(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var connections = await _context.Connections.Include(x=>x.Receiver).Include(x=>x.Sender).Where(c => c.Sender == user && !c.IsConnected).ToListAsync();
            var connectionsdto = new List<ConnectionDto>();
            foreach (var connection in connections)
            {
                var connect = new ConnectionDto
                {
                    ConnectionId = connection.Id,
                    UserName = connection.Receiver.UserName
                };
                connectionsdto.Add(connect);
            }
            var dto = new ConnectionsDto
            {
                Count = connections.Count,
                Connections = connectionsdto
            };
            return dto;
        }
        public async Task<ConnectionsDto> Connections(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var connections = await _context.Connections.Include(x => x.Receiver).Include(x => x.Sender).Where(c => (c.Receiver == user||c.Sender==user) &&( c.IsConnected)).ToListAsync();
            var connectionsdto = new List<ConnectionDto>();
            foreach (var connection in connections)
            {
                var connect = new ConnectionDto();
                if (connection.Receiver==user)
                {
                    connect.ConnectionId = connection.Id;
                    connect.UserName = connection.Sender.UserName;
                }
                else
                {
                    connect.ConnectionId = connection.Id;
                    connect.UserName = connection.Receiver.UserName;
                }
                connectionsdto.Add(connect);
            }
            var dto = new ConnectionsDto
            {
                Count = connections.Count,
                Connections = connectionsdto
            };
            return dto;
        }
        public async Task<ConnectionsDto> ReceivedConnections(string userId)
        {
           var user = await _userManager.FindByIdAsync(userId);
           var connections = await _context.Connections.Include(x => x.Receiver).Include(x => x.Sender).Where(c=>c.Receiver==user&&!c.IsConnected).ToListAsync();
           var connectionsdto = new List<ConnectionDto>();
           foreach (var connection in connections)
           {
                var connect= new ConnectionDto
                {
                    ConnectionId=connection.Id,
                    UserName=connection.Sender.UserName
                };
                connectionsdto.Add(connect);
           }
           var dto = new ConnectionsDto
           {
               Count = connections.Count,
               Connections = connectionsdto
           };
            return dto;
        }
        public async Task<ResultDto> AcceptConnection(int id,string userId)
        {
            var user =await _userManager.FindByIdAsync(userId);
            var connection = await _context.Connections.FirstOrDefaultAsync(c => (c.Id == id)&& (c.Receiver==user ||c.Sender==user));
            if (connection == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Errors = new string[] { "Connection not found" }
                };
            }
            if(connection.IsConnected)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Errors = new string[] { "Already Connected" }
                };
            }
            connection.IsConnected = true;
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "You are connected now"
            };
        }
        public async Task<ResultDto> refuseConnection(int id,string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var connection = await _context.Connections.FirstOrDefaultAsync(c => (c.Id == id) && (c.Receiver == user || c.Sender == user));
            if (connection == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Errors = new string[] { "Connection not found" }
                };
            }
            if(connection.IsConnected)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Errors = new string[] { "Already Connected" }
                };
            }
            _context.Connections.Remove(connection);
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "connection was refused"
            };
        }
        public async Task<ResultDto> Connect(string SenderId,string username)
        {
            var sender = await _userManager.FindByIdAsync(SenderId);
            var users = await _userManager.Users.ToListAsync();
            var receiver = await _userManager.FindByNameAsync(username);
            //var receiver = users.FirstOrDefault(x=>x.Id == ReceiverId);
            if (receiver == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Errors = new string[] { "Receiver not found" }
                };
            }
            var currentConnection = await _context.Connections.FirstOrDefaultAsync(x=>(x.Sender==sender&&x.Receiver==receiver)||(x.Sender==receiver&&x.Receiver==sender));
            if(currentConnection !=null)
            {
                if (currentConnection.IsConnected)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Errors = new string[] { "Already connected" }
                    };
                }
                return new ResultDto
                {
                    IsSuccess = false,
                    Errors = new string[] { "Already requested" }
                };
            }
            var connection = new Connection
            {
                Sender= sender,
                Receiver= receiver,
            };
            await _context.Connections.AddAsync(connection);
            await _context.SaveChangesAsync();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "Request was sent successfully"
            };
        }
    }
}
