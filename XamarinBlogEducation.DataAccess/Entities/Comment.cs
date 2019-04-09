using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
   
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        public long PostId { get; set; }
        public string UserId { get; set; }

       // [ForeignKey(nameof(User))]
        public  ApplicationUser User { get; set; }
       // [ForeignKey(nameof(Post))]
        public  Post Post { get; set; }

    }
}
