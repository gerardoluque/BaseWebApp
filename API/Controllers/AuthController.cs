using System.Threading.Tasks;
using API.DTOs;
using API.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AuthController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new Usuario
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Nombre = registerDto.Nombre,
                PrimerApellido = registerDto.PrimerApellido,
                SegundoApellido = registerDto.SegundoApellido,
                GrupoId = registerDto.GrupoId,
                RolId = registerDto.RolId,
                FechaCreacion = DateTime.UtcNow,
                FechaUltimaActualizacion = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                // Send confirmation email logic here

                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            if (await _userManager.GetTwoFactorEnabledAsync(user))
            {
                return Ok(new { Requires2FA = true });
            }

            return Ok();
        }

        [HttpPost("enable-2fa")]
        public async Task<IActionResult> Enable2FA([FromBody] Enable2FADto enable2FADto)
        {
            var user = await _userManager.FindByEmailAsync(enable2FADto.Email);
            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Authenticator");
            // Send token to user via email or SMS

            return Ok(new { Token = token });
        }

        [HttpPost("verify-2fa")]
        public async Task<IActionResult> Verify2FA([FromBody] Verify2FADto verify2FADto)
        {
            var user = await _userManager.FindByEmailAsync(verify2FADto.Email);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Authenticator", verify2FADto.Token);

            if (!result)
            {
                return BadRequest("Invalid token");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);

            return Ok();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);

            if (user == null)
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Send email with token logic here

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false) 
                return NoContent();

            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (user == null) 
                return Unauthorized();

            return Ok(new
            {
                user.NombreCompleto,
                user.Email,
                user.Id 
            });
        }
        
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }
    }
}