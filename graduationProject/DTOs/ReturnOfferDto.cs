using Org.BouncyCastle.Bcpg.Sig;
using System.Text.Json.Serialization;

namespace graduationProject.DTOs
{
    public class ReturnOfferDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int PostId { get; set; }
        public int Id { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string InvestorName { get; set; }
        public double OfferValue { get; set; }
        public double ProfitRate { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public bool IsAccepted { get; set; }
        //public string InvestorCard { get; set; }
        //public string InvestorNationalId { get; set; }
    }
    public class ReturnAcceptedOfferDto
    {
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingDefault)]
        public int PostId { get; set; }
        public int Id { get; set; }
        public double OfferValue { get; set; }
        public double ProfitRate { get; set; }
        public string Description { get; set; }
        public string UserNationalId { get; set; }
        public string InvestorRealName { get; set; }
        public string UserRealName { get; set; }
        public string InvestorNationalId { get; set; }
        public string UserNationalCard { get; set; }
        public string InvestorNationalCard { get; set; }
        [JsonIgnore]
        public bool IsAccepted { get; set; }
        //public string InvestorCard { get; set; }
        //public string InvestorNationalId { get; set; }
    }
}