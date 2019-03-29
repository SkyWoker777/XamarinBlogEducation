using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{

    [Table("Posts")]
    public class Post : BaseEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();

        }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        [ForeignKey("Category")]
        public long CategoryId { get; set; }
        [ForeignKey("AuthorId")]
        public string AuthorId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
