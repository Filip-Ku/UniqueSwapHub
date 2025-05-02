using System;

namespace HubApi.DTO
{
    public class UserLoginDto
    {
        public required string email { get; set; }
        public required string password { get; set; }
    }
}
