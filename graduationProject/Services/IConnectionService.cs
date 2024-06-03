using graduationProject.Dtos;
using graduationProject.DTOs;

namespace graduationProject.Services
{
    public interface IConnectionService
    {
        Task<ResultDto> Connect(string SenderId, string ReceiverId);
        Task<ResultDto> refuseConnection(int id, string userId);
        Task<ResultDto> AcceptConnection(int id, string userId);
        Task<ConnectionsDto> ReceivedConnections(string userId);
        Task<ConnectionsDto> Connections(string userId);
        Task<ConnectionsDto> RequestedConnections(string userId);

    }
}
