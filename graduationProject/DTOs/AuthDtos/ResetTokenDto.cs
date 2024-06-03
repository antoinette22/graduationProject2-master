﻿using System.Text.Json.Serialization;

namespace graduationProject.DTOs.AuthDtos
{
    public class ResetTokenDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public bool IsSuccess { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[] Errors { get; set; }
        public ResetTokenDto()
        {
            Errors = null;
            Message = null;
        }

    }
}
