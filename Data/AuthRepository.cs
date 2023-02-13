
using McU.Models;

namespace McU.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<(string? data, bool success, string? message)> Register(User user, string password)
        {
            var existingUser = await _context.User!.SingleOrDefaultAsync(x => x.Username == user.Username);
            if (existingUser != null)
            {
                return (null, false, "Username already exists");
            }
            
            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            await _context.User!.AddAsync(user);
            await _context.SaveChangesAsync();

            return (user.Username, true, null);
        }

        public async Task<(string? data, bool success, string? message)> Login(string username, string password)
        {
            var user = await _context.User!.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null)
            {
                return (null, false, "User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return (null, false, "Incorrect password");
            }

            return (user.Username, true, null);
        }
    }
}