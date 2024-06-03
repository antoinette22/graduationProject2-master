using graduationProject.DTOs;
using graduationProject.DTOs.OfferDtos;
using graduationProject.Models;

namespace graduationProject.Mapping
{
    public static class GetOfferedUserDtoMap
    {
        //public static async Task<List<GetOffersDto>> MapToGetOfferedUserDtoMap(this IEnumerable<Post> posts)
        //{
        //    var response = new List<GetOffersDto>();

        //    foreach (var post in posts)
        //    {
        //        var dto = new GetOffersDto()
        //        {
        //            PostId = post.Id,
        //            Content = post.Content,
        //            //Rrice = post.Offers.FirstOrDefault()?.Price ?? 0, // handle if Offers are null or empty
        //            //Description = post.Offers.FirstOrDefault()?.Description,
        //            //NationalId = post.Offers.FirstOrDefault()?.NationalIdInvestor,
        //            //ProfitRate = post.Offers.FirstOrDefault()?.ProfitRate ?? 0 // handle if Offers are null or empty
        //            Offers = post.Offers.Select(offer => new ReturnOfferDto
        //            {
        //                Id=offer.Id,
        //                InvestorCard=offer.NationalcardInvestor,
        //                Price = offer.Price,
        //                Description = offer.Description,
        //                InvestorNationalId = offer.NationalIdInvestor,
        //                ProfitRate = offer.ProfitRate
        //            }).ToList()
        //        };
        //        response.Add(dto);
        //    }

        //    return response;
        //}
    }
}
