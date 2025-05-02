using System;

namespace HubApi.DTO
{
    public class UserRegisterDto
    {
        public required string name { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public required string firstName { get; set; }
        public string? lastName { get; set; }
    }
}
