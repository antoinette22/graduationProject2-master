using graduationProject.Models;
using System.ComponentModel.DataAnnotations;

namespace graduationProject.DTOs.OfferDtos
{
    public class offer
    {
        public int Id { get; set; }
        //public int PostId { get; set; }
        public Post Post { get; set; }
        //public string NationalcardInvestor { get; set; }
        public double OfferValue { get; set; }
        public double ProfitRate { get; set; }
        public string Description { get; set; }
        //public string NationalIdInvestor { get; set; }
        //public string? NationalIdUser { get; set; }
        //public string? NationalcardUser{ get; set; }
        //public string? SignatureUser { get; set; }
        public bool IsAccepted { get; set; }
        public ApplicationUser Investor { get; set; }
        public ApplicationUser User { get; set; }
    }
}
