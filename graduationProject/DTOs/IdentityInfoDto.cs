using System.ComponentModel.DataAnnotations;

namespace graduationProject.DTOs
{
    public class IdentityInfoDto
    {
        [Required]
        public string NationalId { get; set; }
        [Required]
        public string RealName { get; set; }
        [Required]
        public IFormFile NationalCard { get; set; }
    }
}
