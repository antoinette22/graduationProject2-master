using graduationProject.Dtos;
using graduationProject.DTOs;
using graduationProject.DTOs.OffersDtos;

namespace graduationProject.Services
{
    public interface IuserService
    {
        Task<searchDto> SearchUserProfile(string userName);
        //Task<List<GetOffersDto>> GetOffers(string id);
        //Task<List<GetAcceptedOffersDto>> GetAcceptedOffers(string id);
        Task<List<ReturnAcceptedOfferDto>> GetAcceptedOffers(string id);
        Task<List<ReturnOfferDto>> GetOffers(string id);
        Task<ResultDto> RefuseOffer(int offerId, string username);
        Task<ResultDto> AcceptOffer(int offerId,string username);
    }
}
