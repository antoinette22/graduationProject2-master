using graduationProject.core.DbContext;
using graduationProject.Dtos;
using graduationProject.DTOs;
using graduationProject.DTOs.OfferDtos;
using graduationProject.DTOs.OffersDtos;
using graduationProject.Mapping;
using graduationProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace graduationProject.Services
{
    public class userService : IuserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private List<string> _allowedExtensions = new List<string> { ".jpg", ".png" };
        private long maxAllowedSize = 1048576;
        private readonly string _uploadsPath;
        private readonly IMailingService _mailingService;
        public userService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment env, IMailingService mailingService)
        {
            _context = context;
            _userManager = userManager;
            _uploadsPath = Path.Combine(env.WebRootPath, "cardId");
            _mailingService = mailingService;
        }

        public async Task<List<ReturnOfferDto>> GetOffers(string id)
        {
            var user = await _userManager.Users
                .Include(u => u.Posts)
                .ThenInclude(p => p.Offers)
                .ThenInclude(p => p.Investor)
                .FirstOrDefaultAsync(x => x.Id == id);

            var offers = _context.Offers.Include(p => p.Post).Include(c => c.User).Include(c => c.Investor).Where(x => x.User == user);
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
                        InvestorName=offer.Investor.RealName
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

            var offers = _context.Offers.Include(p => p.Post).Include(c => c.User).Include(c => c.Investor).Where(x => x.User == user);


            var returnedOffers = new List<ReturnAcceptedOfferDto>();

            foreach (var offer in offers)
            {
                if (offer.IsAccepted)
                {
                    var offerdto = new ReturnAcceptedOfferDto
                    {
                        PostId = offer.Post.Id,
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


        //public async Task<List<GetOffersDto>> GetOffers(string id)
        //{
        //    var user = await _userManager.Users
        //        .Include(u => u.Posts)
        //        .ThenInclude(p => p.Offers).ThenInclude(p => p.Investor)
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //    //FirstOrDefaultAsync
        //    if (user == null || user.Posts == null)
        //        return null; // or handle the situation as appropriate

        //    // Filter the posts that have offers
        //    var postsWithOffers = user.Posts.Where(p => p.IsHaveOffer);
        //    var response = new List<GetOffersDto>();
        //    foreach (var post in postsWithOffers)
        //    {
        //        var getoffersdto = new GetOffersDto();
        //        getoffersdto.PostId = post.Id;
        //        getoffersdto.Content = post.Content;
        //        foreach (var offer in post.Offers)
        //        {
        //            if (!offer.IsAccepted)
        //            {
        //                var offerdto = new ReturnOfferDto
        //                {
        //                    Id = offer.Id,
        //                    Description = offer.Description,
        //                    OfferValue = offer.OfferValue,
        //                    ProfitRate = offer.ProfitRate,
        //                    InvestorName = offer.Investor.RealName
        //                };
        //                getoffersdto.Offers.Add(offerdto);
        //            }

        //        }
        //        if (getoffersdto.Offers.Any(x => !x.IsAccepted))
        //        {
        //            response.Add(getoffersdto);
        //        }
        //    }
        //    //var response = await postsWithOffers.MapToGetOfferedUserDtoMap();
        //    return response;
        //}
        //public async Task<List<GetAcceptedOffersDto>> GetAcceptedOffers(string id)
        //{
        //    var user = await _userManager.Users
        //        .Include(u => u.Posts)
        //        .ThenInclude(p => p.Offers)
        //        .ThenInclude(p=>p.Investor)
        //        .FirstOrDefaultAsync(x => x.Id == id);
        //    //FirstOrDefaultAsync
        //    if (user == null || user.Posts == null)
        //        return null; // or handle the situation as appropriate

        //    // Filter the posts that have offers
        //    var postsWithOffers = user.Posts.Where(p => p.IsHaveOffer);
        //    var response = new List<GetAcceptedOffersDto>();
        //    foreach (var post in postsWithOffers)
        //    {
        //        var getoffersdto = new GetAcceptedOffersDto();
        //        getoffersdto.PostId = post.Id;
        //        getoffersdto.Content = post.Content;
        //        foreach (var offer in post.Offers)
        //        {
        //            if (offer.IsAccepted)
        //            {
        //                var offerdto = new ReturnAcceptedOfferDto
        //                {
        //                    Id = offer.Id,
        //                    Description = offer.Description,
        //                    OfferValue = offer.OfferValue,
        //                    ProfitRate = offer.ProfitRate,
        //                    InvestorNationalCard=Path.Combine(_uploadsPath,offer.Investor.NationalCard),
        //                    InvestorNationalId=offer.Investor.NationalId,
        //                    UserNationalCard= Path.Combine(_uploadsPath, offer.User.NationalCard),
        //                    UserNationalId=offer.User.NationalId,
        //                    InvestorRealName=offer.Investor.RealName,
        //                    UserRealName=offer.User.RealName
        //                };
        //                getoffersdto.Offers.Add(offerdto);
        //            }
        //        }
        //        if (getoffersdto.Offers.Any(x => x.IsAccepted))
        //        {
        //            response.Add(getoffersdto);
        //        }
        //    }
        //    //var response = await postsWithOffers.MapToGetOfferedUserDtoMap();
        //    return response;
        //}

        public async Task<ResultDto> RefuseOffer(int offerId,string username)
        {
            //var offer = await _context.Offers.FindAsync(id);
            var offer = await _context.Offers.Include(c=>c.Post).Include(c => c.Investor).Include(o => o.User).FirstOrDefaultAsync(x => x.Id == offerId);
            var user = await _userManager.FindByNameAsync(username);
            if (offer == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "offer not found"
                };
            }
            if (offer.User != user)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "offer not found"
                };
            }
            //   var post = _context.Posts.FirstOrDefault(o => o.Id==offer.PostId);
            offer.Post.IsHaveOffer = false;
            _context.Offers.Remove(offer);
            _context.Posts.Update(offer.Post);
            await _context.SaveChangesAsync();
            var RefusedEmail = $"Dear {offer.Investor.FirstName} {offer.Investor.LastName}," +
                $"" +
                $"We trust this message finds you in good spirits. Regrettably , we must inform you that your investment offer has not been accepted by the project owner '{offer.User.FirstName} {offer.User.LastName}' at this time." +
                $"" +
                $"While we value your interest and participationn,the project owner has opted to pursue a different investment path. we sincerely  appreciate your consideration and hope for future opportunities to collaborate." +
                $"" +
                $"Thank you for your understanding." +
                $"" +
                $"Best Regards," +
                $"Linka Team";
            try
            {
                await _mailingService.SendEmailAsync(offer.Investor.Email, "Decision on Your Investment Offer", RefusedEmail);
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "something went wrong , please try again later"
                };
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "offer removed Successfuly"
            };
        }

        public async Task<ResultDto> AcceptOffer(int offerId,string username)
        {
            var offer = await _context.Offers.Include(c => c.Investor).Include(o => o.User).FirstOrDefaultAsync(x => x.Id == offerId);
            var user =await _userManager.FindByNameAsync(username);
            if (offer == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "offer not found"
                };
            }
            if (offer.User != user)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "offer not found"
                };
            }
            if (string.IsNullOrEmpty(user.NationalCard) || string.IsNullOrEmpty(user.NationalId) || string.IsNullOrEmpty(user.RealName))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "Please fill your identity information and try again"
                };
            }
            offer.IsAccepted = true;
            var investorAcceptedEmail= $"" +
                $"Dear {offer.Investor.FirstName} {offer.Investor.LastName}," +
                $"" +
                $"We hope this message finds you well, We are pleased to inform you that your investment offer has been accepted by the project owner '{offer.User.FirstName} {offer.User.LastName}'" +
                $"" +
                $"Your support and interest in this projet are greatly appreciated. We believe your investment will contribute significantly to its success." +
                $"" +
                $"Thank you for your commitment and confidence. we look forward to a fruitful collaboration between you and the project owner." +
                $"You can connect with the project owner via his email:'{offer.User.Email}' " +
                $"" +
                $"Best regards," +
                $"Linka Team";

            var UserAcceptedEmail = $"Dear {offer.User.FirstName} {offer.User.LastName}," +
                $"" +
                $"Congratulation! We are pleased to inform you that your acceptance of the investment offer from '{offer.Investor.FirstName} {offer.Investor.LastName}' has been successfully recorded." +
                $"" +
                $"Your decision marks an important milestone in the progress of your project, and we are excited to see how this partnership unfolds." +
                $"" +
                $"Thank you for your trust in our platform and choosing to move forward with '{offer.Investor.FirstName} {offer.Investor.LastName}'. We wish you all the best in your collaboration." +
                $"You can connect with the investor via his email:'{offer.Investor.Email}' " +
                $"" +
                $"Warm regards," +
                $"Linka Team";
            try
            {
                await _mailingService.SendEmailAsync(offer.Investor.Email, "Your Offer Has Been Accepted", investorAcceptedEmail);
                await _mailingService.SendEmailAsync(offer.User.Email, "Acceptence of Investment Offer",UserAcceptedEmail);
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "something went wrong,please try again later"
                };
            }

            // Save the changes to the database
            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "Offer accepted successfully"
            };
        }
            public async Task<searchDto> SearchUserProfile(string userName)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(a => a.UserName.Contains(userName.Trim()));
                if (user != null)
                {
                    return new searchDto

                    {
                        userName = user.UserName,
                    };
                }
                else return new searchDto

                {
                    userName = null,
                };

            }
        }
}

