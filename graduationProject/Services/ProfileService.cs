using graduationProject.DTOs;
using graduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Text.Json.Serialization;

namespace graduationProject.Services
{
    public class ProfileService:IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _uploadsPath;

        public ProfileService(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _uploadsPath = Path.Combine(env.WebRootPath, "uploads");

        }

        public async Task<QualificationsInterestsDTO> UpdateQualificationsAndInterests(QualificationsInterestsDTO dto, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            user.Qualifications = dto.Qualifications;
            user.Interests = dto.Interests;
            await _userManager.UpdateAsync(user);

            return new QualificationsInterestsDTO
            {
                Qualifications = user.Qualifications,
                Interests = user.Interests
            };
        }
        public async Task<QualificationsInterestsDTO> GetQualificationsInterests(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return new QualificationsInterestsDTO
            {
                Qualifications = user.Qualifications,
                Interests = user.Interests
            };
        }
        public async Task<ProfileDto> GetProfile(string username)
        {
            
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new ProfileDto
                {
                    IsSuccess = false,
                    Message="User not found"
                };
            }
            var dtoo= new ProfileDto
            {
                IsSuccess=true,
                FirstName=user.FirstName,
                LastName=user.LastName,
                Interests=user.Interests,
                Qualifications=user.Qualifications,
            };
            if (!string.IsNullOrEmpty(user.PictureUrl))
            {
                dtoo.PictureUrl = Path.Combine(_uploadsPath, user.PictureUrl);
            }
            return dtoo;
        }
        public class ProfileDto
        {
            [JsonIgnore]
            public bool IsSuccess { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string? PictureUrl { get; set; }
            public string? Qualifications { get; set; } = string.Empty;
            public string? Interests { get; set; } = string.Empty;
            [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
            public string Message { get; set; }
        }
    }
}
