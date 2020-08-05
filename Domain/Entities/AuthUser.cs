using System;

namespace Domain.Entities
{
    public class AuthUser
    {
        public string Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
