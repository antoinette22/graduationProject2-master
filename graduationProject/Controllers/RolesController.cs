using graduationProject.DTOs;
using graduationProject.Models;
using graduationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace graduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(IRoleService roleService, UserManager<ApplicationUser> userManager)
        {
            _roleService = roleService;
            _userManager = userManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            var result = await _roleService.ManageUserRoles(userId);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUserRoles(UserRolesDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _roleService.UpdateUserRoles(dto);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ViewUsers()
        {
            var result = await _roleService.ViewUsers();
            return Ok(result);
        }
    }
}
