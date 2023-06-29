using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using TaskManagerCourse.Api.Models;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Api.Models.Services;

namespace TaskManagerCourse.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly UserService _usersService;
        public AccountController(ApplicationContext db)
        {
            _db = db;
        }
        [HttpGet("info")]
        public IActionResult GetCurrentUserInfo()
        {
            string username = HttpContext.User.Identity.Name;
            var user = _db.Users.FirstOrDefault(x => x.Email == username);

            if (user != null)
                return Ok(user.ToDto());
            return NotFound();
        }
        [HttpPost("token")]
        public IActionResult GetToken()
        {
            var userData = _usersService.GetUserLoginPassFromBasicAuth(Request);
            var login = userData.Item1;
            var password = userData.Item2;
            var identity = _usersService.GetIdentity(login, password);
            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return Ok(response);
        }
    }
}
