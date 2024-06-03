﻿using graduationProject.core.DbContext;
using graduationProject.core.Dtos;
using graduationProject.DTOs.OfferDtos;
using graduationProject.DTOs.OffersDtos;
using graduationProject.Models;
using graduationProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace graduationProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Investor")]
    public class offerController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IuserService _userService;
        public offerController(IOfferService offerService, UserManager<ApplicationUser> userManager, ApplicationDbContext context, IuserService userService)
        {
            _offerService = offerService;
            _userManager = userManager;
            _context = context;
            _userService = userService;
        }
        [Authorize(Roles ="Investor")]
        [HttpPost("SendOffer")]
        public async Task<IActionResult> SendOfferAsync([FromForm] offerDto offer)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var result = await _offerService.SendOffer(offer,username);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("something went wrong");

        }
        // Web API Controller
        //[Route("api/images")]


        //    [HttpGet("offer")]
        //    public IActionResult GetOfferImage()
        //    {
        //        // Read the image file from the wwwroot/images folder
        //        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "offer_image.jpg");

        //        // Check if the file exists
        //        if (!System.IO.File.Exists(imagePath))
        //            return NotFound();

        //        // Return the image file
        //        var imageData = System.IO.File.ReadAllBytes(imagePath);
        //        return File(imageData, "image/jpeg");
        //    }


        [Authorize]
        [HttpPost]
        [Route("add Rate")]
        public async Task<IActionResult> RatingUserAsync([FromBody] userRatingDto userRating)
        {

            var user = await _userManager.FindByIdAsync(userRating.UserId.ToString());
            if (user != null)
            {
                if (userRating.ratingValue < 1 || userRating.ratingValue > 5)
                {
                    return BadRequest("rating value must bs bwtween 1 and 5 ");
                }
                var Rating = new UserRating
                {
                    Rate = userRating.ratingValue,
                    UserId = userRating.UserId
                };
                await _context.UserRatings.AddAsync(Rating);
                await _context.SaveChangesAsync();
                return Ok("rate value saved successfully");

            }
            return BadRequest("user not found");
        }
        [Authorize]
        [HttpGet]
        [Route("view Rate")]
        public async Task<IActionResult> getUserRate(string userId)
        {
            try
            {
                var userRating = await _context.UserRatings
                    .Where(r => r.UserId == userId)
                    .Select(r => r.Rate)
                    .ToListAsync();
                if (userRating == null || !userRating.Any())
                    return Ok(0);
                double averageRating = userRating.Average();
                return Ok(averageRating);
            }



            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize]
        [HttpGet("serch users")]

        public async Task<IActionResult> SearchUsers(string userName)

        {
            var result = await _userService.SearchUserProfile(userName);
            return Ok(result);
        }
        [Authorize(Roles ="User")]
        [HttpGet("GetOffers")]

        public async Task<IActionResult> GetOffers()
        {
            var userId = User.FindFirstValue("UserId");
            var result = await _userService.GetOffers(userId);
            if (result == null)
                return BadRequest("Post not found");
            return Ok(result);
        }
        [Authorize(Roles = "User")]
        [HttpGet("GetAcceptedOffers")]

        public async Task<IActionResult> GetAcceptedOffers()
        {
            var userId = User.FindFirstValue("UserId");
            var result = await _userService.GetAcceptedOffers(userId);
            //if (result == null)
            //    return BadRequest("Post not found");
            return Ok(result);
        }

        [Authorize(Roles = "Investor")]
        [HttpGet("GetInvestorAcceptedOffers")]

        public async Task<IActionResult> GetInvestorAcceptedOffers()
        {
            var userId = User.FindFirstValue("UserId");
            var result = await _offerService.GetAcceptedOffers(userId);
            //if (result == null)
            //    return BadRequest("Post not found");
            return Ok(result);
        }

        [Authorize(Roles = "Investor")]
        [HttpGet("GetInvestorOffers")]

        public async Task<IActionResult> GetInvestorOffers()
        {
            var userId = User.FindFirstValue("UserId");
            var result = await _offerService.GetOffers(userId);
            //if (result == null)
            //    return BadRequest("Post not found");
            return Ok(result);
        }
        [Authorize(Roles ="User")]
        [HttpDelete("RefuseOffer/{offerId}")]

        public async Task<IActionResult> RefuseOffer(int offerId)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var result = await _userService.RefuseOffer(offerId,username);
            return Ok(result);
        }
        [Authorize(Roles = "User")]
        [HttpPost("AcceptOffer/{offerId}")]
        public async Task<IActionResult> AcceptOffer(int offerId)
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var result = await _userService.AcceptOffer(offerId,username);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("something went wrong");
        }

    }

}
