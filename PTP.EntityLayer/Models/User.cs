using PTP.EntityLayer.Abstractions;

namespace PTP.EntityLayer.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = "User";
    }
}
