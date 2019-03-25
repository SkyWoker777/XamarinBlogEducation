using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Models.Account
{
    public class EditAccountViewModel
    {
        public string Email { get; set; }
        
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
        public byte[] UserImage { get; set;}
    }
}
