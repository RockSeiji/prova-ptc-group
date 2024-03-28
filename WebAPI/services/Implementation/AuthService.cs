using Cast.Models;
using Cast.services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cast.services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly CastDBContext _context;

        public AuthService(IConfiguration config, CastDBContext context)
        {
            _config = config;
            _context = context;
        }

        public void Register(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public string Login(Login login)
        {
            string token = null;
            var user = Authenticate(login);
            if (user != null)
               token = GenerateToken(user);

            return token;
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("username",user.Username),
                new Claim("role", user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //To authenticate user
        private User? Authenticate(Login login)
        {
            var currentUser = _context.Users.FirstOrDefault(x => x.Username.ToLower() ==
                login.Username.ToLower() && x.Password == login.Password);
            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }

    }
}
