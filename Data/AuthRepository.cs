using McU.Data;
using McU.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            user.Password = password;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return (user.Id.ToString(), true, null);
        }

        public async Task<(string? data, bool success, string? message)> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                return (null, false, "User not found.");
            }

            if (user.Password != password)
            {
                return (null, false, "Wrong password.");
            }

            return (user.Id.ToString(), true, null);
        }
    }
}