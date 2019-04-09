using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace XamarinBlogEducation.DataAccess.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public byte[] UserImage { get; set; }
    }
}
