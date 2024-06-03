using graduationProject.Dtos;
using graduationProject.DTOs;
using graduationProject.DTOs.OfferDtos;

namespace graduationProject.Services
{
    public interface IOfferService
    {
        Task<ResultDto> SendOffer(offerDto Offer,string username);
        Task<List<ReturnAcceptedOfferDto>> GetAcceptedOffers(string id);
        Task<List<ReturnOfferDto>> GetOffers(string id);
    }
}
