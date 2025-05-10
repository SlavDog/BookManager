using System.ComponentModel.DataAnnotations;

namespace BookManagerApp.DataAccessLayer
{
    public class User
    {
        public User() { }
        public User(string username, string passwordHash, string salt)
        {
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
        }
        [Key]
        public string? Username { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        [Required]
        public string? Salt { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
