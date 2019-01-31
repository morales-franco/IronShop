using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos
{
    public class UserDto : RegisterUserDto
    {
        [Required]
        public int UserId { get; set; }
    }

    public class ChangeUserDto
    {
        [Required]
        public int UserId { get; set; }
        [Required, MaxLength(512)]
        public string FullName { get; set; }
        [Required, MaxLength(256), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string Role { get; set; }
    }

    public class RegisterUserDto
    {
        [Required, MaxLength(512)]
        public string FullName { get; set; }
        [Required, MaxLength(256), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }
        [Required, MaxLength(50)]
        public string Role { get; set; }
    }

    public class ResetPasswordDto
    {
        [Required]
        public int UserId { get; set; }
        [Required, MaxLength(256), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }
    }

}
