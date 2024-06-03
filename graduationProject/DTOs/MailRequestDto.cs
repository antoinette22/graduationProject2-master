﻿using System.ComponentModel.DataAnnotations;

namespace graduationProject.DTOs
{
    public class MailRequestDto
    {
        [Required]
        public string MailTo { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Content { get; set; }
        public IList<IFormFile> Attachments { get; set; }
    }
}
