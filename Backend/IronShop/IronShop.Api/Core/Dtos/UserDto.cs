﻿using System;
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

}
