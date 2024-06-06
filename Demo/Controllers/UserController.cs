using Demo.Dto;
using Demo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Demo.Controllers
{
    [Authorize()]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public DatabaseContext _dbContext;
        public UserController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetName")]
        public IActionResult GetName()
        {
            return Ok(new List<string> { "Adam", "Robert" });
        }

        [HttpPost("Signup")]
        public async Task<string> Signup(Users userDetails)
        {
            Users user = new Users()
            {
                CreationDate = DateTime.Now,
                Email = userDetails.Email,
                Mobile = userDetails.Mobile,
                Password = userDetails.Password,
                UserName = userDetails.UserName
            };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return "User created";
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(UserRoleDto user)
        {
            UserRoles userRole = new UserRoles();
            var userId = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName);//.Result.Id;
            var RoleId = await _dbContext.Roles.FirstOrDefaultAsync(u => u.Role == user.Role);//.Result.Id;
            if(userId == null)
            {
                return BadRequest("No user found");
            }

            if (RoleId == null)
                return BadRequest("No role found");

            if (user.UserName != null)
            {

                userRole.UserId = userId.Id;
                userRole.RoleId = RoleId.Id;
                await _dbContext.UserRoles.AddAsync(userRole);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            return Unauthorized();

        }

        [Authorize]
        [HttpGet("GetUsers")]
        public async Task<IList<Users>> GetUsers()
        {
            IList<Users> lstUsers = new List<Users>();
            lstUsers = (from r in _dbContext.Users
                        select r).ToList();

            return lstUsers;
        }
    }
}
