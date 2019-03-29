using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XamarinBlogEducation.ViewModels.Models.Blog
{
    public class CreatePostBlogViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Title { get; set; }
        [StringLength(300, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 20)]
        public string Description { get; set; }
        [Required]
        [StringLength(10000, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 300)]
        public string Content { get; set; }
        [Required]
        public long CategoriesId { get; set; }
        [StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 5)]
        public string Author { get; set; }
        public string AuthorId { get; set; }

    }
}
