using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
   
    public class Comment : BaseEntity
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public long PostId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public  ApplicationUser User { get; set; }

        [ForeignKey(nameof(PostId))]
        public  Post Post { get; set; }

    }
}
