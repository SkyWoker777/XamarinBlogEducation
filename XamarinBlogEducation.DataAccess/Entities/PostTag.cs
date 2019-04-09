using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
    public class PostTag:BaseEntity
    {
        [Required]
        public long PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        [Required]
        public long TagId { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
    }
}
