using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
    [Table("Comments")]
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        [ForeignKey("Post")]
        public long PostId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }


        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }

    }
}
