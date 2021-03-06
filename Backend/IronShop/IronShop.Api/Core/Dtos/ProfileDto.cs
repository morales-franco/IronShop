﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IronShop.Api.Core.Dtos
{
    public class ProfileDto
    {
        [Required]
        public int UserId { get; set; }
        [Required, MaxLength(512)]
        public string FullName { get; set; }
        [Required, MaxLength(256), EmailAddress]
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string ImageFileName { get; set; }
        public bool GoogleAuth { get; set; }
        public List<PermissionDto> Permissions { get; set; }

        public ProfileDto()
        {
            Permissions = new List<PermissionDto>();
        }

    }
}
