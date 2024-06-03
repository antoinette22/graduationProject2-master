using graduationProject.DTOs;
using static graduationProject.Services.ProfileService;

namespace graduationProject.Services
{
    public interface IProfileService
    {
        Task<QualificationsInterestsDTO> GetQualificationsInterests(string username);
        Task<QualificationsInterestsDTO> UpdateQualificationsAndInterests(QualificationsInterestsDTO dto, string username);
        Task<ProfileDto> GetProfile(string username);
    }
}
