using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using BookManagerApp.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BookManagerApp.Managers
{
    class UserManager
    {
        public static async Task<int> AddUser(string? username, string? password)
        {
            if (username == null || password == null 
                || username == "" || password == "") 
            {
                return -2;
            }
            using var context = new AppDbContext();
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                return -1;
            }
            string salt = GenerateSalt();
            context.Users.Add(new User(username, HashPassword(password, salt), salt));
            await context.SaveChangesAsync();
            return 0;
        }

        public static async Task<User> GetUser(string username)
        {
            using var context = new AppDbContext();
            var user = await context.Users
                .Include(u => u.Books)
                .FirstOrDefaultAsync(u => u.Username == username);
            Debug.Assert(user != null, "User not found");
            return user;
        }

        public static async Task<bool> CheckCredentialsUser(string? username, string? password)
        {
            if (username == null || password == null
                || username == "" || password == "")
            {
                return false;
            }
            using var context = new AppDbContext();
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
            return (user != null && user.Salt != null && user.PasswordHash == HashPassword(password, user.Salt));
        }

        public static string GenerateSalt(int size = 16)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] saltBytes = new byte[size];
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            using var sha256 = SHA256.Create();
            string combined = salt + password;
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));

            // Convert to hex string
            string[] result = new string[hashBytes.Length];
            for (int i = 0; i < hashBytes.Length; ++i)
            {
                result[i] = hashBytes[i].ToString("x2");
            }

            return string.Join("", result);
        }
    }
}
