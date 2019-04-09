using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{

    
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }

        public long CategoryId { get; set; }
        public string AuthorId { get; set; }

      //  [ForeignKey(nameof(Category))]
        public  Category Category { get; set; }
       // [ForeignKey(nameof(ApplicationUser))]
        public  ApplicationUser ApplicationUser { get; set; }
        public IList<PostTag> PostTags { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
