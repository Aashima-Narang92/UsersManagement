using Demo.Dto;
using Demo.Helper.Interface;
using Demo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IJwtTokenManager _jwtTokenManager;
        public DatabaseContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthController(IJwtTokenManager jwtTokenManager, DatabaseContext dbContext, IConfiguration configuration)
        {
            _jwtTokenManager = jwtTokenManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login([FromBody] LoginDto userCredential)
        {
            if (userCredential != null)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userCredential.UserName && u.Password == userCredential.Password);
                if (user != null)
                {
                    var token = _jwtTokenManager.Authenticate(userCredential.UserName, userCredential.Password);
                    return Ok(token);
                }

            }
            
            //Here we implement the logic for the login. 
            //If the login is successful, you can retrive the
            //
            //if (isLoginSuccess)
            //{
            //    var token = _jwtTokenManager.Authenticate(userCredential.UserName, userCredential.Password);
            //    return Ok(token);
            //}
            return Unauthorized();
        }

        [HttpPost("AddRoles")]
        public async Task<string> AddRoles(Roles roles)
        {
            if (roles == null)
                return "Enter role";

            var roleExists = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Role.ToLower() == roles.Role.ToLower());
            if (roleExists != null)
                return "Roles already added";

            Roles role = new Roles()
            {
                Role = roles.Role
            };

            await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();
            return "Role added";
        }
    }
}
