using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{

    
    public class Post : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string AuthorName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public long CategoryId { get; set; }

        public string AuthorId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
