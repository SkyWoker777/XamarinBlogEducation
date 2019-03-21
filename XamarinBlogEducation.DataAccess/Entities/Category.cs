using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
    [Table("Catgories")]
    public class Category : BaseEntity
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        public string CategoryName { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
