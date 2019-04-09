using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
