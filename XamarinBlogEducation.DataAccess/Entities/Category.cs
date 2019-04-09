using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
