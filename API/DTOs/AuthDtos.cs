using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } = "";
        [Required]
        public string Nombre { get; set; } = "";
        [Required]
        public string PrimerApellido { get; set; } = "";
        public string SegundoApellido { get; set; } = "";
        public int GrupoId { get; set; }
        public int RolId { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ForgotPasswordDto
    {
        public string Email { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }

    public class Enable2FADto
    {
        public string Email { get; set; }
    }

    public class Verify2FADto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }    
}