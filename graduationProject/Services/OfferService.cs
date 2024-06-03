using graduationProject.core.DbContext;
using graduationProject.Dtos;
using graduationProject.DTOs;
using graduationProject.DTOs.OfferDtos;
using graduationProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace graduationProject.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _context;
        private List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };
        private long maxAllowedSize = 1048576;
        private readonly string _uploadsPath;
        private readonly UserManager<ApplicationUser> _userManager;
        public OfferService(ApplicationDbContext context, IWebHostEnvironment env, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _uploadsPath = Path.Combine(env.WebRootPath, "cardId");
            _userManager = userManager;
        }
        public async Task<ResultDto> SendOffer(offerDto Offer,string username)
        {
            var post = await _context.Posts.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id==Offer.postId);
            if (post == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Errors = new string[] { "Post not found" }
                };
            }
            var user = post.User;
            var investor = await _userManager.FindByNameAsync(username);
            if(string.IsNullOrEmpty(investor.NationalCard) || string.IsNullOrEmpty(investor.NationalId) || string.IsNullOrEmpty(investor.RealName))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Errors = new string[] { "Please fill your identity information and try again" }
                };
            }
            var offer = new offer
            {
                User=user,
                Investor=investor,
                ProfitRate = Offer.ProfitRate,
                Description = Offer.Description,
                OfferValue=Offer.OfferValue,
                Post=post
            };
            post.IsHaveOffer = true;
            _context.Update(post);
            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "Offer was sent successfully"
            };
        }
        public async Task<List<ReturnOfferDto>> GetOffers(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.Posts)
                .ThenInclude(p => p.Offers)
                .ThenInclude(p => p.Investor)
                .FirstOrDefaultAsync(x => x.Id == id);

            var offers = _context.Offers.Include(p => p.Post).Include(c => c.User).Include(c => c.Investor).Where(x => x.Investor == user);
            var returnedOffers = new List<ReturnOfferDto>();

            foreach (var offer in offers)
            {
                if (!offer.IsAccepted)
                {
                    var offerdto = new ReturnOfferDto
                    {
                        PostId = offer.Post.Id,
                        Id = offer.Id,
                        Description = offer.Description,
                        OfferValue = offer.OfferValue,
                        ProfitRate = offer.ProfitRate,
                    };
                    returnedOffers.Add(offerdto);
                }
            }
            return returnedOffers;
        }
        public async Task<List<ReturnAcceptedOfferDto>> GetAcceptedOffers(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.Posts)
                .ThenInclude(p => p.Offers)
                .ThenInclude(p => p.Investor)
                .FirstOrDefaultAsync(x => x.Id == id);

            var offers = _context.Offers.Include(p=>p.Post).Include(c=>c.User).Include(c=>c.Investor).Where(x => x.Investor == user);


            var returnedOffers = new List<ReturnAcceptedOfferDto>();

            foreach(var offer in offers)
            {
                if (offer.IsAccepted)
                {
                    var offerdto = new ReturnAcceptedOfferDto
                    {
                        PostId=offer.Post.Id,
                        Id = offer.Id,
                        Description = offer.Description,
                        OfferValue = offer.OfferValue,
                        ProfitRate = offer.ProfitRate,
                        InvestorNationalCard = Path.Combine(_uploadsPath, offer.Investor.NationalCard),
                        InvestorNationalId = offer.Investor.NationalId,
                        UserNationalCard = Path.Combine(_uploadsPath, offer.User.NationalCard),
                        UserNationalId = offer.User.NationalId,
                        InvestorRealName = offer.Investor.RealName,
                        UserRealName = offer.User.RealName
                    };
                    returnedOffers.Add(offerdto);
                }
            }
            return returnedOffers;

            //var posts = new List<Post>();
            //foreach(var offer in offers)
            //{
            //    if (!posts.Contains(offer.Post))
            //        posts.Add(offer.Post);
            //}
            //if (user == null|| posts==null)
            //    return null; // or handle the situation as appropriate
            //// Filter the posts that have offers
            //var response = new List<GetAcceptedOffersDto>();
            //foreach (var post in posts)
            //{
            //    var getoffersdto = new GetAcceptedOffersDto();
            //    getoffersdto.PostId = post.Id;
            //    getoffersdto.Content = post.Content;
            //    foreach (var offer in post.Offers)
            //    {
            //        if (offer.IsAccepted && offer.Investor == user)
            //        {
            //            var offerdto = new ReturnAcceptedOfferDto
            //            {
            //                Id = offer.Id,
            //                Description = offer.Description,
            //                OfferValue = offer.OfferValue,
            //                ProfitRate = offer.ProfitRate,
            //                InvestorNationalCard = Path.Combine(_uploadsPath, offer.Investor.NationalCard),
            //                InvestorNationalId = offer.Investor.NationalId,
            //                UserNationalCard = Path.Combine(_uploadsPath, offer.User.NationalCard),
            //                UserNationalId = offer.User.NationalId,
            //                InvestorRealName = offer.Investor.RealName,
            //                UserRealName = offer.User.RealName
            //            };
            //            getoffersdto.Offers.Add(offerdto);
            //        }
            //    }
            //    if (getoffersdto.Offers.Any(x => x.IsAccepted))
            //    {
            //        response.Add(getoffersdto);
            //    }
            //}
            ////var response = await postsWithOffers.MapToGetOfferedUserDtoMap();
            //return response;
        }
    }
}
