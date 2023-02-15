using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using McU.Models;
using Microsoft.IdentityModel.Tokens;

namespace McU.Data{
    public class AuthRepository : IAuthRepository{
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration) {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(string? data, bool success, string? message)> Register(User user, string password) {
            var existingUser = await _context.User!.SingleOrDefaultAsync(x => x.Username == user.Username);
            if (existingUser != null) {
                return (null, false, "Username already exists");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
            await _context.User!.AddAsync(user);
            await _context.SaveChangesAsync();

            return (user.Username, true, null);
        }

        [Obsolete("Obsolete")]
        public async Task<(string? data, bool success, string? message)> Login(string username, string password) {
            var user = await _context.User!.SingleOrDefaultAsync(x => x.Username == username);
            if (user == null) {
                return (null, false, "User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) {
                return (null, false, "Incorrect password");
            }

            var token = CreateToken(user, false);

            return (token, true, null);
        }

        [Obsolete("Obsolete")]
        public string CreateToken(User user, bool rememberMe) {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!);
            if (key.Length * 8 < 128) {
                var newKey = new byte[16];
                Array.Copy(key, newKey, Math.Min(key.Length, newKey.Length));
                key = newKey;
            }
            var signingKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = rememberMe ? DateTime.UtcNow.AddDays(7) : DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



    }
}