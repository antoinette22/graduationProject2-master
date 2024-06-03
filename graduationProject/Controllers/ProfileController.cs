using graduationProject.DTOs;
using graduationProject.Models;
using graduationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace graduationProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ProfileService profileService, UserManager<ApplicationUser> userManager)
        {
            _profileService = profileService;
            _userManager = userManager;
        }

        [HttpPut("update-qualifications-interests")]
        public async Task<IActionResult> UpdateQualificationsAndInterests(QualificationsInterestsDTO dto)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var result =await _profileService.UpdateQualificationsAndInterests(dto, username);
            return Ok(result);
        }
        [HttpGet("get-qualifications-interests")]
        public async Task<IActionResult> GetQualificationsAndInterests()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var result =await _profileService.GetQualificationsInterests( username);
            return Ok(result);
        }

        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile(string username=null)
        {
            if(string.IsNullOrEmpty(username))
                username = User.FindFirstValue(ClaimTypes.Name);
            var result =await _profileService.GetQualificationsInterests(username);
            return Ok(result);
        }
    }
}
