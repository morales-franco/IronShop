using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos
{
    public class LoginGoogleDto
    {
        [Required, MaxLength(256), EmailAddress]
        public string Email { get; set; }
        [Required]
        public string TokenGoogle { get; set; }
    }
}
