using graduationProject.DTOs.OfferDtos;

namespace graduationProject.DTOs
{
    public class GetOffersDto
    {
        public int PostId { get; set; }
        public string Content { get; set; }
        //  public string Image { get; set; }
        //public double Rrice { get; set; }
        //public double ProfitRate { get; set; }
        //public string Description { get; set; }
        //public string NationalId { get; set; }
        public List<ReturnOfferDto> Offers { get; set; }=new List<ReturnOfferDto>();
    }
}
