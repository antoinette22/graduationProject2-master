using graduationProject.DTOs.OfferDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace graduationProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PictureUrl { get; set; }
        public bool Status { get; set; } = true;//true=active,false=disactive
        public string? NationalId { get; set; }
        public string? NationalCard { get; set; }
        public string? RealName { get; set; }
        public string? Qualifications { get; set; } = string.Empty;
        public string? Interests { get; set; } = string.Empty;
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Connection> Connections { get; set; } = new List<Connection>(); // for all users
    }
}
