using Cast.Models;
using Cast.services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cast.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] User user)
        {
            _authService.Register(user);
            return Ok("Registro salvo com sucesso.");
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Login login)
        {
            var token = _authService.Login(login);
            if (token != null)
                return Ok(token);
            else
                return NotFound("user not found");
        }
    }
}
