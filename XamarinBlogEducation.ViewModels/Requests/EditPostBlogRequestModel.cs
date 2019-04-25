using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Requests
{
    public class EditPostBlogRequestModel
    {
        public long Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Title { get; set; }
        [StringLength(300, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 20)]
        public string Description { get; set; }
        [Required]
        [StringLength(10000, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 300)]
        public string Content { get; set; }
    }
}
