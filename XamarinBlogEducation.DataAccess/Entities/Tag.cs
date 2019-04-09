using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
   public class Tag:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        
    }
}
