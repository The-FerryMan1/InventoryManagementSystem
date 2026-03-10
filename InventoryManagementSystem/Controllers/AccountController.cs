using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<IdentityUser> signInManager,
        ILogger<AccountController> logger
    ) : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly ILogger<AccountController> _logger = logger;


        #region --End points--
        //register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest register )
        {
            var user = new IdentityUser
            {
               UserName = register.Email,
               Email = register.Email
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if(!result.Succeeded) return BadRequest(result.Errors);

            if(!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            await _userManager.AddToRoleAsync(user, "User");

            _logger.LogInformation("New user has been made");

            return Ok(new {message = "Account created successfully"});

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
           
            var result = await _signInManager.PasswordSignInAsync(
                login.Email,
                login.Password,
                isPersistent: true,
                lockoutOnFailure: true
            );
             _logger.LogInformation("Login attempt for user: {Email}", login.Email);
            if (!result.Succeeded)
            {
                _logger.LogWarning("Login attept failed for user: {Email}", login.Email);
                return Unauthorized("Invalid email or password");
            }
            return Ok(new {message = "Login Successfully"});
        }


        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user is null) return Unauthorized();

            var role = await _userManager.GetRolesAsync(user);

            return Ok(new
                {
                user.Id,
                user.Email,
                Role = role[0],
                }
            );

        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {   

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User loggoed out");
            return Ok(new {message = "Logged out successfully"});
        }

        #endregion


    }
}
