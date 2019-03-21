using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XamarinBlogEducation.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }

        public BaseEntity()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}
